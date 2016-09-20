using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LifeTester.UC;

namespace LifeTester
{
    /// <summary>
    /// 表示一个提示弹出框窗口
    /// </summary>
    public partial class MessageBoxWindow : BaseWindow
    {
        /// <summary>
        /// 获取或设置提示信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 获取或设置窗口标题
        /// </summary>
        public string Title { get; set; }
        public MessageBoxWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使用指定的提示信息初始化弹出框窗口
        /// </summary>
        /// <param name="message">提示信息</param>
        public MessageBoxWindow(string message)
            : this()
        {
            this.Message = message;
            this.DataContext = this;
        }

        /// <summary>
        /// 使用指定的标题与提示信息初始化弹出框窗口
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">提示信息</param>
        public MessageBoxWindow(string title, string message)
            : this(message)
        {
            this.Title = title;
        }

        /// <summary>
        /// 弹出对话框，显示提示信息
        /// </summary>
        /// <param name="message">要显示的提示信息</param>
        public static void ShowDialog(string message)
        {
            var window = new MessageBoxWindow(message);
            window.ShowDialog();
        }

        /// <summary>
        /// 弹出对话框，显示提示信息
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="message">要显示的提示信息</param>
        public static void ShowDialog(string title, string message)
        {
            var window = new MessageBoxWindow(title, message);
            window.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
