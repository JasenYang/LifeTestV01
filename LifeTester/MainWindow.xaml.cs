using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LifeTester.UC;
using LifeTester.ViewModel;
using LifeTester.Commands;
using LifeTester.View;
using System.Timers;
using LifeTester.Model;
using System.Diagnostics;
using System.Threading;
using LifeTester.Util;
using NAudio.CoreAudioApi;

namespace LifeTester
{
    /// <summary>
    /// 主窗口
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConfigManager app = new AppConfigManager();
            var style = (Style)this.TryFindResource("ViewUCStyle");
            var mainPage = new MainPage();
            mainPage.Style = style;
            mainPageGrid.Children.Add(mainPage);

            var signalSettingPage = new SignalSettingPage();
            signalSettingPage.Style = style;
            signalSettingPageGrid.Children.Add(signalSettingPage);

            var resultAdjudgeSettingPage = new ResultAdjudgeSettingPage();
            resultAdjudgeSettingPage.Style = style;
            resultAdjudgeSettingPageGrid.Children.Add(resultAdjudgeSettingPage);

            var controlPage = new ControlPage();
            controlPage.Style = style;
            controlPageGrid.Children.Add(controlPage);

            var resultPage = new ResultPage();
            resultPage.Style = style;
            resultPageGrid.Children.Add(resultPage);

            var vm = new MainWindowVM();
            vm.Init();
            vm.AddCommands(this);

            vm.PageNameAndVMDic.Add(MainWindowVM.MAIN_PAGE, mainPage.VM);
            vm.PageNameAndVMDic.Add(MainWindowVM.SIGNAL_SETTING_PAGE, signalSettingPage.VM);
            vm.PageNameAndVMDic.Add(MainWindowVM.RESULT_ADJUDGE_SETTING_PAGE, resultAdjudgeSettingPage.VM);
            vm.PageNameAndVMDic.Add(MainWindowVM.CONTROL_PAGE, controlPage.VM);
            vm.PageNameAndVMDic.Add(MainWindowVM.RESULT_PAGE, resultPage.VM);

            vm.InitObservers();

            VM = vm;
            this.InitCommandBindings();
            this.DataContext = VM;

            var deviceEnumerator = new MMDeviceEnumerator();
            foreach (var d in deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                if (d.FriendlyName.Contains(app.GetAppValue("CardDevicesOne")))
                {

                    d.AudioEndpointVolume.MasterVolumeLevelScalar = Convert.ToInt32(app.GetAppValue("CardDevicesOneVolume")) / 100.0f;
                }
                if (d.FriendlyName.Contains(app.GetAppValue("CardDevicesTwo")))
                {
                    d.AudioEndpointVolume.MasterVolumeLevelScalar = Convert.ToInt32(app.GetAppValue("CardDevicesTwoVolume")) / 100.0f;
                }
            }




            try
            {
                ISerial serialPort = new SerialPortEx(TesterConfig.Instance.GetApplicationCom);
                //serialPort.Wirte("ff", 1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "程序启动时写入ff失败");
                throw;
            }



            //aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            //aTimer.Interval = 1000;    // 1秒 = 1000毫秒
            //aTimer.Start();
          HistoryTimer hisTimer = new HistoryTimer();
        }
        //System.Timers.Timer aTimer = new System.Timers.Timer();
        //static int elapsedTimes;
        //static readonly Int64 count=20*3600*24;
        //static Int64 currentCount = 0;
        ///// <summary>
        ///// Timer的Elapsed事件处理程序
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="e"></param>
        //private static void OnTimedEvent(object source, ElapsedEventArgs e)
        //{
        //    currentCount++;

        //    for (int i = 0; i < 10000; i++)
        //    {
        //        Random ra = new Random();
        //        Channel tmp = new Channel();
        //        tmp.Voltage = ra.Next(16, 20);
        //        tmp.ActualDuration = ra.Next(1, 59) * 1000;
        //        tmp.CardNumber = ra.Next(1, 2).ToString();
        //        tmp.Duration = ra.Next(1, 59) * 1000;
        //        tmp.Electricity = ra.Next(1, 20);
        //        tmp.Number = ra.Next(1, 10).ToString();
        //        tmp.ImageTooltip = "";
        //        tmp.Ohm = ra.Next(1, 20);
        //        tmp.VoltageType = VoltageType.Effective;
        //        ChannelHistory history = new ChannelHistory();
        //        history.CopyFrom(tmp);
        //        Cache.Instance.HistorySates.Add(history);
        //    }

        //    //if (currentCount>count)
        //    //{
        //    //    MessageBox.Show("已经达到上相"+count);
        //    //}
        //    Debug.WriteLine(currentCount);
        //}

    }
}
