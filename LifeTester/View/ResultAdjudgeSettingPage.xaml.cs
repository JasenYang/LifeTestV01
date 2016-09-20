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

namespace LifeTester.View
{
    /// <summary>
    /// 表示结果判决设置页面
    /// </summary>
    public partial class ResultAdjudgeSettingPage : BaseView
    {
        public ResultAdjudgeSettingPage()
        {
            InitializeComponent();
            VM = new ResultAdjudgeSettingPageVM();

            AppConfigManager app = new AppConfigManager();
            ResultAdjudgeSettingPageVM resultAdjudgeSettingPageVM = VM as ResultAdjudgeSettingPageVM;

            int iOHMMin1 = Convert.ToInt32(app.GetAppValue("OHMMin1"));
            int iOHMMax1 = Convert.ToInt32(app.GetAppValue("OHMMax1"));
            int iOHMMin2 = Convert.ToInt32(app.GetAppValue("OHMMin2"));
            int iOHMMax2 = Convert.ToInt32(app.GetAppValue("OHMMax2"));
            int iSKP = Convert.ToInt32(app.GetAppValue("SKP"));
            resultAdjudgeSettingPageVM.OhmMin1 = iOHMMin1;
            resultAdjudgeSettingPageVM.OhmMax1 = iOHMMax1;
            resultAdjudgeSettingPageVM.OhmMin2 = iOHMMin2;
            resultAdjudgeSettingPageVM.OhmMax2 = iOHMMax2;
            resultAdjudgeSettingPageVM.SKP = iSKP;
            this.DataContext = VM;
        }
    }
}
