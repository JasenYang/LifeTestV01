using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using LifeTester.Model;

namespace LifeTester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {

            if (PlayerModel.afrOne != null)
            {
                PlayerModel.afrOne.Dispose();
                PlayerModel.afrOne = null;
            }
            if (PlayerModel.afrTwo != null)
            {
                PlayerModel.afrTwo.Dispose();
                PlayerModel.afrTwo = null;
            }

            if (PlayerModel.loopOne != null)
            {
                PlayerModel.loopOne.Dispose();
                PlayerModel.loopOne = null;
            }
            if (PlayerModel.loopTwo != null)
            {
                PlayerModel.loopTwo.Dispose();
                PlayerModel.loopTwo = null;
            }

            if (PlayerModel._dsOne != null)
            {
                PlayerModel._dsOne.Dispose();
                PlayerModel._dsOne = null;
            }
            if (PlayerModel._dsTwo != null)
            {
                PlayerModel._dsTwo.Dispose();
                PlayerModel._dsTwo = null;
            }

            if (PlayerModel.wgOne != null)
            {
                PlayerModel.wgOne = null;
            }
            if (PlayerModel.wgTwo != null)
            {
                PlayerModel.wgTwo = null;
            }


            try
            {
                ISerial serialPort = new SerialPortEx(TesterConfig.Instance.GetApplicationCom);
                serialPort.Wirte("09", 1);
                serialPort.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "程序关闭时写入09失败");
                throw;
            }
            if (Cache.Instance.Contexts != null && Cache.Instance.Contexts.Any())
            {
                try
                {
                    //关闭所有的串口
                    Cache.Instance.Contexts.ForEach(c =>
                        {
                            c.Release();
                        });


                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }



        }
    }
}
