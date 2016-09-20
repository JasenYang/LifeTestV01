using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace LifeTester.Model
{
    /// <summary>
    /// 表示一个菜单项
    /// </summary>
    public class MenuItem : NotifyPropertyChanged
    {
        private string text;
        /// <summary>
        /// 获取或设置菜单文本
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value;
                    this.OnPropertyChanged(p => p.Text);
                }
            }
        }

        private ImageSource imageSource;
        /// <summary>
        /// 获取或设置菜单图片
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    this.OnPropertyChanged(p => p.ImageSource);//泛型
                }
            }
        }

        private bool? isSelected;
        /// <summary>
        /// 获取或设置一个值，该值标识菜单项是否选中
        /// </summary>
        public bool? IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    this.OnPropertyChanged(p => p.IsSelected);
                }
            }
        }

        private string itemType;
        /// <summary>
        /// 获取或设置菜单项类型
        /// </summary>
        public string ItemType
        {
            get
            {
                return itemType;
            }
            set
            {
                if (itemType != value)
                {
                    itemType = value;
                    this.OnPropertyChanged(p => p.ItemType);
                }
            }
        }
    }
}
