using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LifeTester.Model
{
    public class ReadThread
    {
        private ISerial serial;

        private Thread readThread;

        //是否终止读数据的线程
        private bool reading;

        public ReadThread(ISerial serial)
        {
            this.serial = serial;
        }



        /// <summary>
        /// 开始从硬件查询数据
        /// </summary>
        public void Start()
        {
            try
            {
                if (!reading)
                {
                    readThread = new Thread(ReadData);
                    serial.Open();

                    readThread.Start();//启动一个线程监控串口传来的数据
                    reading = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                LogHelper.WriteInfoLog("开始从硬件查询数据:" + ex.Message);
                MessageBoxWindow.ShowDialog("操作失败");
            }
        }

        /// <summary>
        /// 停止从硬件查询数据
        /// </summary>
        public void Stop()
        {
            if (reading)
            {
                reading = false;
                readThread.Abort();//关闭该线程
                readThread = null;
            }
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        private void ReadData()
        {
            Card currentCard = null;
            try
            {
                while (reading)
                {
                    if (serial.IsOpen)
                    {
                        // Model.PlayerModel._mu.WaitOne();
                        //  string readStr = serial.ReceivedData;
                        string readStr = serial.ReceivedData;
                        // LogHelper.WriteInfoLog("接受的数据：" + readStr);
                        if (!string.IsNullOrEmpty(readStr) && readStr.Length == 88)
                        {
                            LogHelper.WriteInfoLog("DSP回传数据给PC的数据：" + DateTime.Now.ToString() + "==||==" + readStr);
                            string cardNumber = readStr.Substring(2, 2)[1].ToString();//得到板卡号// Substring(2,2)//第一个2是从第三个数开始，第二个2表示长度为2
                            //string channelNumber = readStr.Substring(3, 1)[1].ToString();//得到通道号
                            for (int i = 1; i < 11; i++)
                            {
                                string channelNumber = i.ToString();
                                currentCard = Cache.Instance.Cards.Find(c => c.Number.Equals(cardNumber));//从开辟的内存中找到对应的板卡
                                System.Diagnostics.Debug.Assert(currentCard != null);

                                var channel = currentCard.Channels.Find(h => h.Number == channelNumber && h.State == States.NORMAL);//找到对应的通道号
                                if (channel != null)
                                {
                                    //Voltage = CommonUtility.HexToDouble(read.Substring(8, 4));

                                    channel.ParseString(readStr.Substring(((i - 1) * 8 + 6), 8));//调用Channel.cs中的函数，将采集到的结果解析完毕并写入对应的内存中。
                                    ChannelHistory history = new ChannelHistory();
                                    LogHelper.WriteInfoLog(i + "channel-----Electricity:" + channel.Electricity + "|ActualDuration:" + channel.ActualDuration + "|CardNumber:" + channel.CardNumber + "|Duration:" + channel.Duration + "|Number:" + channel.Number + "|Ohm:" + channel.Ohm + "|QTime:" + channel.QTime + "|State:" + channel.State + "|Voltage:" + channel.Voltage);
                                    history.CopyFrom(channel);
                                    LogHelper.WriteInfoLog(i + "history-----Electricity:" + history.Electricity + "|ActualDuration:" + history.ActualDuration + "|CardNumber:" + history.CardNumber + "|Duration:" + history.Duration + "|Number:" + history.Number + "|Ohm:" + history.Ohm + "|QTime:" + history.QTime + "|State:" + history.State + "|Voltage:" + history.Voltage);
                                    //if (channel.CardNumber=="1")
                                    //{
                                    //    System.Diagnostics.Debug.WriteLine(channel);
                                    //}

                                    Cache.Instance.HistorySates.Add(history);

                                }
                            }
                            serial.ReceivedData = string.Empty;
                        }
                        // Model.PlayerModel._mu.ReleaseMutex();
                        Thread.Sleep(500);
                    }
                }
            }
            catch (TimeoutException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                SetCardException(currentCard);
                LogHelper.WriteInfoLog("处理数据" + ex.Message);
                reading = false;
            }
        }

        private void SetCardException(Card card)
        {
            if (card != null)
            {
                card.Channels.ForEach(c =>
                {
                    c.State = States.EXCEPTION;
                });
                var context = Cache.Instance.Contexts.Find(p => p.CurrentCard == card);
                if (context != null)
                {
                    context.CurrentSerialPort.Close();
                }
            }
        }
    }
}
