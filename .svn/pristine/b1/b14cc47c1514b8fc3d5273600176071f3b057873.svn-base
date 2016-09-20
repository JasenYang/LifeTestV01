using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace LifeTester.Converter
{
    /// <summary>
    /// 提供将测试数据（电压、电流、电阻）加上单位的转换器
    /// </summary>
    public class UnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null || string.IsNullOrEmpty(parameter.ToString()))
            {
                return value;
            }

            switch (parameter.ToString())
            {
                case "U":
                    // 电压
                    return value + "V";
                case "I":
                    // 电流
                    return value + "A";
                case "R":
                    // 电阻
                    return value + "Ω";
                case "H":
                    // 小时
                    int seconds;
                    int.TryParse(value.ToString(), out seconds);
                    return string.Format("{0}:{1}", (seconds / 3600).ToString().PadLeft(2, '0'),
                        ((seconds % 3600) / 60).ToString().PadLeft(2, '0'));
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
