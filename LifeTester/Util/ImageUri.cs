﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LifeTester.Util
{
    /// <summary>
    /// 提供图片uri
    /// </summary>
    public static class ImageUri
    {
        /// <summary>
        /// 首页状态菜单图片源
        /// </summary>
        public static readonly ImageSource HOME_MENUITEM_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/home.png", UriKind.Relative));
        /// <summary>
        /// 信号设置菜单图片源
        /// </summary>
        public static readonly ImageSource SIGNAL_MENUITEM_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/signal.png", UriKind.Relative));
        /// <summary>
        /// 判决设置菜单图片源
        /// </summary>
        public static readonly ImageSource SETTING_MENUITEM_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/settings.png", UriKind.Relative));
        /// <summary>
        /// 测试控制菜单图片源
        /// </summary>
        public static readonly ImageSource CONTROL_MENUITEM_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/control.png", UriKind.Relative));
        /// <summary>
        /// 结果查看菜单图片源
        /// </summary>
        public static readonly ImageSource RESULT_MENUITEM_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/chart.png", UriKind.Relative));
        /// <summary>
        /// 启动按钮图片源
        /// </summary>
        public static readonly ImageSource START_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/start.png", UriKind.Relative));
        /// <summary>
        /// 停止按钮图片源
        /// </summary>
        public static readonly ImageSource STOP_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/stop.png", UriKind.Relative));
        /// <summary>
        /// 暂停按钮图片源
        /// </summary>
        public static readonly ImageSource PAUSE_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/pause.png", UriKind.Relative));
        /// <summary>
        /// 继续播放按钮图片源
        /// </summary>
        public static readonly ImageSource PLAY_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/play.png", UriKind.Relative));
        ///// <summary>
        ///// 选中按钮图片源
        ///// </summary>
        //public static readonly ImageSource Cheak_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/check.png", UriKind.Relative));
        ///// <summary>
        ///// 未选中
        ///// </summary>
        //public static readonly ImageSource NOTCheak_IMAGE_SOURCE = new BitmapImage(new Uri("/LifeTester;component/Images/stop.png", UriKind.Relative));
    }
}
