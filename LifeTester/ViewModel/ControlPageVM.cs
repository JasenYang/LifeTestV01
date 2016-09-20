﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using LifeTester.Model;
using System.Windows;
using System.Windows.Input;
using LifeTester.Commands;
using System.Windows.Controls;
using LifeTester.Util;
using WMPLib;
using System.Runtime.InteropServices;//用来引入dll 文件！
using System.Diagnostics;// 用来引入process类
using System.Windows.Forms;
using System.IO;


namespace LifeTester.ViewModel
{


    /// <summary>
    /// 提供测试控制页面的处理逻辑
    /// </summary>
    public class ControlPageVM : BaseVM
    {
        //public System.Media.SoundPlayer myMedia1 = new System.Media.SoundPlayer();

        static MediaPlayer myMedia = new MediaPlayer();
        static ControlPageVM instance = null;//by ldb定义一个对自身的引用,AutoTimer停止时使用

        public static ControlPageVM Instance
        {
            get { return ControlPageVM.instance; }
            set { ControlPageVM.instance = value; }
        }

        public static MediaPlayer MyMedia
        {
            get { return myMedia; }
            set { myMedia = value; }
        }

        private string signalName;
        private string signalName2;
        private string signalNameAll;
        private int volume1;
        private int volume2;
        private double voltume1Temp;
        private double voltume2Temp;
        private int TestMin;
        /// <summary>
        /// 获取或设置信号名称
        /// </summary>
        public string SignalName//状态栏显示
        {
            get
            {
                return signalName;
            }
            set
            {
                if (signalName != value)
                {
                    signalName = value;
                    this.OnPropertyChanged(p => p.SignalName);//更新状态栏显示信息
                }
            }
        }



        public string SignalName2
        {
            get
            {
                return signalName2;
            }
            set
            {
                if (signalName2 != value)
                {
                    signalName2 = value;
                    this.OnPropertyChanged(p => p.SignalName2);
                }
            }
        }

        public string SignalNameAll//状态栏显示
        {
            get
            {
                return signalNameAll = signalName + signalName2;
            }
            set
            {
                if (signalNameAll != value)
                {
                    signalNameAll = value;
                    this.OnPropertyChanged(p => p.SignalNameAll);//更新状态栏显示信息
                }
            }
        }

        private ObservableCollection<Card> cards;
        /// <summary>
        /// 获取或设置板卡集合
        /// </summary>
        public ObservableCollection<Card> Cards
        {
            get
            {
                return cards;
            }
            set
            {
                if (cards != value)
                {
                    cards = value;
                    this.OnPropertyChanged(p => p.Cards);
                }
            }
        }

        /// <summary>
        /// 添加命令绑定
        /// </summary>
        /// <param name="ele">测试控制窗口对象</param>
        public override void AddCommands(UIElement ele)
        {
            ele.CommandBindings.Add(new CommandBinding(ControlPageCommands.CardStartStopCommand, OnCardStartStop));
            ele.CommandBindings.Add(new CommandBinding(ControlPageCommands.ChannelPausePlayCommand, OnChannelPausePlay));
            ele.CommandBindings.Add(new CommandBinding(ControlPageCommands.SelectionChangedCommand, OnSelectionChanged));
            // ele.CommandBindings.Add(new CommandBinding(ControlPageCommands.SelectionTDChangedCommand, OnSelectionTDChanged));
        }

        /// <summary>
        /// 当前板卡发生变化时触发事件
        /// </summary>
        /// <param name="sender">命令绑定的对象</param>
        /// <param name="e">命令相关参数</param>
        public void OnSelectionChanged(object sender, ExecutedRoutedEventArgs e)
        {
            Channelss.Clear();
            var card = e.Parameter as Card;
            System.Diagnostics.Debug.Assert(card != null);
            Cache.Instance.SetCurrent(card.Number);

        }
        List<Channel> Channelss = new List<Channel>();
        ///// <summary>
        ///// 当前板卡发生变化时触发事件
        ///// </summary>
        ///// <param name="sender">命令绑定的对象</param>
        ///// <param name="e">命令相关参数</param>
        //public void OnSelectionTDChanged(object sender, ExecutedRoutedEventArgs e)
        //{

