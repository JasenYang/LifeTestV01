using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.IO.Ports;

namespace LifeTester.Model
{
    public class TesterConfig
    {
        private static TesterConfig config;

        private List<SerialConfig> serialConfigs;

        public List<SerialConfig> SerialConfigs
        {
            get
            {
                return serialConfigs;
            }
        }

        private SerialConfig _getapplicationcom;
        /// <summary>
        /// 获取applicationCom配置数据
        /// </summary>
        public SerialConfig GetApplicationCom
        {

            get
            {
                _getapplicationcom = new SerialConfig();

                _getapplicationcom.BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings.Get("BaudRate"));
                _getapplicationcom.DataBits = Convert.ToInt32(ConfigurationManager.AppSettings.Get("DataBits"));
                _getapplicationcom.PortName = ConfigurationManager.AppSettings.Get("PortName");
                _getapplicationcom.ReadBufferSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ReadBufferSize"));
                _getapplicationcom.ReadTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ReadTimeout"));
                StopBits stopBits;
                Enum.TryParse<StopBits>(ConfigurationManager.AppSettings.Get("StopBits"), out stopBits);
                _getapplicationcom.StopBits = stopBits;
                _getapplicationcom.WriteBufferSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("WriteBufferSize"));
                _getapplicationcom.WriteTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("WriteTimeout"));

                return _getapplicationcom;
            }
        }


        private int channelCount;

        /// <summary>
        /// 通道数
        /// </summary>
        public int ChannelCount
        {
            get { return channelCount; }
        }

        private TesterConfig()
        {
            serialConfigs = new List<SerialConfig>(4);

            LoadConfig();
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadConfig()
        {
            string strCount = ConfigurationManager.AppSettings["channelcount"];
            int.TryParse(strCount, out channelCount);
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            System.Diagnostics.Debug.Assert(cfg != null);
            var group = cfg.GetSectionGroup("COM");
            Type type = typeof(SerialConfig);
            try
            {
                PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (group != null && group.Sections.Count > 0)
                {
                    foreach (DefaultSection section in group.Sections)
                    {
                        IDictionary dict = ConfigurationManager.GetSection(section.SectionInformation.SectionName) as IDictionary;
                        var serialConfig = new SerialConfig();
                        if (dict != null && dict.Count > 0)
                        {
                            foreach (DictionaryEntry item in dict)
                            {
                                var propertyInfo = propertyInfos.ToList().Find(p => p.Name.Equals(item.Key));
                                if (propertyInfo != null)
                                {
                                    if (propertyInfo.PropertyType == typeof(string))
                                        propertyInfo.SetValue(serialConfig, item.Value, null);
                                    else if (propertyInfo.PropertyType == typeof(int))
                                        propertyInfo.SetValue(serialConfig, Convert.ToInt32(item.Value), null);
                                    else if (propertyInfo.PropertyType == typeof(StopBits))
                                    {
                                        StopBits stopBits;
                                        Enum.TryParse<StopBits>(item.Value.ToString(), out stopBits);
                                        propertyInfo.SetValue(serialConfig, stopBits, null);
                                    }
                                    else if (propertyInfo.PropertyType == typeof(Parity))
                                    {
                                        Parity parity;
                                        Enum.TryParse<Parity>(item.Value.ToString(), out parity);
                                        propertyInfo.SetValue(serialConfig, parity, null);

                                    }
                                }
                            }

                            //App.config不支持以数字为名称的节点，添加数字节点时需添加前缀"A_"
                            if (section.SectionInformation.Name.StartsWith("A_"))
                                serialConfig.CardNumber = section.SectionInformation.Name.Substring(2);
                            else
                                serialConfig.CardNumber = section.SectionInformation.Name;

                            serialConfigs.Add(serialConfig);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 调用时实例化
        /// </summary>
        public static TesterConfig Instance
        {
            get
            {
                if (config == null)
                    config = new TesterConfig();

                return config;
            }
        }

        public SerialConfig GetSerialConfig(string cardNumber)
        {
            return serialConfigs.Find(c => c.CardNumber.Equals(cardNumber));
        }
    }
}
