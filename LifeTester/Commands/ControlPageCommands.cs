using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LifeTester.Commands
{
    /// <summary>
    /// 提供测试控制使用的命令
    /// </summary>
    public static class ControlPageCommands
    {
        /// <summary>
        /// 板卡启动/停止点击事件触发的命令
        /// </summary>
        public static RoutedUICommand CardStartStopCommand = new RoutedUICommand("板卡启动停止点击事件", "CardStartStopCommand", typeof(ControlPageCommands));

        /// <summary>
        /// 通道暂停/继续点击事件触发的命令
        /// </summary>
        public static RoutedUICommand ChannelPausePlayCommand = new RoutedUICommand("通道暂停继续点击事件", "ChannelPausePlayCommand", typeof(ControlPageCommands));

        /// <summary>
        /// 当前板卡发生变化时触发的命令
        /// </summary>
        public static RoutedUICommand SelectionChangedCommand = new RoutedUICommand("当前板卡发生变化时触发事件", "SelectionChangedCommand", typeof(ControlPageCommands));
        /// <summary>
        /// 当前板卡发生变化时触发的命令
        /// </summary>
        public static RoutedUICommand SelectionTDChangedCommand = new RoutedUICommand("当前通道发生变化时触发事件", "SelectionTDChangedCommand", typeof(ControlPageCommands));
    }
}
