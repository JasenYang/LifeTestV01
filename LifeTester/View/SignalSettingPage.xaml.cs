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
using LifeTester.Model;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using LifeTester.Util;

namespace LifeTester.View
{
    /// <summary>
    /// 表示信号设置页面
    /// </summary>
    public partial class SignalSettingPage : BaseView
    {
        AppConfigManager app = new AppConfigManager();
        public SignalSettingPage()
        {
            InitializeComponent();
            VM = new SignalSettingPageVM();
            VM.AddCommands(this);
            this.DataContext = VM;
            //cbxSweepDeviceOne.ItemsSource = DirectSoundOut.Devices;
            //cbxSweepDeviceOne.DisplayMemberPath = "Description";
            //cbxSweepDeviceOne.SelectedValuePath = "Guid";
            // cbxSweepDeviceOne.SelectionChanged += CbxSweepDeviceOne_SelectionChanged;
            // int _CardDevicesOne = Convert.ToInt32();
            //if (cbxSweepDeviceOne.Items.Count >= _CardDevicesOne + 1)
            //{
            //    cbxSweepDeviceOne.SelectedIndex = _CardDevicesOne;
            //}
            //else
            //{
            //    cbxSweepDeviceOne.SelectedIndex = 0;
            //}
            foreach (var item in DirectSoundOut.Devices)
            {
                if (item.Description.Contains(app.GetAppValue("CardDevicesOne")))
                {
                    PlayerModel.DirectSoundOutDeviceOne = item.Guid;
                }
                if (item.Description.Contains(app.GetAppValue("CardDevicesTwo")))
                {
                    PlayerModel.DirectSoundOutDeviceTwo = item.Guid;
                }
            }

            //cbxSweepDeviceTwo.ItemsSource = DirectSoundOut.Devices;
            //cbxSweepDeviceTwo.DisplayMemberPath = "Description";
            //cbxSweepDeviceTwo.SelectedValuePath = "Guid";
            //cbxSweepDeviceTwo.SelectionChanged += CbxSweepDeviceTwo_SelectionChanged;
            //int _CardDevicesTwo = Convert.ToInt32(app.GetAppValue("CardDevicesTwo"));
            //if (cbxSweepDeviceTwo.Items.Count >= _CardDevicesTwo + 1)
            //{
            //    cbxSweepDeviceTwo.SelectedIndex = _CardDevicesTwo;
            //}
            //else
            //{
            //    cbxSweepDeviceTwo.SelectedIndex = 0;
            //}


        }

        private void CbxSweepDeviceTwo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DirectSoundDeviceInfo ds = cbxSweepDeviceTwo.SelectedItem as DirectSoundDeviceInfo;
            PlayerModel.DirectSoundOutDeviceTwo = (Guid)cbxSweepDeviceTwo.SelectedValue;
            LogHelper.WriteInfoLog("选择的声卡二：" + ds.ModuleName + "||" + ds.Description + "||" + ds.Guid);

        }

