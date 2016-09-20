using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using LifeTester.ViewModel;

namespace LifeTester.UC
{
    /// <summary>
    /// 表示一个窗口基类
    /// </summary>
    public abstract class BaseWindow : Window, IBaseUC
    {
        /// <summary>
        /// 获取或设置界面处理逻辑类对象
        /// </summary>
        public BaseVM VM
        {
            get;
            set;
        }

        public BaseWindow()
        {
            InitCommandBindings();
        }

        #region 窗口按钮命令绑定
        /// <summary>
        /// 初始化命令绑定
        /// </summary>
        public virtual void InitCommandBindings()
        {
            this.CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.CloseWindowCommand, OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.RestoreWindow(this);
        }

        #endregion 窗口按钮命令绑定
    }
}
