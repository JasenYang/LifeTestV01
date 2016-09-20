﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using WMPLib;
using System.Runtime.InteropServices;//用来引入dll 文件！
using System.Diagnostics;// 用来引入process类
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
using System.Configuration;
using NAudio.Wave.WaveStreams;
using NAudio.Wave.SampleProviders;

namespace LifeTester.Model
{


    public class MediaPlayer
    {



        //private Process p;
        //private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        //private const int APPCOMMAND_VOLUME_UP = 0x0a0000;
        //private const int APPCOMMAND_VOLUME_DOWN = 0x090000;
        //private const int WM_APPCOMMAND = 0x319;

        //[DllImport("user32.dll")]
        ////[DllImport("winmm.dll")]// 该句只能放在方法的上面！不能放在文件或者类的开头的位置。
        //public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr IParam);

        //public void SetVol()
        //{
        //    p = Process.GetCurrentProcess();
        //    for (int i = 0; i < 2000; i++)
        //    {
        //        SendMessageW(p.Handle,WM_APPCOMMAND,p.Handle,(IntPtr)APPCOMMAND_VOLUME_UP);
        //    }
        //    SendMessageW(p.Handle, WM_APPCOMMAND, p.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
        //}

        // private Process p;

        // const uint WM_APPCOMMAND = 0x319;
        // const uint APPCOMMAND_VOLUME_UP = 0x0a;
        // const uint APPCOMMAND_VOLUME_DOWN = 0x09;
        // const uint APPCOMMAND_VOLUME_MUTE = 0x08;

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        // static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
        // public void VolumeUp(){
        //     SendMessage(this.p.Handle, WM_APPCOMMAND, 0x30292, APPCOMMAND_VOLUME_UP * 0x10000);
        // }

        // public void SetVol()
        // {
        //     p = Process.GetCurrentProcess();
        //     for (int i = 0; i < 500; i++)
        //     {
        //         VolumeUp();
        //     }
        // }




        public WMPLib.WindowsMediaPlayer myMedia2 = new WindowsMediaPlayer();
        public WMPLib.WindowsMediaPlayer myMedia1 = new WindowsMediaPlayer();

        public void myMedia1Play1(bool start, string location, int volume)
        {
            try
            {
                if (start.Equals(true))
                {
                    float _vol = float.Parse(volume.ToString());
                    if (PlayerModel.IsCheckSweepOne)
                    {
                        PlayerModel._dsOne = new DirectSoundOut(PlayerModel.DirectSoundOutDeviceOne);
                        PlayerModel.wgOne = new SignalGenerator();
                        PlayerModel._dsOne.Init(PlayerModel.wgOne);
                        PlayerModel.wgOne.Type = SignalGeneratorType.Sweep;
                        PlayerModel.wgOne.Frequency = PlayerModel.StartfrequencyOne;
                        PlayerModel.wgOne.FrequencyEnd = PlayerModel.EndfrequencyOne;
                        PlayerModel.wgOne.SweepLengthSecs = PlayerModel.SweepLengthOne;
                        PlayerModel.wgOne.Gain = (float)(_vol / 1000);
                        PlayerModel._dsOne.Play();
                    }
                    else
                    {
                        PlayerModel.afrOne = new AudioFileReader(location);
                        PlayerModel.loopOne = new LoopStream(PlayerModel.afrOne);
                        SampleChannel sclOne = new SampleChannel(PlayerModel.loopOne, true);
                        sclOne.Volume = _vol / 1000;
                        PlayerModel._dsOne = new DirectSoundOut(PlayerModel.DirectSoundOutDeviceOne);
                        PlayerModel._dsOne.Init(new MeteringSampleProvider(sclOne));
                        PlayerModel._dsOne.Play();
                    }

                    //myMedia1.controls.currentPosition = 1;
                    //myMedia1.settings.setMode("loop", true);

                    //myMedia1.URL = location;
                    //myMedia1.settings.volume = volume;
                    //myMedia1.controls.play();

                }
                if (start.Equals(false))
                {
                    //myMedia1.controls.stop();
                    PlayerModel._dsOne.Stop();

                }
            }
            catch (Exception ex)
            {
                //myMedia1.controls.stop();
                if (PlayerModel.IsCheckSweepOne)
                {
                    if (PlayerModel.afrOne != null)
                    {
                        PlayerModel.afrOne.Dispose();
                    }
                    if (PlayerModel.loopOne != null)
                    {
                        PlayerModel.loopOne.Dispose();
                    }
                }
                PlayerModel._dsOne.Stop();

                LogHelper.WriteInfoLog("播放器一启动/停止失败：" + ex.Message);
            }

        }

        public void myMedia1Play2(bool start, string location, int volume)
        {
            try
            {
                if (start.Equals(true))
                {
                    float _vol = float.Parse(volume.ToString());
                    if (PlayerModel.IsCheckSweepTwo)
                    {


                        PlayerModel._dsTwo = new DirectSoundOut(PlayerModel.DirectSoundOutDeviceTwo);

                        PlayerModel.wgTwo = new SignalGenerator();
                        PlayerModel._dsTwo.Init(PlayerModel.wgTwo);
                        PlayerModel.wgTwo.Type = SignalGeneratorType.Sweep;
                        PlayerModel.wgTwo.Frequency = PlayerModel.StartfrequencyTwo;
                        PlayerModel.wgTwo.FrequencyEnd = PlayerModel.EndfrequencyTwo;
                        PlayerModel.wgTwo.SweepLengthSecs = PlayerModel.SweepLengthTwo;
                        PlayerModel.wgTwo.Gain = _vol / 1000;
                        PlayerModel._dsTwo.Play();




                    }
                    else
                    {
                        //myMedia2.settings.setMode("loop", true);
                        //myMedia2.URL = location;
                        //myMedia2.settings.volume = volume;
                        //myMedia2.controls.play();
                        PlayerModel.afrTwo = new AudioFileReader(location);
                        PlayerModel.loopTwo = new LoopStream(PlayerModel.afrTwo);
                        SampleChannel sclTwo = new SampleChannel(PlayerModel.loopTwo, true);
                        sclTwo.Volume = _vol / 1000;
                        PlayerModel._dsTwo = new DirectSoundOut(PlayerModel.DirectSoundOutDeviceTwo);
                        PlayerModel._dsTwo.Init(new MeteringSampleProvider(sclTwo));
                        PlayerModel._dsTwo.Play();
                    }
                }
                if (start.Equals(false))
                {
                    if (PlayerModel.IsCheckSweepTwo)
                    {
                        if (PlayerModel.afrTwo != null)
                        {
                            PlayerModel.afrTwo.Dispose();
                        }
                        if (PlayerModel.loopTwo != null)
                        {
                            PlayerModel.loopTwo.Dispose();
                        }
                    }
                    PlayerModel._dsTwo.Stop();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("播放器二启动/停止失败：" + ex.Message);
            }
        }



    }
}