        //    //    var card = e.Parameter as Channel;
        //    //    if (card == null) return;
        //    //    if (card.State == States.COMPLETE)
        //    //    {
        //    //        card.PausePlayImageSource = ImageUri.PAUSE_IMAGE_SOURCE;
        //    //        Channelss.Remove(card);
        //    //    }
        //    //    else
        //    //    {
        //    //        card.PausePlayImageSource = ImageUri.PLAY_IMAGE_SOURCE;
        //    //        card.State = States.STOP;
        //    //        Channelss.Add(card);
        //    //    }
        //    //    //if (card.State == States.COMPLETE)
        //    //    //{
        //    //    //    card.State = States.STOP;
        //    //    //}
        //    //    //else
        //    //    //{
        //    //    //    card.State = States.COMPLETE;
        //    //    //}
        //    //    //System.Diagnostics.Debug.Assert(card != null);
        //    //    //Cache.Instance.SetCurrent(card.Number);
        //}



        [DllImport("winmm.dll", EntryPoint = "waveOutSetVolume", CharSet = CharSet.Auto)]
        public static extern int waveOutSetVolume(uint deviceID, uint Volume);

        /// <summary>
        /// 板卡启动/停止点击触发的事件
        /// </summary>
        /// <param name="sender">命令绑定的对象</param>
        /// <param name="e">命令相关参数</param>
        public void OnCardStartStop(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {


                //LogHelper.WriteInfoLog(string.Format("板卡{0}启动",card.Number));
                var card = e.Parameter as Card;
                Cache.Instance.SetCurrent(card.Number);
                LogHelper.WriteInfoLog(string.Format("板卡{0}启动", card.Number));
                //if (Channelss.Count == 0)
                //{


                //    Channelss.AddRange(card.Channels);
                //}
                //by ldb 重置每个通道的连续超限次数SKP为0，并将按钮突变变更该停止
                Cache.Instance.CurrentContext.Stop();
                if (card.StartStopImageSource == ImageUri.START_IMAGE_SOURCE)
                {
                    if (card.Number.Equals("1"))
                    {
                        string oldpath = Directory.GetCurrentDirectory() + @"\Data\1";
                        string newpath = Directory.GetCurrentDirectory() + @"\History\" + DateTime.Now.ToString("yyyyMMddHHmmss")+"\\1";
                        Common.CopyDir(oldpath, newpath);
                        Common.DeleteDir(oldpath);
                    }
                    if (card.Number.Equals("2"))
                    {
                        string oldpath = Directory.GetCurrentDirectory() + @"\Data\2";
                        string newpath = Directory.GetCurrentDirectory() + @"\History\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\\2";
                        Common.CopyDir(oldpath, newpath);
                        Common.DeleteDir(oldpath);
                    }

                    foreach (Channel item in card.Channels)
                    {

                        item.State = States.NORMAL;
                        item.PausePlayImageSource = ImageUri.PAUSE_IMAGE_SOURCE;
                    }

                    if (Cache.Instance.CurrentContext.Start())//启动所有通道的指令入口，如何成功则启动{}，否则“操作失败”
                    {

                        card.StartStopImageSource = ImageUri.STOP_IMAGE_SOURCE;//更换显示的图标
                        card.ImageTooltip = "停止";
                        startPlaye();

                    }
                    else
                    {
                        MessageBoxWindow.ShowDialog("操作失败");
                    }
                }
                else
                {
                    card.Channels.ForEach(p =>
                   {
                       p.OutSKP = 0;
                       if (p.PausePlayImageSource != ImageUri.PAUSE_IMAGE_SOURCE)
                       {
                           p.Duration = 0;
                           p.PausePlayImageSource = ImageUri.PAUSE_IMAGE_SOURCE;
                       }
                   });
                    if (Cache.Instance.CurrentContext.Stop())
                    {
                        card.StartStopImageSource = ImageUri.START_IMAGE_SOURCE;
                        card.ImageTooltip = "启动";
                        if (Cache.Instance.CurrentContext.CurrentCard.Number == "1")
                        {
                            myMedia.myMedia1Play1(false, signalName, 0);//关闭播放器1
                        }
                        if (Cache.Instance.CurrentContext.CurrentCard.Number == "2")
                        {
                            myMedia.myMedia1Play2(false, signalName2, 0);//关闭播放器1
                        }
                    }
                    else
                    {
                        MessageBoxWindow.ShowDialog("操作失败");
                    }
                }
                Channelss.Clear();
            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("板卡启动/停止点击触发出现异常：" + ex.Message);
            }
        }

