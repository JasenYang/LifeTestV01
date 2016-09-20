using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using LifeTester.Util;
using System.Windows;
using LifeTester.Observer;

namespace LifeTester.ViewModel
{
    /// <summary>
    /// 界面处理逻辑的最终基类
    /// </summary>
    public abstract class BaseVM : IObserver, INotifyPropertyChanged
    {
        /// <summary>
        /// 当属性改变时触发的事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性改变时通知UI刷新
        /// </summary>
        /// <param name="propertyName">改变的属性名称</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值标识界面处理逻辑类是否已初始化
        /// </summary>
        public bool Inited
        {
            get;
            protected set;
        }
        /// <summary>
        /// 初始化信息
        /// </summary>
        public virtual void Init()
        {
            Inited = true;
        }
        /// <summary>
        /// 添加命令绑定
        /// </summary>
        /// <param name="ele">要添加命令绑定的对象</param>
        public virtual void AddCommands(UIElement ele) { }

        /// <summary>
        /// 获取或设置通知者对象
        /// </summary>
        public ISubject Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 接受到通知者消息时的处理
        /// </summary>
        /// <param name="sender">通知者对象</param>
        /// <param name="e">通知时发送过来的事件对象</param>
        public virtual void Receive(object sender, NotifyEventArgs e)
        {
            
        }
    }

    /// <summary>
    /// 提供对属性通知基类的扩展
    /// </summary>
    public static class BaseVMEx
    {
        /// <summary>
        /// 当属性变化时通知UI更新
        /// </summary>
        /// <typeparam name="T">属性发生变化的对象</typeparam>
        /// <typeparam name="TProperty">对象发生变化的属性</typeparam>
        /// <param name="npModel">发生变化的属性所属对象</param>
        /// <param name="expression">表达式树</param>
        public static void OnPropertyChanged<T, TProperty>(this T npModel, Expression<Func<T, TProperty>> expression)
            where T : BaseVM
        {
            string propertyName = Common.GetPropertyName(expression);
            npModel.OnPropertyChanged(propertyName);
        }
    }
}
