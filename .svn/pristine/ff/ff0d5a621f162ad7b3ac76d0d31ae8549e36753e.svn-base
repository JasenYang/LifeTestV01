using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LifeTester.Util;
using LifeTester.ViewModel;
using System.Threading;

namespace LifeTester.Model
{
    public class AutoTimer
    {
        private Mutex mu = new Mutex();
        private static object obj = new object();
        private Channel channel;

        private System.Timers.Timer timer;

        public AutoTimer(Channel channel)
        {

            this.channel = channel;
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            CalcuActualTime();

        }

        /// <summary>
        /// 计算已测试时间，间隔1s.
        /// </summary>
        /// <param name="state"></param>
        private void CalcuActualTime()
        {
           // PlayerModel._mu.WaitOne();
            try
            {
                channel.ActualDuration++;
                if (channel.ActualDuration >= channel.Duration && channel.State == States.NORMAL)// 
                {
                    //timer.Stop();
                    //ldb 此处增加板卡停止功能
                    //Cache.Instance.Cards.ForEach(p =>
                    //{
                    //    if (p.Number == channel.CardNumber)
                    //    {
                    //        p.Stop();
                    //    }
                    //});
                    Cache.Instance.SetCurrent(channel.CardNumber);
                    //Cache.Instance.CurrentContext.Stop();
                    //by ldb 改成超时后停止单个通道，当所有通道都停止了再将板卡停止
                    channel.State = States.COMPLETE;
                    Cache.Instance.CurrentContext.Pause(channel.Number);
                    
                    List<Channel> list = Cache.Instance.CurrentContext.CurrentCard.Channels.Where(p => p.State == States.NORMAL || p.State == States.STOP).ToList();
                    if (list == null || list.Count == 0)
                    {
                        Cache.Instance.CurrentContext.CurrentCard.StartStopImageSource = ImageUri.START_IMAGE_SOURCE;
                        Cache.Instance.CurrentContext.CurrentCard.ImageTooltip = "启动";
                        if (Cache.Instance.CurrentContext.CurrentCard.Number == "1")
                        {
                            ControlPageVM.MyMedia.myMedia1Play1(false, ControlPageVM.Instance.SignalName, 0);//关闭播放器1
                        }
                        if (Cache.Instance.CurrentContext.CurrentCard.Number == "2")
                        {

                            ControlPageVM.MyMedia.myMedia1Play2(false, ControlPageVM.Instance.SignalName2, 0);//关闭播放器1
                        }
                        Cache.Instance.CurrentContext.CurrentCard.StartStopImageSource = ImageUri.START_IMAGE_SOURCE;
                    }
                }

            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("CalcuActualTime():" + ex.Message);
            }
            finally
            {
               // PlayerModel._mu.ReleaseMutex();
            }

        }

        /// <summary>
        /// 开始计时
        /// </summary>
        public void StartTime()
        {
            timer.Start();
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        public void StopTime()
        {
            channel.ActualDuration = 0;
            timer.Stop();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            timer.Stop();
        }

        /// <summary>
        /// 继续
        /// </summary>
        public void Continue()
        {
            timer.Start();
        }

        /// <summary>
        /// 是否已启动计时器
        /// </summary>
        public bool Enabled
        {
            get
            {
                return timer.Enabled;
            }
        }
    }
}
