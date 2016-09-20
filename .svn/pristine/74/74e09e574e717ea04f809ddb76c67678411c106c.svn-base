using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using LifeTester.Util;

namespace LifeTester.Model
{
    /// <summary>
    /// 实现了INotifyPropertyChanged接口的类
    /// </summary>
    public abstract class NotifyPropertyChanged : BaseEntity, INotifyPropertyChanged
    {
        /// <summary>
        /// 属性改变时执行事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 当属性变化时通知UI更新
        /// </summary>
        /// <param name="name">变化的属性名称</param>
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    /// <summary>
    /// 提供对属性通知基类的扩展
    /// </summary>
    public static class NotifyPropertyChangedEx
    {
        /// <summary>
        /// 当属性变化时通知UI更新
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">对象发生变化的属性</typeparam>
        /// <param name="npModel">发生变化的属性所属对象</param>
        /// <param name="expression">表达式树</param>
        public static void OnPropertyChanged<T, TProperty>(this T npModel, Expression<Func<T, TProperty>> expression)
            where T : NotifyPropertyChanged
        {
            string propertyName = Common.GetPropertyName(expression);
            npModel.OnPropertyChanged(propertyName);
        }
    }
}