        private void startPlaye()
        {

            if (Cache.Instance.CurrentContext.CurrentCard.Number == "1")
            {
                volume1 = (int)(voltume1Temp * 10);
                if (volume1 == 0)
                {
                    MessageBoxWindow.ShowDialog("请设置测试电压值");
                }
                else if (TestMin == 0)
                {
                    MessageBoxWindow.ShowDialog("请设置测试时间长度");
                }
                else if (string.IsNullOrWhiteSpace(signalName) && PlayerModel.IsCheckSweepOne == false)
                {
                    MessageBoxWindow.ShowDialog("请设置测试文件一");
                }
                else
                {

                    myMedia.myMedia1Play1(true, signalName, volume1);//启动播放器1

                    // //myMedia.SetVol();
                    // //myMedia.waveOutSetVolume();
                    //// waveOutSetVolume(0, 65000);
                    //     //myMedia.SetVol();
                    //     //myMedia.waveOutSetVolume();
                    //     waveOutSetVolume(0, 65000);

                    //card.StartStopImageSource = ImageUri.STOP_IMAGE_SOURCE;//更换显示的图标
                    //card.ImageTooltip = "停止";
                    LogHelper.WriteInfoLog(string.Format("启动播放器一,板卡号：{0},电压值：{1},时间长度：{2},测试文件：{3},是否是扫频：{4},开始频率：{5}，结束频率：{6}，扫描时长：{7}，选择声卡GUID{8}", Cache.Instance.CurrentContext.CurrentCard.Number, volume1, TestMin, signalName, PlayerModel.IsCheckSweepOne.ToString(), PlayerModel.StartfrequencyOne, PlayerModel.EndfrequencyOne, PlayerModel.SweepLengthOne, PlayerModel.DirectSoundOutDeviceOne.ToString()));
                }

            }
            if (Cache.Instance.CurrentContext.CurrentCard.Number == "2")
            {
                volume2 = (int)(voltume2Temp * 10);
                if (volume2 == 0)
                {
                    MessageBoxWindow.ShowDialog("请设置测试电压值");
                }
                else if (TestMin == 0)
                {
                    MessageBoxWindow.ShowDialog("请设置测试时间长度");
                }
                else if (string.IsNullOrWhiteSpace(signalName2) && PlayerModel.IsCheckSweepTwo == false)
                {
                    MessageBoxWindow.ShowDialog("请设置测试文件二");
                }
                else
                {
                    myMedia.myMedia1Play2(true, signalName2, volume2);//启动播放器1.
                    //card.StartStopImageSource = ImageUri.STOP_IMAGE_SOURCE;//更换显示的图标
                    //card.ImageTooltip = "停止";
                    LogHelper.WriteInfoLog(string.Format("启动播放器二,板卡号：{0},电压值：{1},时间长度：{2},测试文件：{3},是否是扫频：{4},开始频率：{5}，结束频率：{6}，扫描时长：{7}，选择声卡GUID{8}", Cache.Instance.CurrentContext.CurrentCard.Number, volume2, TestMin, signalName2, PlayerModel.IsCheckSweepTwo.ToString(), PlayerModel.StartfrequencyTwo, PlayerModel.EndfrequencyTwo, PlayerModel.SweepLengthTwo, PlayerModel.DirectSoundOutDeviceTwo.ToString()));
                }
            }
        }

