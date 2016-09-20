using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using LifeTester.Model;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows;
using LifeTester.Commands;
using LifeTester.Util;
using LifeTester.Observer;

namespace LifeTester.ViewModel
{
    /// <summary>
    /// 提供对主窗口的处理逻辑
    /// </summary>
    public class MainWindowVM : BaseVM
    {
        /// <summary>
        /// 表示首页状态页面
        /// </summary>
        public const string MAIN_PAGE = "MainPage";
        /// <summary>
        /// 表示信号设置页面
        /// </summary>
        public const string SIGNAL_SETTING_PAGE = "SignalSettingPage";
        /// <summary>
        /// 表示判决设置页面
        /// </summary>
        public const string RESULT_ADJUDGE_SETTING_PAGE = "ResultAdjudgeSettingPage";
        /// <summary>
        /// 表示测试控制页面
        /// </summary>
        public const string CONTROL_PAGE = "ControlPage";
        /// <summary>
        /// 表示结果查看页面
        /// </summary>
        public const string RESULT_PAGE = "ResultPage";

        /// <summary>
        /// 获取或设置主窗口中所有页面名称与处理逻辑对象字典
        /// </summary>
        public Dictionary<string, BaseVM> PageNameAndVMDic
        {
            get;
            set;
        }

        private ObservableCollection<MenuItem> menuItems;
        /// <summary>
        /// 获取或设置菜单列表
        /// </summary>
        public ObservableCollection<MenuItem> MenuItems
        {
            get
            {
                return menuItems;
            }
            set
            {
                if (menuItems != value)
                {
                    menuItems = value;
                    this.OnPropertyChanged(p => p.MenuItems);
                }
            }
        }

        private Visibility mainPageVisibility = Visibility.Visible;
        /// <summary>
        /// 获取或设置主页面的显示状态
        /// </summary>
        public Visibility MainPageVisibility
        {
            get { return mainPageVisibility; }
            set
            {
                if (mainPageVisibility != value)
                {
                    mainPageVisibility = value;
                    this.OnPropertyChanged(p => p.MainPageVisibility);//更新显示的结果

                }
            }
        }

        private Visibility signalSettingPageVisibility = Visibility.Collapsed;
        /// <summary>
        /// 获取或设置信号设置页面的显示状态
        /// </summary>
        public Visibility SignalSettingPageVisibility
        {
            get { return signalSettingPageVisibility; }
            set
            {
                if (signalSettingPageVisibility != value)
                {
                    signalSettingPageVisibility = value;
                    this.OnPropertyChanged(p => p.SignalSettingPageVisibility);//将该事件发送到显示界面进行显示效果的更新
                }
            }
        }

        private Visibility resultAdjudgeSettingPageVisibility = Visibility.Collapsed;
        /// <summary>
        /// 获取或设置结果判决设置页面的显示状态
        /// </summary>
        public Visibility ResultAdjudgeSettingPageVisibility
        {
            get { return resultAdjudgeSettingPageVisibility; }
            set
            {
                if (resultAdjudgeSettingPageVisibility != value)
                {
                    resultAdjudgeSettingPageVisibility = value;
                    this.OnPropertyChanged(p => p.ResultAdjudgeSettingPageVisibility);
                }
            }
        }

        private Visibility controlPageVisibility = Visibility.Collapsed;
        /// <summary>
        /// 获取或设置测试控制页面的显示状态
        /// </summary>
        public Visibility ControlPageVisibility
        {
            get { return controlPageVisibility; }
            set
            {
                if (controlPageVisibility != value)
                {
                    controlPageVisibility = value;
                    this.OnPropertyChanged(p => p.ControlPageVisibility);
                }
            }
        }

        private Visibility resultPageVisibility = Visibility.Collapsed;
        /// <summary>
        /// 获取或设置测试结果查看页面的显示状态
        /// </summary>
        public Visibility ResultPageVisibility
        {
            get { return resultPageVisibility; }
            set
            {
                if (resultPageVisibility != value)
                {
                    resultPageVisibility = value;
                    this.OnPropertyChanged(p => p.ResultPageVisibility);
                }
            }
        }

        /// <summary>
        /// 初始化主界面逻辑处理对象
        /// </summary>
        public MainWindowVM()
        {
            this.Init();
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        public override void Init()
        {
            PageNameAndVMDic = new Dictionary<string, BaseVM>();
            menuItems = new ObservableCollection<MenuItem>();
            menuItems.Add(new MenuItem
            {
                Text = "首页状态",//主界面显示内容
                ItemType = MAIN_PAGE,
                ImageSource = ImageUri.HOME_MENUITEM_IMAGE_SOURCE,
                IsSelected = true
            });
            menuItems.Add(new MenuItem
            {
                Text = "信号设置",//主界面显示

                ItemType = SIGNAL_SETTING_PAGE,
                ImageSource = ImageUri.SIGNAL_MENUITEM_IMAGE_SOURCE
            });
            menuItems.Add(new MenuItem
            {
                Text = "判决设置",
                ItemType = RESULT_ADJUDGE_SETTING_PAGE,
                ImageSource = ImageUri.SETTING_MENUITEM_IMAGE_SOURCE
            });
            menuItems.Add(new MenuItem
            {
                Text = "测试控制",
                ItemType = CONTROL_PAGE,
                ImageSource = ImageUri.CONTROL_MENUITEM_IMAGE_SOURCE
            });
            menuItems.Add(new MenuItem
            {
                Text = "结果查看",
                ItemType = RESULT_PAGE,
                ImageSource = ImageUri.RESULT_MENUITEM_IMAGE_SOURCE
            });
        }

        /// <summary>
        /// 添加命令绑定
        /// </summary>
        /// <param name="ele">主窗口对象</param>
        public override void AddCommands(UIElement ele)//在主窗口发生的时间，控制指令的变化
        {
            ele.CommandBindings.Add(new CommandBinding(MainWindowCommands.MenuItemSelectionChangedCommand, OnMenuItemSelectionChanged));
        }

        /// <summary>
        /// 菜单选择项改变时触发的事件
        /// </summary>
        /// <param name="sender">命令绑定的对象</param>
        /// <param name="e">命令相关参数</param>
        public void OnMenuItemSelectionChanged(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedMenuItem = e.Parameter as MenuItem;
            if (selectedMenuItem != null)
            {
                foreach (var item in menuItems)
                {
                    var visibility = (item.ItemType == selectedMenuItem.ItemType) ? Visibility.Visible : Visibility.Collapsed;
                    this.GetType().GetProperty(item.ItemType + "Visibility").SetValue(this, visibility, null);
                }
                BaseVM vm = null;
                PageNameAndVMDic.TryGetValue(selectedMenuItem.ItemType, out vm);
                if (vm != null)
                {
                    vm.Init();
                }
            }
        }

        /// <summary>
        /// 初始化观察者
        /// </summary>
        public void InitObservers()
        {
            var subject = new Subject();
            foreach (var vm in PageNameAndVMDic.Values)
            {
                subject.AddObserver(vm.Receive);
                vm.Subject = subject;
            }
        }
    }
}
