using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.Wave.WaveStreams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestAudio
{
    public partial class Form1 : Form
    {
        private DirectSoundOut _dsOne;
        private DirectSoundOut _dsTwo;
        private AudioFileReader afrOne = null;
        private LoopStream loopOne = null;
        private AudioFileReader afrTwo = null;
        private LoopStream loopTwo = null;
        private SignalGenerator wg;
        private SignalGenerator wgTwo;

        private string _FilePathOne = string.Empty;
        private int _DeviceOne;
        private string _FilePathTwo = string.Empty;
        private int _DeviceTwo;
        private IWavePlayer driverOut;
        private IWavePlayer driverOutTwo = null;

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // DirectSoundOut.Devices.coun


            //for (int deviceId = 0; deviceId < WaveOut.DeviceCount; deviceId++)
            //{
            //    var capabilities = WaveOut.GetCapabilities(deviceId);

            //    cmbSoundCardOne.Items.Add(String.Format("Device {0} ({1})", deviceId, capabilities.ProductName));
            //    cmbSoundCardTwo.Items.Add(String.Format("Device {0} ({1})", deviceId, capabilities.ProductName));
            //    cbxsweepsevice.Items.Add(String.Format("Device {0} ({1})", deviceId, capabilities.ProductName));
            //    // comboBoxWaveOutDevice.Items.Add(String.Format("Device {0} ({1})", deviceId, capabilities.ProductName));
            //}

            cmbSoundCardOne.DisplayMember = "Description";
            cmbSoundCardOne.ValueMember = "Guid";
            cmbSoundCardOne.DataSource = DirectSoundOut.Devices;

            cmbSoundCardOne.SelectedIndex = 0;

            cmbSoundCardTwo.DisplayMember = "Description";
            cmbSoundCardTwo.ValueMember = "Guid";
            cmbSoundCardTwo.DataSource = DirectSoundOut.Devices;
            cmbSoundCardTwo.SelectedIndex = 0;
            cbxsweepsevice.DisplayMember = "Description";
            cbxsweepsevice.ValueMember = "Guid";
            cbxsweepsevice.DataSource = DirectSoundOut.Devices;
            cbxsweepsevice.SelectedIndex = 0;

            cbxsweepseviceTwo.DisplayMember = "Description";
            cbxsweepseviceTwo.ValueMember = "Guid";
            cbxsweepseviceTwo.DataSource = DirectSoundOut.Devices;
            cbxsweepseviceTwo.SelectedIndex = 0;








            ////也可以获取输出设备

            //DirectSoundOut.Devices //获取声卡

            //foreach (var device in DirectSoundOut.Devices)
            //{
            //    Debug.WriteLine(String.Format("{0} {1} {2}", device.Description, device.ModuleName, device.Guid));
            //}

            //var waveOutDevice = new WaveOut();

            //var audioFileReader = new AudioFileReader(@"C:\Users\netsm\Desktop\1.wav");

            ////waveOutDevice.Init(audioFileReader);

            //waveOutDevice.DeviceNumber = 1;
            //waveOutDevice.Play();
            //IWavePlayer device;
            //waveOut = new WaveOutEvent();
            //waveOut.DeviceNumber = 0; //获取的输出设备
            //waveOut.DesiredLatency = 300; //延迟执行毫秒数
            //waveOut.NumberOfBuffers = 2;
            //IWavePlayer device = waveOut; //使用此输出设备实现播放接口
            //                              // var waveOutDevice = new WaveOut();
            //var audioFileReader = new AudioFileReader(@"C:\Users\netsm\Desktop\1.wav");
            //// waveOutDevice.Resume();

            //waveOut.Init(audioFileReader, true);
            //waveOut.PlaybackStopped += WaveOut_PlaybackStopped;

            // 关闭释放
            //if (waveOut != null)
            //{
            //    waveOut.Stop();
            //}

            //if (waveOut != null)
            //{
            //    waveOut.Dispose();
            //    waveOut = null;
            //}
            // device.Init()



        }


        private void SpecifiedDevicePlayTwo(string _FilePath, int _Device)
        {
            //_FilePathTwo = _FilePath;
            //_DeviceTwo = _Device;
            //_WaveOutEventTwo = new WaveOutEvent();
            //IWavePlayer wp = _WaveOutEventTwo;

            //_WaveOutEventTwo.DeviceNumber = _Device; //获取的输出设备
            //_WaveOutEventTwo.DesiredLatency = 300; //延迟执行毫秒数
            //_WaveOutEventTwo.NumberOfBuffers = 2;
            //_WaveOutEventTwo.Init(new AudioFileReader(_FilePath), true);
            //_WaveOutEventTwo.PlaybackStopped += _WaveOutEventTwo_PlaybackStopped;
            //_WaveOutEventTwo.Play();
        }

        private void _WaveOutEventTwo_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            //if (_WaveOutEventTwo != null)
            //{
            //    if (_WaveOutEventTwo.PlaybackState == PlaybackState.Playing) _WaveOutEventTwo.Stop();
            //    _WaveOutEventTwo.Dispose();
            //    _WaveOutEventTwo = null;
            //}
            //SpecifiedDevicePlayTwo(_FilePathTwo, _DeviceTwo);

        }

        private void SpecifiedDevicePlayOne(string _FilePath, int _Device)
        {
            //_FilePathOne = _FilePath;
            //_DeviceOne = _Device;
            //_WaveOutEventOne = new WaveOutEvent();
            //_WaveOutEventOne.DeviceNumber = _Device; //获取的输出设备
            //_WaveOutEventOne.DesiredLatency = 300; //延迟执行毫秒数
            //_WaveOutEventOne.NumberOfBuffers = 2;
            //_WaveOutEventOne.Init(new AudioFileReader(_FilePath), true);
            //_WaveOutEventOne.PlaybackStopped += _WaveOutEvent_PlaybackStopped;
            //_WaveOutEventOne.Play();
        }

        private void _WaveOutEvent_PlaybackStopped(object sender, StoppedEventArgs e)
        {

            //if (_WaveOutEventOne != null)
            //{
            //    _WaveOutEventOne.Stop();
            //    _WaveOutEventOne.Dispose();
            //    _WaveOutEventOne = null;
            //}
            //SpecifiedDevicePlayOne(_FilePathOne, _DeviceOne);
        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            //if (waveOut.PlaybackState == PlaybackState.Stopped)
            //{
            //    waveOut.Play();
            //    Thread.Sleep(100);
            //}
        }

        private void btnStartPalyer_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtPathOne.Text))
            {
                MessageBox.Show("请浏览或填写文件一地址");
                return;
            }
            if (string.IsNullOrEmpty(txtPathOne.Text))
            {
                MessageBox.Show("请浏览或填写文件一地址");
                return;
            }

            afrOne = new AudioFileReader(txtPathOne.Text);
            loopOne = new LoopStream(afrOne);
            SampleChannel sclOne = new SampleChannel(loopOne, true);
            sclOne.Volume = 1.0f;
            _dsOne = new DirectSoundOut((Guid)cmbSoundCardOne.SelectedValue);
            _dsOne.Init(new MeteringSampleProvider(sclOne));
            _dsOne.Play();



            afrTwo = new AudioFileReader(txtPathTwo.Text);
            loopTwo = new LoopStream(afrTwo);
            SampleChannel sclTwo = new SampleChannel(loopTwo, true);
            _dsTwo = new DirectSoundOut((Guid)cmbSoundCardTwo.SelectedValue);
            _dsTwo.Init(CreateInputStream(txtPathTwo.Text, 1.0f));
            _dsTwo.Play();


            //_lsOne = new LoopedSong(txtPathOne.Text, cmbSoundCardOne.SelectedIndex, true, -1);
            //_lsOne.Play();
            //_lsTwo = new LoopedSong(txtPathTwo.Text, cmbSoundCardTwo.SelectedIndex, true, -1);
            //_lsTwo.Play();
            //SpecifiedDevicePlayOne("1.wav", cmbSoundCardOne.SelectedIndex);
            //SpecifiedDevicePlayTwo("2.wav", cmbSoundCardTwo.SelectedIndex);
        }

        private ISampleProvider CreateInputStream(string fileName, float _volume = 1.0f)
        {
            //using ()
            //{

            AudioFileReader audioFileReader = new AudioFileReader(fileName);
            LoopStream loop = new LoopStream(audioFileReader);
            SampleChannel sampleChannel = new SampleChannel(loop, true);
            sampleChannel.Volume = _volume;
            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            return postVolumeMeter;



            //
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dsOne != null)
            {
                afrOne.Dispose();
                loopOne.Dispose();
                _dsOne.Dispose();
            }
            if (_dsTwo != null)
            {
                afrTwo.Dispose();
                loopTwo.Dispose();
                _dsTwo.Dispose();
            }
            if (driverOut != null)
            {
                driverOut.Dispose();
            }
            //if (_WaveOutEventOne != null)
            //{
            //    _WaveOutEventOne.Dispose();
            //    _WaveOutEventOne = null;
            //}

            //if (_WaveOutEventTwo != null)
            //{
            //    _WaveOutEventTwo.Dispose();
            //    _WaveOutEventTwo = null;
            //}

        }

        private void btnClosePalyer_Click(object sender, EventArgs e)
        {
            if (_dsOne != null)
            {
                afrOne.Dispose();
                loopOne.Dispose();
                _dsOne.Dispose();
            }
            if (_dsTwo != null)
            {
                afrTwo.Dispose();
                loopTwo.Dispose();
                _dsTwo.Dispose();
            }
            if (driverOut != null)
            {
                driverOut.Dispose();
                driverOut = null;
            }
            if (driverOutTwo != null)
            {
                driverOut.Dispose();
                driverOut = null;
            }

            //if (_WaveOutEventOne != null)
            //{
            //    _WaveOutEventOne.Stop();
            //    _WaveOutEventOne.Dispose();
            //    _WaveOutEventOne = null;
            //}


            //if (_WaveOutEventTwo != null)
            //{
            //    _WaveOutEventTwo.Stop();
            //    _WaveOutEventTwo.Dispose();
            //    _WaveOutEventTwo = null;
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbSoundCardOne_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSoundCardTwo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            string allExtensions = "*.wav;*.aiff;*.mp3;*.aac";
            openFileDialog.Filter = String.Format("All Supported Files|{0}|All Files (*.*)|*.*", allExtensions);
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPathOne.Text = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            string allExtensions = "*.wav;*.aiff;*.mp3;*.aac";
            openFileDialog.Filter = String.Format("All Supported Files|{0}|All Files (*.*)|*.*", allExtensions);
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPathTwo.Text = openFileDialog.FileName;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnsweepStart_Click(object sender, EventArgs e)
        {
            DirectSoundOut woe = new DirectSoundOut((Guid)cbxsweepsevice.SelectedValue);
            driverOut = woe;
            wg = new SignalGenerator();
            driverOut.Init(wg);
            wg.Type = SignalGeneratorType.Sweep;
            wg.Frequency = Convert.ToDouble(txtminhz.Text);
            wg.FrequencyEnd = Convert.ToDouble(txtmaxhz.Text);
            wg.SweepLengthSecs = Convert.ToDouble(txtsweeptime.Text);
            driverOut.Play();

            DirectSoundOut woeTwo = new DirectSoundOut((Guid)cbxsweepseviceTwo.SelectedValue);
            driverOutTwo = woeTwo;
            wgTwo = new SignalGenerator();
            driverOutTwo.Init(wgTwo);
            wgTwo.Type = SignalGeneratorType.Sweep;
            wgTwo.Frequency = Convert.ToDouble(txtminhz.Text);
            wgTwo.FrequencyEnd = Convert.ToDouble(txtmaxhz.Text);
            wgTwo.SweepLengthSecs = Convert.ToDouble(txtsweeptime.Text);
            driverOutTwo.Play();
        }

        private void btnsweepStop_Click(object sender, EventArgs e)
        {
            if (driverOut != null)
            {
                driverOut.Dispose();
                driverOut = null;
            }
            if (driverOutTwo != null)
            {
                driverOutTwo.Dispose();
                driverOutTwo = null;
            }
        }
    }
}
