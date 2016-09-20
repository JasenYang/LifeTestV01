using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using LifeTester.Model;
using System.Windows;

namespace LifeTester.Converter
{
    /// <summary>
    /// 提供导出按钮可见性的转换器
    /// </summary>
    public class ExportButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || (value is Channel) == false)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
