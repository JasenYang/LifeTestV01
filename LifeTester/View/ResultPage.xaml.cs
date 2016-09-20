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
using LifeTester.Util;
using LifeTester.Model;
using System.Collections.ObjectModel;

namespace LifeTester.View
{
    /// <summary>
    /// 表示结果查看界面
    /// </summary>
    public partial class ResultPage : BaseView
    {
        public ResultPage()
        {
            InitializeComponent();
            VM = new ResultPageVM();
            VM.AddCommands(this);
            //VM.Init();
            volAndEleChart.DataContext = VM;
            ohmChart.DataContext = VM;
            this.DataContext = VM;
            Common.SelectedDateTime = DateTime.Now;//将当前日期赋值给要显示的曲线的日期
            //dpDate.Text= DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
            ResultPageVM resultPageVM = (ResultPageVM)VM as ResultPageVM;
            resultPageVM.StartTimer();
        }



        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            DatePicker datePicker = (DatePicker)sender;
            DateTime? date = datePicker.SelectedDate;
            string value = date != null ? date.Value.ToString("yyyy-MM-dd") : null;
            datePicker.ToolTip = value;
            if (date.HasValue)
            {
                Common.SelectedDateTime = date.Value;
                //((ResultPageVM)VM).OnDtpSelectChanged();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("请选择日期");
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //dpDate.Text = Convert.ToDateTime(dpDate.Text).AddDays(1).ToString("yyyy-MM-dd") ;
            //Common.SelectedDateTime = Common.SelectedDateTime.AddDays(1);
        }

        private void btnPre_Click(object sender, RoutedEventArgs e)
        {
            //dpDate.Text = Convert.ToDateTime(dpDate.Text).AddDays(-1).ToString("yyyy-MM-dd");
            //Common.SelectedDateTime = Common.SelectedDateTime.AddDays(-1);
        }

        private void btnQueryData_Click(object sender, RoutedEventArgs e)
        {
            Common.SelectedDateTime = Convert.ToDateTime(dpDate.Text);

        }

    }
}
