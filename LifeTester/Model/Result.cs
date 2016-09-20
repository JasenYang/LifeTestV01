using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeTester.Model
{
    /// <summary>
    /// 表示一条测试结果
    /// </summary>
    public class Result : NotifyPropertyChanged,ICloneable
    {
        private double _value;
        /// <summary>
        /// 获取或设置值
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    this.OnPropertyChanged(p => p.Value);
                }
            }
        }

        private int seconds;
        /// <summary>
        /// 获取或设置当前运行秒数
        /// </summary>
        public int Seconds
        {
            get { return seconds; }
            set
            {
                if (seconds != value)
                {
                    seconds = value;
                    this.OnPropertyChanged(p => p.Seconds);
                }
            }
        }

        /// <summary>
        /// 获取运行秒数实际显示的格式
        /// </summary>
        public string SecondsShow
        {
            get
            {
                string result = string.Empty;
                int hour = Seconds / 3600;
                int minute = (Seconds - (hour * 3600)) / 60;
                int seconds = (Seconds - (hour * 3600)) % 60;
                result = string.Format("{0}:{1}:{2}", hour.ToString().PadLeft(2, '0'),
                    minute.ToString().PadLeft(2, '0'), seconds.ToString().PadLeft(2, '0'));
                return result;
            }
        }

        public object Clone()
        {
            Result tmp = new Result();
            tmp.Value = this.Value;
            tmp.Seconds = this.Seconds;
            return tmp;
        }
    }
}
