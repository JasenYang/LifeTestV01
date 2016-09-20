using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeTester.Observer
{
    /// <summary>
    /// 观察者接口
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 获取或设置通知者对象
        /// </summary>
        ISubject Subject { get; set; }

        /// <summary>
        /// 获取或设置接受到通知时的处理
        /// </summary>
        void Receive(object sender, NotifyEventArgs e);
    }
}
