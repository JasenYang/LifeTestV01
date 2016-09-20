﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using LifeTester.Model;
using System.Windows.Media;

namespace LifeTester.Converter
{
    /// <summary>
    /// 提供将状态转换成颜色的转换器
    /// </summary>
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || (value is States) == false)
            {
                return Brushes.Red;
            }
            var status = (States)value;
            switch (status)
            {
                case States.NORMAL:
                    return "#FF3ED13E";
                case States.STOP:
                    return "#FFA7A7A7";
                //case States.NOTCHECK:
                    return "#FFA7A7A7";
                case States.EXCEPTION:
                    return "#FFFF3939";
                case States.COMPLETE:
                    return "#FF69AEFF";
                //case States.CHECK:
                //    return "#FF69AEFF";
                default:
                    return "#FFFF3939";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
