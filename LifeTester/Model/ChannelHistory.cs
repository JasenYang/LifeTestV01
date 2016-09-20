using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeTester.Model
{
    //by ldb 不再继承 channel，发现没有其他地方调用channlehistory，用于序列化
     //public class ChannelHistory : Channel  
    /// <summary>
    /// 历史数据
    /// </summary>
    [Serializable]
    public class ChannelHistory 
    {

        public void CopyFrom(Channel channel)
        {
            //by ldb “信号设置”下的“电压设置”“峰峰值”的算法更改，实绩的峰峰值等于有效值*2倍的根号二。
            if (channel.VoltageType == VoltageType.Peak)
            {
                this.Voltage = channel.Voltage * 2 * Math.Sqrt(2);
            }
            else
            {
                this.Voltage = channel.Voltage;
            }
            
            this.VoltageType = channel.VoltageType;
            this.Ohm = channel.Ohm;
            this.Electricity = channel.Electricity;
            this.CardNumber = channel.CardNumber;
            this.Number = channel.Number;
            this.Duration = channel.Duration;
            this.ActualDuration = channel.ActualDuration;
            this.State = channel.State;
            this.QTime = DateTime.Now;//by ldb 增加时间戳，用于根据时间段清理数据缓存，防止缓存过大
        }
        public ChannelHistory()
        {

        }
        //by ldb 定义属性
        public double Voltage { get; set; }

        public VoltageType VoltageType { get; set; }

        public double Electricity { get; set; }

        public string CardNumber { get; set; }

        public double Duration { get; set; }

        public double ActualDuration { get; set; }

        public States State { get; set; }

        public string Number { get; set; }

        public double Ohm { get; set; }

        public DateTime QTime { get; set; }
    }
}