        private void CbxSweepDeviceOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DirectSoundDeviceInfo ds = cbxSweepDeviceOne.SelectedItem as DirectSoundDeviceInfo;
            PlayerModel.DirectSoundOutDeviceOne = (Guid)cbxSweepDeviceOne.SelectedValue;
            LogHelper.WriteInfoLog("选择的声卡一：" + ds.ModuleName + "||" + ds.Description + "||" + ds.Guid);
        }

        private void btnBrowser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBrowser2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSweepStart_Click(object sender, RoutedEventArgs e)
        {
            //WaveOutEvent woe = new WaveOutEvent();
            //woe.DeviceNumber = cbxSweepDevice.SelectedIndex;
            //Cache.driverOut = woe;
            //Cache.wg = new SignalGenerator();
            //Cache.driverOut.Init(Cache.wg);
            //Cache.wg.Type = SignalGeneratorType.Sweep;
            //Cache.wg.Frequency = Convert.ToDouble(duStartfrequency.Text);
            //Cache.wg.FrequencyEnd = Convert.ToDouble(duEndfrequency.Text);
            //Cache.wg.SweepLengthSecs = Convert.ToDouble(duSweepLength.Text);
            //Cache.driverOut.Play();
        }

        private void btnSweepEnd_Click(object sender, RoutedEventArgs e)
        {
            //if (Cache.driverOut != null)
            //{
            //    Cache.driverOut.Stop();
            //}

        }

        private void ckSweepOne_Checked(object sender, RoutedEventArgs e)
        {

            gbsweepOne.Visibility = Visibility.Visible;
            btnBrowser.IsEnabled = false;
            btnBrowser.Visibility = Visibility.Collapsed;
            if (PlayerModel._dsOne != null)
            {
                PlayerModel._dsOne.Dispose();
                PlayerModel._dsOne = null;
            }
            if (PlayerModel.wgOne != null)
            {
                PlayerModel.wgOne = null;
            }
            PlayerModel.IsCheckSweepOne = true;

        }

        private void ckSweepTwo_Checked(object sender, RoutedEventArgs e)
        {


            gbsweepTwo.Visibility = Visibility.Visible;
            btnBrowser2.IsEnabled = false;
            btnBrowser2.Visibility = Visibility.Collapsed;
            PlayerModel.IsCheckSweepTwo = true;
            if (PlayerModel._dsTwo != null)
            {
                PlayerModel._dsTwo.Dispose();
                PlayerModel._dsTwo = null;
            }
            if (PlayerModel.wgTwo != null)
            {
                PlayerModel.wgTwo = null;
            }
            //if (Cache._dsTwo == null)
            //{
            //    DirectSoundOut woe = new DirectSoundOut(Cache.DirectSoundOutDeviceTwo);
            //    Cache._dsTwo = woe;
            //    Cache.wgTwo = new SignalGenerator();
            //    Cache._dsTwo.Init(Cache.wgTwo);
            //    Cache.wgTwo.Type = SignalGeneratorType.Sweep;
            //    Cache.wgTwo.Frequency = Convert.ToDouble(duStartfrequencyTwo.Text);
            //    Cache.wgTwo.FrequencyEnd = Convert.ToDouble(duEndfrequencyTwo.Text);
            //    Cache.wgTwo.SweepLengthSecs = Convert.ToDouble(duSweepLengthTwo.Text);

            //}
        }

        private void ckSweepOne_Unchecked(object sender, RoutedEventArgs e)
        {
            gbsweepOne.Visibility = Visibility.Collapsed;
            btnBrowser.IsEnabled = true;
            btnBrowser.Visibility = Visibility.Visible;
            PlayerModel.IsCheckSweepOne = false;

            if (PlayerModel._dsOne != null)
            {
                PlayerModel._dsOne.Dispose();
                PlayerModel._dsOne = null;
            }
            if (PlayerModel.wgOne != null)
            {
                PlayerModel.wgOne = null;
            }

        }

        private void ckSweepTwo_Unchecked(object sender, RoutedEventArgs e)
        {
            btnBrowser2.IsEnabled = true;
            btnBrowser2.Visibility = Visibility.Visible;
            gbsweepTwo.Visibility = Visibility.Collapsed;
            PlayerModel.IsCheckSweepTwo = false;
            if (PlayerModel._dsTwo != null)
            {
                PlayerModel._dsTwo.Dispose();
                PlayerModel._dsTwo = null;
            }
            if (PlayerModel.wgTwo != null)
            {
                PlayerModel.wgTwo = null;
            }
        }

        private void SweepLengthTwo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlayerModel.SweepLengthTwo = Convert.ToDouble(SweepLengthTwo.Value);
        }

        private void EndfrequencyTwo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlayerModel.EndfrequencyTwo = Convert.ToDouble(EndfrequencyTwo.Value);
        }

        private void StartfrequencyTwo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlayerModel.StartfrequencyTwo = Convert.ToDouble(StartfrequencyTwo.Value);
        }

        private void SweepLengthOne_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlayerModel.SweepLengthOne = Convert.ToDouble(SweepLengthOne.Value);
        }

        private void EndfrequencyOne_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlayerModel.EndfrequencyOne = Convert.ToDouble(EndfrequencyOne.Value);
        }

        private void StartfrequencyOne_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlayerModel.StartfrequencyOne = Convert.ToDouble(StartfrequencyOne.Value);
        }
    }
}
