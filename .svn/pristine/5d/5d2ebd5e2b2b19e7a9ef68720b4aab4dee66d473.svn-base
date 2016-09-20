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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LifeTester.UC;
using LifeTester.ViewModel;

namespace LifeTester.View
{
    /// <summary>
    /// 表示主页面
    /// </summary>
    public partial class MainPage : BaseView
    {
        public MainPage()
        {
            InitializeComponent();
            VM = new MainPageVM();
            VM.Init();
            this.DataContext = VM;
        }
    }
}
