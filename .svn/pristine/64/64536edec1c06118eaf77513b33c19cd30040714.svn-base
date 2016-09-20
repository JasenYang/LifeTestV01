using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace LifeTester.Converter
{
    /// <summary>
    /// 提供将秒数转换为HH:mm:ss的转换器
    /// </summary>
    public class SecondsValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int seconds = System.Convert.ToInt32(value);
                string result = string.Empty;
                int hour = seconds / 3600;
                int minute = (seconds - (hour * 3600)) / 60;
                seconds = (seconds - (hour * 3600)) % 60;
                result = string.Format("{0}:{1}:{2}", hour.ToString().PadLeft(2, '0'),
                    minute.ToString().PadLeft(2, '0'), seconds.ToString().PadLeft(2, '0'));
                return result;
            }
            catch (Exception)
            {
                return "00:00:" + value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
