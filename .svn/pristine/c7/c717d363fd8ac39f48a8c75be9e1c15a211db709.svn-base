using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeTester.Observer
{
    /// <summary>
    /// 表示一个通知者
    /// </summary>
    public class Subject : ISubject
    {
        /// <summary>
        /// 发送通知时观察者需要进行的事件处理委托
        /// </summary>
        public EventHandler<NotifyEventArgs> NotifyEventHandler;

        /// <summary>
        /// 添加一个观察者
        /// </summary>
        /// <param name="handler">观察者中的事件处理</param>
        public void AddObserver(EventHandler<NotifyEventArgs> handler)
        {
            NotifyEventHandler += handler;
        }

        /// <summary>
        /// 移除一个观察者
        /// </summary>
        /// <param name="handler">观察者中的事件处理</param>
        public void RemoveObserver(EventHandler<NotifyEventArgs> handler)
        {
            NotifyEventHandler -= handler;
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="e">发送通知时事件参数</param>
        public void Notify(NotifyEventArgs e)
        {
            if (NotifyEventHandler != null)
            {
                NotifyEventHandler(this, e);
            }
        }
    }
}
