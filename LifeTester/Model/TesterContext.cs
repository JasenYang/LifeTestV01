using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LifeTester.Model
{
    public class TesterContext : ICommand
    {
        private ISerial serialPort;
        /// <summary>
        /// 当前串口
        /// </summary>
        public ISerial CurrentSerialPort
        {
            get { return serialPort; }
        }

        private ReadThread readThread;
        private ICommand command;
        private Mutex mu = new Mutex();
        private Card card;

        /// <summary>
        /// 当前操作的板卡
        /// </summary>
        public Card CurrentCard
        {
            get
            {
                return card;
            }
        }

        public TesterContext(Card card, ISerial serialPort)
        {
            this.card = card;
            this.serialPort = serialPort;
            this.readThread = new ReadThread(serialPort);
            this.command = new Command(serialPort);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Release()
        {
            try
            {
                if (serialPort.IsOpen)
                    serialPort.Close();

                card.Stop();
                readThread.Stop();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 设置板卡信号源
        /// </summary>
        /// <param name="siganlSource"></param>
        public void SetSignalSource(string siganlSource)
        {
            card.SignalSource = siganlSource;
        }

        /// <summary>
        /// 设置板卡测试电压
        /// </summary>
        /// <param name="strVoltage"></param>
        public void SetCardVoltage(string strVoltage)
        {
            double voltage;
            double.TryParse(strVoltage, out voltage);
            if (voltage > 0)
                SetCardVoltage(voltage);
        }

        /// <summary>
        /// 设置板卡测试电压
        /// </summary>
        /// <param name="voltage"></param>
        public void SetCardVoltage(double voltage)
        {
            card.Voltage = voltage;
            card.Channels.ForEach(h => h.Voltage = voltage);
        }

        /// <summary>
        /// 设置通道实际测试的时间
        /// </summary>
        /// <param name="channelNumber">通道号</param>
        /// <param name="actualTime">实际测试时间</param>
        public void SetChannelActualTime(string channelNumber, double actualTime)
        {
            var channel = card.Channels.Find(h => h.Number.Equals(channelNumber));
            if (channel != null)
            {
                channel.ActualDuration = actualTime;
            }
        }

        /// <summary>
        /// 设置所有通道测试时间
        /// </summary>
        /// <param name="cardNumber"></param>
        public void SetAllChannelTime(double setTime)
        {
            card.Channels.ForEach(h =>
            {
                h.Duration = setTime;
            });
        }

        /// <summary>
        /// 开始测试所有通道
        /// </summary>
        /// <returns></returns>
        public bool Start(string channelNumber = null)
        {

            try
            {

                if (!serialPort.IsOpen)
                    serialPort.Open();//打开串口
                LogHelper.WriteInfoLog(string.Format("开始测试所有通道"));
                bool ret = command.Start(channelNumber);//寻找和组合start指令，在command.cs文件中//当系统下发了启动指令后

                if (ret)//启动指令发送成功的时候，启动readThread线程，监控串口传来的数据
                {
                    LogHelper.WriteInfoLog("启动指令发送成功，启动readThread线程，监控串口传来的数据");
                    readThread.Start();
                }
                return ret;

                // return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                LogHelper.WriteInfoLog("开始测试所有通道失败：" + ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 停止测试所有通道
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {

            try
            {

                LogHelper.WriteInfoLog(string.Format("停止测试所有通道"));
                bool ret = command.Stop();
                if (ret)
                {
                    LogHelper.WriteInfoLog("关闭指令发送成功，关闭readThread线程，停止监控串口传来的数据");
                    readThread.Stop();
                }

                return ret;

            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("停止测试所有通道失败：" + ex.Message);
                return false;
            }
            //readThread.Stop();

            // bool ret = command.Stop();

        }

        /// <summary>
        /// 暂停测试所有通道
        /// </summary>
        /// <returns></returns>
        public bool Pause()
        {

            //Cache._mu.WaitOne();
            try
            {

                LogHelper.WriteInfoLog(string.Format("暂停测试所有通道"));
                bool ret = command.Pause();
                if (ret)
                {
                    readThread.Stop();
                }


                return ret;

            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("暂停测试所有通道失败：" + ex.Message);
                return false;
            }
            //finally
            //{
            //   PlayerModel._mu.ReleaseMutex();
            //}


        }

        /// <summary>
        /// 暂停测试特定通道
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        public bool Pause(string channelNumber)
        {


            try
            {

                LogHelper.WriteInfoLog(string.Format("暂停测试{0}通道", channelNumber));
                bool ret = command.Pause(channelNumber);
                if (ret)
                {

                    // readThread.Stop(); //by ldb 不应该操作总线程
                }

                return ret;

            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("暂停测试特定通道失败：" + ex.Message);
                return false;
            }

        }



        /// <summary>
        /// 继续测试所有通道
        /// </summary>
        /// <returns></returns>
        public bool Continue()
        {
            try
            {

                LogHelper.WriteInfoLog("继续测试所有通道");
                bool ret = command.Continue();
                if (ret)
                {
                    readThread.Start();
                }

                return ret;

            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("继续测试所有通道失败：" + ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 继续测试特定通道
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        public bool Continue(string channelNumber)
        {
            try
            {

                LogHelper.WriteInfoLog(string.Format("继续测试{0}通道", channelNumber));
                bool ret = command.Continue(channelNumber);
                if (ret)
                {
                    //readThread.Start();//by ldb 不应该操作总线程
                }

                return ret;


            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("继续测试特定通道失败：" + ex.Message);
                return false;
            }

        }
    }
}