        /// <summary>
        /// 通道暂停/继续点击触发的事件
        /// </summary>
        /// <param name="sender">命令绑定的对象</param>
        /// <param name="e">命令相关参数</param>
        public void OnChannelPausePlay(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                //var chan = e.Parameter  as Channel;
                //foreach (Card item in Cards)
                //{
                //    if (chan.CardNumber==item.)
                //    {

                //    }
                //}
                // ;
                Channel channel = e.Parameter as Channel;
                int channeltoCard = (Convert.ToInt32(channel.CardNumber) - 1);
                if (Cards[channeltoCard].StartStopImageSource == ImageUri.START_IMAGE_SOURCE)
                {
                    Cards[channeltoCard].StartStopImageSource = ImageUri.STOP_IMAGE_SOURCE;//更换显示的图标
                    Cards[0].ImageTooltip = "停止";
                    startPlaye();
                }

                if (channel.PausePlayImageSource == ImageUri.PAUSE_IMAGE_SOURCE && channel.State == States.STOP)
                {
                    if (Cache.Instance.CurrentContext.Start(channel.Number))
                    {
                        channel.PausePlayImageSource = ImageUri.PAUSE_IMAGE_SOURCE;
                        channel.ImageTooltip = "停止";
                        channel.Timer.StartTime();
                        channel.State = States.NORMAL;
                    }
                    else
                    {
                        MessageBoxWindow.ShowDialog("操作失败");
                    }
                }
                else if (channel.PausePlayImageSource == ImageUri.PAUSE_IMAGE_SOURCE && channel.State == States.NORMAL)
                {
                    if (Cache.Instance.CurrentContext.Pause(channel.Number))
                    {
                        channel.PausePlayImageSource = ImageUri.PLAY_IMAGE_SOURCE;
                        channel.ImageTooltip = "继续";
                    }
                    else
                    {
                        MessageBoxWindow.ShowDialog("操作失败");
                    }
                }
                else
                {
                    if (Cache.Instance.CurrentContext.Continue(channel.Number))
                    {
                        channel.PausePlayImageSource = ImageUri.PAUSE_IMAGE_SOURCE;
                        channel.ImageTooltip = "暂停";
                    }
                    else
                    {
                        MessageBoxWindow.ShowDialog("操作失败");
                    }
                }

            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("通道暂停/继续点击触发出现异常：" + ex.Message);
            }

        }

        public override void Init()
        {
            if (Inited)
            {
                return;
            }
            base.Init();

            //默认设置第一个测试板卡的信号源
            SignalName = Cache.Instance.Cards[0].SignalSource;
            Cards = new ObservableCollection<Card>();

            Cache.Instance.Cards.ForEach(c => Cards.Add(c));
        }

        /// <summary>
        /// 接受到通知时的处理
        /// </summary>
        /// <param name="sender">通知者对象</param>
        /// <param name="e">通知者发送通知时的事件参数</param>
        public override void Receive(object sender, Observer.NotifyEventArgs e)
        {
            switch (e.MessageTypes)
            {
                case LifeTester.Observer.MessageTypes.UPDATE_SIGNAL_SOURCE://更新播放文件
                    SignalName = e.ParamObject.ToString();
                    break;

                case LifeTester.Observer.MessageTypes.UPDATE_SIGNAL_SOURCE2:
                    SignalName2 = e.ParamObject.ToString();
                    break;

                case LifeTester.Observer.MessageTypes.UPDATE_SYSTEM_VOLTAGE://捕获测试电压参数
                    voltume1Temp = (double)e.ParamObject;//将 object double 转化成int 
                    break;

                case LifeTester.Observer.MessageTypes.UPDATE_SYSTEM_TIME: //捕获测试时间长度参数
                    TestMin = (int)e.ParamObject;
                    break;

                case LifeTester.Observer.MessageTypes.UPDATE_SYSTEM_VOLTAGEBOARD2:
                    voltume2Temp = (double)e.ParamObject;
                    break;

                default:
                    break;
            }
        }
    }
}
