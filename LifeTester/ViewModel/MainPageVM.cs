using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using LifeTester.Model;

namespace LifeTester.ViewModel
{
    /// <summary>
    /// 提供对主页面的处理逻辑
    /// </summary>
    public class MainPageVM : BaseVM
    {
        private ObservableCollection<Channel> channels;
        /// <summary>
        /// 获取或设置所有通道集合
        /// </summary>
        public ObservableCollection<Channel> Channels
        {
            get
            {
                return channels;
            }
            set
            {
                if (channels != value)
                {
                    channels = value;
                    this.OnPropertyChanged(p => p.Channels);
                }
            }
        }

        public override void Init()
        {
            if (Inited)
            {
                return;
            }
            base.Init();

            channels = new ObservableCollection<Channel>();
            var cards = Cache.Instance.Cards;
            cards.ForEach(c =>
                {
                    c.Channels.ForEach(h => channels.Add(h));
                });
        }
    }
}
