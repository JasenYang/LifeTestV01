using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeTester.ViewModel
{
    /// <summary>
    /// 提供结果判决设置页面的处理逻辑
    /// </summary>
    public class ResultAdjudgeSettingPageVM : BaseVM
    {
        public static double OHMMin1 = 4;//by ldb 第一个板子最小值和最大值，生成曲线比较超范围的点使用
        public static double OHMMax1 = 4;
        public static double OHMMin2 = 4;//by ldb 第二个板子最小值和最大值，生成曲线比较超范围的点使用
        public static double OHMMax2 = 4;
        public static double SKP1 = 0;//by ldb 记录连续超限次数
        private double ohmMin1 = 4;
        /// <summary>
        /// 获取或设置板卡1电阻最小值
        /// </summary>
        public double OhmMin1
        {
            get { return ohmMin1; }
            set
            {
                if (ohmMin1 != value)
                {
                    ohmMin1 = value;
                    this.OnPropertyChanged(p => p.OhmMin1);//OnPropertyChanged该函数在BaseVM中进行了接收，当发生该指令的时候，则BaseVM通知UI更新
                    OHMMin1 = value;
                }
            }
        }

        private double ohmMax1 = 4;
        /// <summary>
        /// 获取或设置板卡1电阻最大值
        /// </summary>
        public double OhmMax1
        {
            get { return ohmMax1; }
            set
            {
                if (ohmMax1 != value)
                {
                    ohmMax1 = value;
                    this.OnPropertyChanged(p => p.OhmMax1);
                    OHMMax1 = value;
                }
            }
        }

        private double ohmMin2 = 4;
        /// <summary>
        /// 获取或设置板卡2电阻最小值
        /// </summary>
        public double OhmMin2
        {
            get { return ohmMin2; }
            set
            {
                if (ohmMin2 != value)
                {
                    ohmMin2 = value;
                    this.OnPropertyChanged(p => p.OhmMin2);
                    OHMMin2 = value;
                }
            }
        }

        private double ohmMax2 = 4;
        /// <summary>
        /// 获取或设置板卡2电阻最大值
        /// </summary>
        public double OhmMax2
        {
            get { return ohmMax2; }
            set
            {
                if (ohmMax2 != value)
                {
                    ohmMax2 = value;
                    this.OnPropertyChanged(p => p.OhmMax2);
                    OHMMax2 = value;
                }
            }
        }

        private int skp=0;
        /// <summary>
        /// 获取或设置SKP次数
        /// </summary>
        public int SKP
        {
            get { return skp; }
            set
            {
                if (skp != value)
                {
                    skp = value;
                    this.OnPropertyChanged(p => p.SKP);
                    SKP1 = value;
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
        }
    }
}
