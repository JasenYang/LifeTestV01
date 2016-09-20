using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;
using LifeTester.Util;

namespace LifeTester.Converter
{
    /// <summary>
    /// 提供切换图片按钮可用性覆盖层可见性的转换器
    /// </summary>
    public class OverlayVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || (value is ImageSource) == false)
            {
                return Visibility.Visible;
            }
            var imageSource = (ImageSource)value;
            if (imageSource == ImageUri.STOP_IMAGE_SOURCE)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
