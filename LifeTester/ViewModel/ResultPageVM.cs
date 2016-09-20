﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using LifeTester.Model;
using System.Windows;
using System.Windows.Input;
using LifeTester.Commands;
using LifeTester.Util;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Timers;
using System.Windows.Data;
using System.Windows.Controls.DataVisualization.Charting;

namespace LifeTester.ViewModel
{
    /// <summary>
    /// 提供结果页面处理逻辑
    /// </summary>
    public class ResultPageVM : BaseVM
    {
        private static object obj = new object();
        private ObservableCollection<Result> voltageResults;
        public System.Timers.Timer freshChartTimer = new System.Timers.Timer();//用于刷新Chart的计时器
        AppConfigManager app = new AppConfigManager();
        private double oHMLineMaxValueParam = 1;//by ldb 设置电阻曲线显示的最大值时，对应于界面设定值的倍数参数 
        private double oHMLineMaxValue = 12;//by ldb 设置电阻曲线的最大值
        private string DZ1 = "";
        private string DZ2 = "";
        private string ZX1 = "";
        private string ZX2 = "";
        /// <summary>
        /// 设置电阻曲线的最大值
        /// </summary>
        public double OHMLineMaxValue
        {
            get
            {
                return oHMLineMaxValue;
            }
            set
            {
                if (oHMLineMaxValue != value)
                {
                    oHMLineMaxValue = value;
                    this.OnPropertyChanged(p => p.OHMLineMaxValue);
                }
            }
        }
        /// <summary>
        /// 2016.06.20 name 黄建华 添加
        /// </summary>
        private string dyshowtitle;
        /// <summary>
        /// 获取或设置电流测试结果集合
        /// </summary>
        public string Dyshowtitle
        {
            get
            {
                if (dyshowtitle == null) dyshowtitle = "电压电流曲线图";
                return dyshowtitle;
            }
            set
            {
                if (dyshowtitle != value)
                {
                    dyshowtitle = value;
                    this.OnPropertyChanged(p => p.Dyshowtitle);
                }
            }
        }
        /// <summary>
        /// 2016.06.20 name 黄建华 添加
        /// </summary>
        private string showtitle;
        /// <summary>
        /// 获取或设置电流测试结果集合
        /// </summary>
        public string Showtitle
        {
            get
            {
                if (showtitle == null) showtitle = "电阻曲线图";
                return showtitle;
            }
            set
            {
                if (showtitle != value)
                {
                    showtitle = value;
                    this.OnPropertyChanged(p => p.Showtitle);
                }
            }
        }

        //by ldb 定时刷新曲线
        public void StartTimer()
        {
            try
            {
                AppConfigManager app = new AppConfigManager();
                int iFreshChartInterval = Convert.ToInt32(app.GetAppValue("FreshChartInterval"));
                freshChartTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent_FreshChart);
                freshChartTimer.Interval = iFreshChartInterval;
                freshChartTimer.Start();
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-StartTimer():" + ex.Message);
            }

        }
        /// <summary>
        /// Timer的Elapsed事件处理程序
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent_FreshChart(object source, ElapsedEventArgs e)
        {
            ////by ldb 创建线程保存数据
            //Thread t1 = new Thread(new ThreadStart(SaveToHistory));
            //t1.IsBackground = true;
            //t1.Start();


            try
            {
                //Debug.WriteLine("存储一次");
                List<Result> listE = new List<Result>();
                List<Result> listV = new List<Result>();
                List<Result> listO = new List<Result>();
                List<Result> listCCO = new List<Result>();
                List<Result> listZXCC = new List<Result>();
                bool bRet = GetChartResultsEx(ref listE, ref listV, ref listO, ref listCCO, ref listZXCC);
                if (bRet == false)
                {

                    VoltageResults = new ObservableCollection<Result>();
                    ElectricityResults = new ObservableCollection<Result>();
                    OhmResults = new ObservableCollection<Result>();
                    CCOhmResults = new ObservableCollection<Result>();
                    ZXCCOhmResults = new ObservableCollection<Result>();
                    return;
                }
                VoltageResults = null;
                VoltageResults = new ObservableCollection<Result>(listV);
                ElectricityResults = null;
                ElectricityResults = new ObservableCollection<Result>(listE);
                OhmResults = null;
                OhmResults = new ObservableCollection<Result>(listO);
                CCOhmResults = null;
                CCOhmResults = new ObservableCollection<Result>(listCCO);
                ZXCCOhmResults = null;
                ZXCCOhmResults = new ObservableCollection<Result>(listZXCC);
                if (listE != null && listE.Count > 0)
                {
                    listE.Clear();
                }
                if (listV != null && listV.Count > 0)
                {
                    listV.Clear();
                }

                if (listO != null && listO.Count > 0)
                {
                    listO.Clear();
                }

                if (listCCO != null && listCCO.Count > 0)
                {
                    listCCO.Clear();
                }

                if (listZXCC != null && listZXCC.Count > 0)
                {
                    listZXCC.Clear();
                }
                GC.Collect();

            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("ResultPageVM-OnTimedEvent_FreshChart:" + ex.Message);
            }
            //finally { PlayerModel._mu.ReleaseMutex(); }

        }
        /// <summary>
        /// 获取或设置电压测试结果集合
        /// </summary>
        public ObservableCollection<Result> VoltageResults
        {
            get
            {
                return voltageResults;
            }
            set
            {
                if (voltageResults != value)
                {
                    voltageResults = value;
                    this.OnPropertyChanged(p => p.VoltageResults);
                }
            }
        }

        private ObservableCollection<Result> electricityResults;
        /// <summary>
        /// 获取或设置电流测试结果集合
        /// </summary>
        public ObservableCollection<Result> ElectricityResults
        {
            get
            {
                return electricityResults;
            }
            set
            {
                if (electricityResults != value)
                {
                    electricityResults = value;
                    this.OnPropertyChanged(p => p.ElectricityResults);
                }
            }
        }

        private ObservableCollection<Result> ohmResults;
        /// <summary>
        /// 获取或设置电阻测试结果集合
        /// </summary>
        public ObservableCollection<Result> OhmResults
        {
            get
            {
                return ohmResults;
            }
            set
            {
                if (ohmResults != value)
                {
                    ohmResults = value;
                    this.OnPropertyChanged(p => p.OhmResults);
                }
            }
        }

        private ObservableCollection<Result> ccohmResults;
        /// <summary>
        /// 获取或设置电阻测试结果集合
        /// </summary>
        public ObservableCollection<Result> CCOhmResults
        {
            get
            {
                return ccohmResults;
            }
            set
            {
                if (ccohmResults != value)
                {
                    ccohmResults = value;
                    this.OnPropertyChanged(p => p.CCOhmResults);
                }
            }
        }

        private ObservableCollection<Result> zxccohmResults;
        /// <summary>
        /// 获取或设置电阻测试结果集合
        /// </summary>
        public ObservableCollection<Result> ZXCCOhmResults
        {
            get
            {
                return zxccohmResults;
            }
            set
            {
                if (zxccohmResults != value)
                {
                    zxccohmResults = value;
                    this.OnPropertyChanged(p => p.ZXCCOhmResults);
                }
            }
        }

        private ObservableCollection<Card> cards;
        /// <summary>
        /// 获取或设置板卡集合
        /// </summary>
        public ObservableCollection<Card> Cards
        {
            get { return cards; }
            set
            {
                if (cards != value)
                {
                    cards = value;
                    this.OnPropertyChanged(p => p.Cards);
                }
            }
        }

        /// <summary>
        /// 添加命令绑定
        /// </summary>
        /// <param name="ele">测试控制窗口对象</param>
        public override void AddCommands(UIElement ele)
        {
            ele.CommandBindings.Add(new CommandBinding(ResultPageCommands.ChannelSelectChangedCommand, OnChannelSelectChanged));
            ele.CommandBindings.Add(new CommandBinding(ResultPageCommands.ExportCommand, OnExport));
            ele.CommandBindings.Add(new CommandBinding(ResultPageCommands.BtnPreChartChangedCommand, OnBtnPre_Click));//by ldb 绑定上一天按钮事件
            ele.CommandBindings.Add(new CommandBinding(ResultPageCommands.BtnNextChartChangedCommand, OnBtnNext_Click));//by ldb 绑定下一天按钮事件
            ele.CommandBindings.Add(new CommandBinding(ResultPageCommands.DtpChartSelectChangedCommand, OnDtpSelectChanged));//by ldb 绑定日期选择改变事件
            ele.CommandBindings.Add(new CommandBinding(ResultPageCommands.BtnMoveDataToHistory, BtnMoveDataToHistory));//by ldb 转移数据文件成为历史文件
        }
        private List<LineSeries> li = new List<LineSeries>();
        /// <summary>
        /// 选择通道改变时触发事件
        /// </summary>
        /// <param name="sender">命令绑定的对象</param>
        /// <param name="e">命令相关参数</param>
        public void OnChannelSelectChanged(object sender, ExecutedRoutedEventArgs e)
        {
            //string[] str = { "test1", "test2", "test3", "test4", "test5", "test6" };
            //System.Windows.Controls.ContentControl cc = sender as System.Windows.Controls.ContentControl;
            //for (int i = 0; i < str.Length; i++)
            //{
            //    System.Windows.Controls.DataVisualization.Charting.LineSeries ls = cc.FindName(str[i]) as System.Windows.Controls.DataVisualization.Charting.LineSeries;

            //    if (ls != null && ls.Points != null)
            //    {
            //        ls.Refresh();
            //        if (ls.Name.Equals("test1"))
            //        {
            //            ls.ItemsSource = CCOhmResults;
            //            li.Add(ls);
            //        }
            //        if (ls.Name.Equals("test2"))
            //        {
            //            //ls.ItemsSource = null;
            //            //ls.DataContext = null;
            //            ls.ItemsSource = ZXCCOhmResults;
            //            li.Add(ls);
            //        }
            //        if (ls.Name.Equals("test3"))
            //        {
            //            //ls.ItemsSource = null;
            //            //ls.DataContext = null;
            //            ls.ItemsSource = OhmResults;
            //            li.Add(ls);
            //        }
            //        if (ls.Name.Equals("test4"))
            //        {
            //            //ls.ItemsSource = null;
            //            //ls.DataContext = null;
            //            ls.ItemsSource = ElectricityResults;
            //            li.Add(ls);
            //        }
            //        if (ls.Name.Equals("test5"))
            //        {
            //            //ls.ItemsSource = null;
            //            //ls.DataContext = null;
            //            ls.ItemsSource = VoltageResults;
            //            li.Add(ls);
            //        }

            //        //Binding myBinding = new Binding("CCOhmResults");
            //        //  myBinding.Source = CCOhmResults;
            //        //  ls.SetBinding(LineSeries.ItemsSourceProperty, myBinding);
            //        //ls.Clear();
            //        // ls.ItemsSource = null;
            //        // var ssd=  ls.ItemsSource;

            //        //  ls.DataContext = null;

            //        //}
            //    }
            //}
            //System.Windows.Controls.DataVisualization.Charting.Chart ss = cc.FindName("ohmChart") as System.Windows.Controls.DataVisualization.Charting.Chart;
            //if (ss != null)
            //{
            //    //ss.Series.Clear();
            //    foreach (var item in li)
            //    {
            //        ss.Series.Add(item);
            //    }

            //    //for (int i = 0; i < ss.Series.Count; i++)
            //    //{
            //    //    //ss.Series[0].Clear();
            //    //    //ss.Series[i].LegendItems.Clear();

            //    //    //ss.Series[i].SeriesHost.Series.Clear();
            //    //}
            //}

            //System.Windows.Controls.DataVisualization.Charting.Series aad = ;

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //System.Windows.Controls.DataVisualization.Charting.LineSeries dd = cc.FindName("test1") as System.Windows.Controls.DataVisualization.Charting.LineSeries;
            //dd.ItemsSource = null;
            //dd.DataContext = null;
            //System.Windows.Controls.ContentControl cc1 = sender as System.Windows.Controls.ContentControl;
            //System.Windows.Controls.DataVisualization.Charting.LineSeries dd2 = cc1.FindName("test2") as System.Windows.Controls.DataVisualization.Charting.LineSeries;
            //dd2.ItemsSource = null;
            //dd2.DataContext = null;
            //System.Windows.Controls.ContentControl cc2 = sender as System.Windows.Controls.ContentControl;
            //System.Windows.Controls.DataVisualization.Charting.LineSeries dd3 = cc2.FindName("test3") as System.Windows.Controls.DataVisualization.Charting.LineSeries;
            //dd3.ItemsSource = null;
            //dd3.DataContext = null;
            //System.Windows.Controls.ContentControl cc3 = sender as System.Windows.Controls.ContentControl;
            //System.Windows.Controls.DataVisualization.Charting.LineSeries dd4 = cc3.FindName("test4") as System.Windows.Controls.DataVisualization.Charting.LineSeries;
            //dd4.ItemsSource = null;
            //dd4.DataContext = null;
            //System.Windows.Controls.ContentControl cc4 = sender as System.Windows.Controls.ContentControl;
            //System.Windows.Controls.DataVisualization.Charting.LineSeries dd5 = cc4.FindName("test5") as System.Windows.Controls.DataVisualization.Charting.LineSeries;
            //dd5.ItemsSource = null;
            //dd5.DataContext = null;
            //((System.Windows.FrameworkElement)(((System.Windows.Controls.Panel)(((System.Windows.Controls.Panel)((System.Windows.Controls.ContentControl)sender).Content).Children[0])).Children[3])).DataContext = null;
            //GC.Collect();
            //((System.Windows.FrameworkElement)(((System.Windows.Controls.Panel)(((System.Windows.Controls.Panel)((System.Windows.Controls.ContentControl)sender).Content).Children[0])).Children[3])).DataContext = this;
            //((System.Windows.FrameworkElement)(((System.Windows.Controls.Panel)(((System.Windows.Controls.Panel)((System.Windows.Controls.ContentControl)sender).Content).Children[0])).Children[2])).DataContext = null;
            //GC.Collect();
            //((System.Windows.FrameworkElement)(((System.Windows.Controls.Panel)(((System.Windows.Controls.Panel)((System.Windows.Controls.ContentControl)sender).Content).Children[0])).Children[2])).DataContext = this;
            try
            {

                oHMLineMaxValueParam = Convert.ToDouble(app.GetAppValue("OHMLineMaxValueParam"));
                var channel = e.Parameter as Channel;

                if (channel == null)
                {
                    VoltageResults = new ObservableCollection<Result>();
                    ElectricityResults = new ObservableCollection<Result>();
                    OhmResults = new ObservableCollection<Result>();
                    return;
                }
                // by 黄建华
                Dyshowtitle = "电压电流曲线图(" + channel.CombineNumber + ")";
                Showtitle = "电阻曲线图(" + channel.CombineNumber + ")";
                if (channel.CardNumber == "1")//by ldb 根据不同板卡设定的电阻最大值和设定的参数定义曲线坐标的做大值
                {
                    OHMLineMaxValue = ResultAdjudgeSettingPageVM.OHMMax1 * oHMLineMaxValueParam;
                }
                else if (channel.CardNumber == "2")
                {
                    OHMLineMaxValue = ResultAdjudgeSettingPageVM.OHMMax2 * oHMLineMaxValueParam;
                }
                //by ldb 存储到常量中，用于曲线显示
                Common.SelectedChannel = new ChannelHistory() { Number = channel.Number, CardNumber = channel.CardNumber };
                //// TODO:显示当前板卡结果曲线图
                //string datetime =DateTime.Now.ToString("yyyy-MM-dd");
                ////var voltageDemoResults = GetVoltageResults(channel.Number, channel.CardNumber);
                //var voltageDemoResults = GetVoltageResultsEx(channel.Number, channel.CardNumber,datetime);
                //VoltageResults = new ObservableCollection<Result>(voltageDemoResults);
                ////var electricityDemoResults = GetElectricResults(channel.Number, channel.CardNumber);
                //var electricityDemoResults = GetElectricResultsEx(channel.Number, channel.CardNumber,datetime);
                //ElectricityResults = new ObservableCollection<Result>(electricityDemoResults);
                ////var ohmDemoResults = GetOhmResults(channel.Number, channel.CardNumber);
                // var ohmDemoResults = GetOhmResultsEx(channel.Number, channel.CardNumber,datetime);
                //OhmResults = new ObservableCollection<Result>(ohmDemoResults);

                // TODO:显示当前板卡结果曲线图
                //string datetime = DateTime.Now.ToString("yyyy-MM-dd");
                string datetime = Common.SelectedDateTime.ToString("yyyy-MM-dd");
                //var voltageDemoResults = GetVoltageResults(channel.Number, channel.CardNumber);
                List<Result> listE = new List<Result>();
                List<Result> listV = new List<Result>();
                List<Result> listO = new List<Result>();

                GetChartResultsEx(ref listE, ref listV, ref listO, channel.Number, channel.CardNumber, datetime);
                Debug.WriteLine("begin" + DateTime.Now.ToString("mmssfff"));
                VoltageResults = null;
                VoltageResults = new ObservableCollection<Result>(listV);
                ElectricityResults = null;
                ElectricityResults = new ObservableCollection<Result>(listE);
                OhmResults = null;
                OhmResults = new ObservableCollection<Result>(listO);
                CCOhmResults = new ObservableCollection<Result>();
                ZXCCOhmResults = new ObservableCollection<Result>();
                int i = 0;
                foreach (var item in ohmResults)
                {
                    if (i == 0)
                    {
                        Result tmpO = item.Clone() as Result;
                        if (channel.CardNumber == "1")
                        {
                            tmpO.Value = ResultAdjudgeSettingPageVM.OHMMax1;
                        }
                        else
                        {
                            tmpO.Value = ResultAdjudgeSettingPageVM.OHMMax2;
                        }
                        CCOhmResults.Add(tmpO);
                        Result tmp1 = item.Clone() as Result;
                        if (channel.CardNumber == "1")
                        {
                            tmp1.Value = ResultAdjudgeSettingPageVM.OHMMin1;
                        }
                        else
                        {
                            tmp1.Value = ResultAdjudgeSettingPageVM.OHMMin2;
                        }
                        ZXCCOhmResults.Add(tmp1);
                    }
                    if (i == ohmResults.Count - 1)
                    {
                        Result tmpO = item.Clone() as Result;
                        if (channel.CardNumber == "1")
                        {
                            tmpO.Value = ResultAdjudgeSettingPageVM.OHMMax1;
                        }
                        else
                        {
                            tmpO.Value = ResultAdjudgeSettingPageVM.OHMMax2;
                        }
                        CCOhmResults.Add(tmpO);
                        Result tmp1 = item.Clone() as Result;
                        if (channel.CardNumber == "1")
                        {
                            tmp1.Value = ResultAdjudgeSettingPageVM.OHMMin1;
                        }
                        else
                        {
                            tmp1.Value = ResultAdjudgeSettingPageVM.OHMMin2;
                        }
                        ZXCCOhmResults.Add(tmp1);

                    }
                    i++;
                }

                if (listE != null && listE.Count > 0)
                {
                    listE.Clear();
                }
                if (listV != null && listV.Count > 0)
                {
                    listV.Clear();
                }

                if (listO != null && listO.Count > 0)
                {
                    listO.Clear();
                }


                HistoryTimer.CurrentPage = 0;//by ldb 重置为第一页
                Debug.WriteLine("all line " + listV.Count);
                Debug.WriteLine("end" + DateTime.Now.ToString("mmssfff"));
                GC.Collect();
            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("ResultPageVM-OnChannelSelectChanged:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取电压测试结果
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        private List<Result> GetVoltageResults(string channelNumber, string cardNumber)
        {
            try
            {
                var results = new List<Result>();
                Cache.Instance.HistorySates.ForEach(s =>
                {
                    if (s.Number.Equals(channelNumber) && s.CardNumber.Equals(cardNumber))
                    {
                        Result result = new Result();
                        result.Seconds = (int)s.ActualDuration;
                        result.Value = s.Voltage;
                        results.Add(result);
                    }
                });

                return results;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-GetVoltageResults:" + ex.Message);
                return null;
            }

        }
        /// <summary>
        /// by ldb 获取电压测试结果
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <param name="cardNumber"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private List<Result> GetVoltageResultsEx(string channelNumber, string cardNumber, string datetime)
        {
            try
            {
                var results = new List<Result>();
                string path = @"\" + cardNumber + @"\" + channelNumber;
                string filename = datetime + ".dat";
                List<ChannelHistory> list = HistroyDBManager.GetHistoryFromFile(path, filename);
                list.ForEach(s =>
                {
                    if (s.Number.Equals(channelNumber) && s.CardNumber.Equals(cardNumber))
                    {
                        Result result = new Result();
                        result.Seconds = (int)s.ActualDuration;
                        result.Value = s.Voltage;
                        results.Add(result);
                    }
                });
                if (list != null && list.Count > 0)
                {
                    list.Clear();
                }


                return results;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-GetVoltageResultsEx:" + ex.Message);
                return null;
            }

        }
        /// <summary>
        /// 获取电流测试结果
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        private List<Result> GetElectricResults(string channelNumber, string cardNumber)
        {
            try
            {
                var results = new List<Result>();

                Cache.Instance.HistorySates.ForEach(s =>
                {
                    if (s.Number.Equals(channelNumber) && s.CardNumber.Equals(cardNumber))
                    {
                        Result result = new Result();
                        result.Seconds = (int)s.ActualDuration;
                        result.Value = s.Electricity;
                        results.Add(result);
                    }
                });

                return results;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-GetElectricResults:" + ex.Message);
                return null;
            }

        }
        /// <summary>
        /// by ldb 获取电流测试结果
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        private List<Result> GetElectricResultsEx(string channelNumber, string cardNumber, string datetime)
        {
            try
            {
                var results = new List<Result>();
                string path = @"\" + cardNumber + @"\" + channelNumber;
                string filename = datetime + ".dat";
                List<ChannelHistory> list = HistroyDBManager.GetHistoryFromFile(path, filename);
                list.ForEach(s =>
                {
                    if (s.Number.Equals(channelNumber) && s.CardNumber.Equals(cardNumber))
                    {
                        Result result = new Result();
                        result.Seconds = (int)s.ActualDuration;
                        result.Value = s.Electricity;
                        results.Add(result);
                    }
                });
                if (list != null && list.Count > 0)
                {
                    list.Clear();
                }

                return results;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-GetElectricResults:" + ex.Message);
                return null;
            }

        }
        ///// <summary>
        ///// by ldb 获取电流,电压和电阻测试结果
        ///// </summary>
        ///// <param name="channelNumber"></param>
        ///// <returns></returns>
        //private void GetChartResultsEx(ref List<Result> listElectric, ref List<Result> listVoltage, ref List<Result> listOhm, string channelNumber, string cardNumber, string datetime)
        //{
        //    var resultsElectric = new List<Result>();
        //    var resultsVoltage = new List<Result>();
        //    var resultsOhm = new List<Result>();
        //    string path = @"\" + cardNumber + @"\" + channelNumber;
        //    string filename = datetime + ".dat";
        //    List<ChannelHistory> list = HistroyDBManager.GetHistoryFromFile(path, filename);
        //    if (list==null)
        //    {
        //        return;
        //    }
        //    list.FindAll(p => p.CardNumber == cardNumber && p.Number == channelNumber).ForEach(s =>
        //    {
        //        //超过要求的chart点的数量就停止存储

        //        Result result = new Result();
        //        result.Seconds = (int)s.ActualDuration;
        //        result.Value = s.Electricity;
        //        if (resultsElectric.Count< HistoryTimer.ChartPointUpLimit&&CheckValue(s.Electricity,1))
        //        {
        //            resultsElectric.Add(result);
        //        }

        //        Result tmpV = result.Clone() as Result;
        //        tmpV.Value = s.Voltage;
        //        if (resultsVoltage.Count < HistoryTimer.ChartPointUpLimit && CheckValue(s.Electricity, 2))
        //        {
        //            resultsVoltage.Add(tmpV);
        //        }

        //        Result tmpO = result.Clone() as Result;
        //        tmpO.Value = s.Ohm;
        //        if (resultsOhm.Count < HistoryTimer.ChartPointUpLimit && CheckValue(s.Electricity, 3))
        //        {
        //            resultsOhm.Add(tmpO);
        //        }

        //    });
        //    listElectric.AddRange(resultsElectric);
        //    listVoltage.AddRange(resultsVoltage);
        //    listOhm.AddRange(resultsOhm);
        //}
        /// <summary>
        /// by ldb 获取电流,电压和电阻测试结果
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        private void GetChartResultsEx(ref List<Result> listElectric, ref List<Result> listVoltage, ref List<Result> listOhm, string channelNumber, string cardNumber, string datetime)
        {
            try
            {
                Debug.WriteLine(" GetChartResultsEx begin" + DateTime.Now.ToString("mmssfff"));
                var resultsElectric = new List<Result>();
                var resultsVoltage = new List<Result>();
                var resultsOhm = new List<Result>();
                string path = @"\" + cardNumber + @"\" + channelNumber;
                string filename = datetime + ".dat";
                List<ChannelHistory> list = HistroyDBManager.GetHistoryFromFile(path, filename);
                //从缓冲区拿未保存的数据，以保证要显示的数据的完整性
                var tmp = HistoryTimer.GetUnSavedChannelHistory();
                if (tmp != null)
                {
                    if (list == null)
                    {
                        list = tmp;
                    }
                    else
                    {
                        list.AddRange(tmp);
                    }

                }
                if (list == null)
                {
                    VoltageResults = new ObservableCollection<Result>();
                    ElectricityResults = new ObservableCollection<Result>();
                    OhmResults = new ObservableCollection<Result>();
                    return;
                }
                list = list.FindAll(p => p.CardNumber == cardNumber && p.Number == channelNumber);
                //如果小于或等于采集上限，则全要
                //如果大于采集上限则将总数去除采集上限得到最小整数，然后按照这个数字分组
                //对每组取边界值，并且对于组内的超出波动范围的也统计进来，最后输出所有值
                //对于超出的要最后一个边界值
                if (list.Count <= HistoryTimer.ChartPointUpLimit)
                {
                    list.ForEach(s =>
                    {
                        Result result = new Result();
                        result.Seconds = (int)s.ActualDuration;
                        result.Value = s.Electricity;
                        resultsElectric.Add(result);

                        Result tmpV = result.Clone() as Result;
                        tmpV.Value = s.Voltage;
                        resultsVoltage.Add(tmpV);

                        Result tmpO = result.Clone() as Result;
                        tmpO.Value = s.Ohm;
                        resultsOhm.Add(tmpO);
                    });
                }
                else
                {
                    int iCount = list.Count;
                    int iUnit = iCount / HistoryTimer.ChartPointUpLimit;//求出最大整数
                    //double avg = list.Average(s=>s.Voltage);//求出电压的平均值
                    //double avg = list.Average(s => s.Ohm);//求出电阻的平均值

                    //判断是否有余数，如果有则把最后一个临界值也要算到统计数据内
                    //by ldb 为了优化，对于电压和电流仅仅采集配置文件中的上限数量设定值，电阻则还包含超出波动范围的部分
                    for (int i = 0; i < iCount; i++)
                    {
                        if (i % iUnit == 0 || i == iCount - 1)
                        {
                            var s = list[i];
                            Result result = new Result();
                            result.Seconds = (int)s.ActualDuration;
                            result.Value = s.Electricity;
                            resultsElectric.Add(result);

                            Result tmpV = result.Clone() as Result;
                            tmpV.Value = s.Voltage;
                            resultsVoltage.Add(tmpV);

                            Result tmpO = result.Clone() as Result;
                            tmpO.Value = s.Ohm;
                            resultsOhm.Add(tmpO);
                        }
                        //else if (Math.Abs(list[i].Ohm-avg)/avg>HistoryTimer.ValueOffSet)//如果非采集点但大于浮动范围，则作为统计数据
                        //{
                        //    var s = list[i];
                        //    Result result = new Result();
                        //    //result.Seconds = (int)s.ActualDuration;
                        //    //result.Value = s.Electricity;
                        //    //resultsElectric.Add(result);

                        //    //Result tmpV = result.Clone() as Result;
                        //    //tmpV.Value = s.Voltage;
                        //    //resultsVoltage.Add(tmpV);

                        //    Result tmpO = result.Clone() as Result;
                        //    tmpO.Value = s.Ohm;
                        //    resultsOhm.Add(tmpO);
                        //}
                        else if (list[i].CardNumber == "1" && (list[i].Ohm > ResultAdjudgeSettingPageVM.OHMMax1 || list[i].Ohm < ResultAdjudgeSettingPageVM.OHMMin1))//如果非采集点但大于浮动范围，则作为统计数据
                        {
                            var s = list[i];
                            Result result = new Result();
                            //result.Seconds = (int)s.ActualDuration;
                            //result.Value = s.Electricity;
                            //resultsElectric.Add(result);

                            //Result tmpV = result.Clone() as Result;
                            //tmpV.Value = s.Voltage;
                            //resultsVoltage.Add(tmpV);

                            Result tmpO = result.Clone() as Result;
                            tmpO.Value = s.Ohm;
                            resultsOhm.Add(tmpO);
                        }
                        else if (list[i].CardNumber == "2" && (list[i].Ohm > ResultAdjudgeSettingPageVM.OHMMax2 || list[i].Ohm < ResultAdjudgeSettingPageVM.OHMMin2))//如果非采集点但大于浮动范围，则作为统计数据
                        {
                            var s = list[i];
                            Result result = new Result();
                            //result.Seconds = (int)s.ActualDuration;
                            //result.Value = s.Electricity;
                            //resultsElectric.Add(result);

                            //Result tmpV = result.Clone() as Result;
                            //tmpV.Value = s.Voltage;
                            //resultsVoltage.Add(tmpV);

                            Result tmpO = result.Clone() as Result;
                            tmpO.Value = s.Ohm;
                            resultsOhm.Add(tmpO);
                        }
                    }
                }
                if (listElectric != null && listElectric.Count > 0)
                {
                    listElectric.Clear();
                }
                if (listVoltage != null && listVoltage.Count > 0)
                {
                    listVoltage.Clear();
                }
                if (listOhm != null && listOhm.Count > 0)
                {
                    listOhm.Clear();
                }

                listElectric.AddRange(resultsElectric);
                listVoltage.AddRange(resultsVoltage);
                listOhm.AddRange(resultsOhm);

                if (tmp != null && tmp.Count > 0)
                {
                    tmp.Clear();
                }
                if (list != null && list.Count > 0)
                {
                    list.Clear();
                }
                if (resultsElectric != null && resultsElectric.Count > 0)
                {
                    resultsElectric.Clear();
                }
                if (resultsVoltage != null && resultsVoltage.Count > 0)
                {
                    resultsVoltage.Clear();
                }
                if (resultsOhm != null && resultsOhm.Count > 0)
                {
                    resultsOhm.Clear();
                }

                GC.Collect();
                Debug.WriteLine(" GetChartResultsEx end" + DateTime.Now.ToString("mmssfff"));
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-GetChartResultsEx:" + ex.Message);

            }

        }
        ///// <summary>
        ///// by ldb 获取电流,电压和电阻测试结果
        ///// </summary>
        ///// <param name="channelNumber"></param>
        ///// <returns></returns>
        //private bool GetChartResultsEx(ref List<Result> listElectric, ref List<Result> listVoltage, ref List<Result> listOhm)
        //{
        //    if (Common.SelectedChannel==null||Common.SelectedDateTime==null)
        //    {
        //        return false;
        //    }
        //    var datetime = Common.SelectedDateTime.ToString("yyyy-MM-dd");
        //    var cardNumber = Common.SelectedChannel.CardNumber;
        //    var channelNumber = Common.SelectedChannel.Number;
        //    var resultsElectric = new List<Result>();
        //    var resultsVoltage = new List<Result>();
        //    var resultsOhm = new List<Result>();
        //    string path = @"\" + cardNumber + @"\" + channelNumber;
        //    string filename = datetime + ".dat";
        //    List<ChannelHistory> list = HistroyDBManager.GetHistoryFromFile(path, filename);
        //    if (list == null)
        //    {
        //        return false;
        //    }
        //    if (HistoryTimer.CurrentPage*HistoryTimer.ChartPointUpLimit>list.Count)
        //    {
        //        HistoryTimer.CurrentPage--;
        //        System.Windows.Forms.MessageBox.Show("已是最后一页");
        //        return false;
        //    }
        //    if (HistoryTimer.CurrentPage <0)
        //    {
        //        HistoryTimer.CurrentPage++;
        //        System.Windows.Forms.MessageBox.Show("已是第一页");
        //        return false;
        //    }
        //    list.RemoveRange(0, HistoryTimer.CurrentPage * HistoryTimer.ChartPointUpLimit);//移除之前的元素
        //    list.FindAll(p => p.CardNumber == cardNumber && p.Number == channelNumber).ForEach(s =>
        //    {
        //        //超过要求的chart点的数量就停止存储

        //        Result result = new Result();
        //        result.Seconds = (int)s.ActualDuration;
        //        result.Value = s.Electricity;
        //        if (resultsElectric.Count < HistoryTimer.ChartPointUpLimit && CheckValue(s.Electricity, 1))
        //        {
        //            resultsElectric.Add(result);
        //        }

        //        Result tmpV = result.Clone() as Result;
        //        tmpV.Value = s.Voltage;
        //        if (resultsVoltage.Count < HistoryTimer.ChartPointUpLimit && CheckValue(s.Electricity, 2))
        //        {
        //            resultsVoltage.Add(tmpV);
        //        }

        //        Result tmpO = result.Clone() as Result;
        //        tmpO.Value = s.Ohm;
        //        if (resultsOhm.Count < HistoryTimer.ChartPointUpLimit && CheckValue(s.Electricity, 3))
        //        {
        //            resultsOhm.Add(tmpO);
        //        }

        //    });
        //    listElectric.AddRange(resultsElectric);
        //    listVoltage.AddRange(resultsVoltage);
        //    listOhm.AddRange(resultsOhm);
        //    return true;
        //}
        /// <summary>
        /// by ldb 获取电流,电压和电阻测试结果
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        private bool GetChartResultsEx(ref List<Result> listElectric, ref List<Result> listVoltage, ref List<Result> listOhm, ref List<Result> listCCO, ref List<Result> listZXCC)
        {
            try
            {
                if (Common.SelectedChannel == null || Common.SelectedDateTime == null)
                {
                    return false;
                }
                var datetime = Common.SelectedDateTime.ToString("yyyy-MM-dd");
                var cardNumber = Common.SelectedChannel.CardNumber;
                var channelNumber = Common.SelectedChannel.Number;
                var resultsElectric = new List<Result>();
                var resultsVoltage = new List<Result>();
                var resultsOhm = new List<Result>();
                var listZXCCohm = new List<Result>();
                var CCOhmResultslist = new List<Result>();
                string path = @"\" + cardNumber + @"\" + channelNumber;
                string filename = datetime + ".dat";

                List<ChannelHistory> list = HistroyDBManager.GetHistoryFromFile(path, filename);
                //如果要获取当天的数据，则从缓冲区拿未保存的数据，以保证要显示的数据的完整性
                if (datetime == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    var tmp = HistoryTimer.GetUnSavedChannelHistory();
                    if (tmp != null)
                    {
                        if (list == null)
                        {
                            list = tmp;
                        }
                        else
                        {
                            list.AddRange(tmp);
                        }
                        tmp.Clear();
                    }
                }

                if (list == null)
                {
                    VoltageResults = new ObservableCollection<Result>();
                    ElectricityResults = new ObservableCollection<Result>();
                    OhmResults = new ObservableCollection<Result>();
                    return false;
                }
                list = list.FindAll(p => p.CardNumber == cardNumber && p.Number == channelNumber);
                //如果小于或等于采集上限，则全要
                //如果大于采集上限则将总数去除采集上限得到最小整数，然后按照这个数字分组
                //对每组取边界值，并且对于组内的超出波动范围的也统计进来，最后输出所有值
                //对于超出的要最后一个边界值
                if (list.Count <= HistoryTimer.ChartPointUpLimit)
                {
                    list.ForEach(s =>
                    {
                        Result result = new Result();
                        result.Seconds = (int)s.ActualDuration;
                        result.Value = s.Electricity;
                        resultsElectric.Add(result);

                        Result tmpV = result.Clone() as Result;
                        tmpV.Value = s.Voltage;
                        resultsVoltage.Add(tmpV);

                        Result tmpO = result.Clone() as Result;
                        tmpO.Value = s.Ohm;
                        resultsOhm.Add(tmpO);
                    });
                }
                else
                {
                    int iCount = list.Count;
                    int iUnit = iCount / HistoryTimer.ChartPointUpLimit;//求出最大整数
                    double avg = list.Average(s => s.Voltage);//求出电压的平均值
                    //判断是否有余数，如果有则把最后一个临界值也要算到统计数据内
                    for (int i = 0; i < iCount; i++)
                    {
                        if (i % iUnit == 0 || i == iCount - 1)
                        {
                            var s = list[i];
                            Result result = new Result();
                            result.Seconds = (int)s.ActualDuration;
                            result.Value = s.Electricity;
                            resultsElectric.Add(result);

                            Result tmpV = result.Clone() as Result;
                            tmpV.Value = s.Voltage;
                            resultsVoltage.Add(tmpV);

                            Result tmpO = result.Clone() as Result;
                            tmpO.Value = s.Ohm;
                            resultsOhm.Add(tmpO);
                        }
                        else if (Math.Abs(list[i].Voltage - avg) / avg > HistoryTimer.ValueOffSet)//如果非采集点但大于浮动范围，则作为统计数据
                        {
                            var s = list[i];
                            Result result = new Result();
                            result.Seconds = (int)s.ActualDuration;
                            result.Value = s.Electricity;
                            resultsElectric.Add(result);

                            Result tmpV = result.Clone() as Result;
                            tmpV.Value = s.Voltage;
                            resultsVoltage.Add(tmpV);

                            Result tmpO = result.Clone() as Result;
                            tmpO.Value = s.Ohm;
                            resultsOhm.Add(tmpO);
                        }
                    }
                }

                listElectric.AddRange(resultsElectric);
                listVoltage.AddRange(resultsVoltage);
                listOhm.AddRange(resultsOhm);
                int j = 0;
                foreach (var item in listOhm)
                {
                    if (j == 0)
                    {
                        Result tmpO = item.Clone() as Result;
                        if (Common.SelectedChannel.CardNumber == "1")
                        {
                            tmpO.Value = ResultAdjudgeSettingPageVM.OHMMax1;
                        }
                        else
                        {
                            tmpO.Value = ResultAdjudgeSettingPageVM.OHMMax2;
                        }

                        CCOhmResultslist.Add(tmpO);
                        Result tmp1 = item.Clone() as Result;
                        if (Common.SelectedChannel.CardNumber == "1")
                        {
                            tmp1.Value = ResultAdjudgeSettingPageVM.OHMMin1;
                        }
                        else
                        {
                            tmp1.Value = ResultAdjudgeSettingPageVM.OHMMin2;
                        }

                        listZXCCohm.Add(tmp1);
                    }
                    if (j == listOhm.Count() - 1)
                    {
                        Result tmpO = item.Clone() as Result;
                        if (Common.SelectedChannel.CardNumber == "1")
                        {
                            tmpO.Value = ResultAdjudgeSettingPageVM.OHMMax1;
                        }
                        else
                        {
                            tmpO.Value = ResultAdjudgeSettingPageVM.OHMMax2;
                        }

                        CCOhmResultslist.Add(tmpO);
                        Result tmp1 = item.Clone() as Result;
                        if (Common.SelectedChannel.CardNumber == "1")
                        {
                            tmp1.Value = ResultAdjudgeSettingPageVM.OHMMin1;
                        }
                        else
                        {
                            tmp1.Value = ResultAdjudgeSettingPageVM.OHMMin2;
                        }

                        listZXCCohm.Add(tmp1);
                    }
                    j++;
                }

                listCCO.AddRange(CCOhmResultslist);
                listZXCC.AddRange(listZXCCohm);
                if (list != null && list.Count > 0)
                {
                    list.Clear();
                }
                if (resultsElectric != null && resultsElectric.Count > 0)
                {
                    resultsElectric.Clear();
                }
                if (resultsVoltage != null && resultsVoltage.Count > 0)
                {
                    resultsVoltage.Clear();
                }
                if (resultsOhm != null && resultsOhm.Count > 0)
                {
                    resultsOhm.Clear();
                }
                if (listZXCCohm != null && listZXCCohm.Count > 0)
                {
                    listZXCCohm.Clear();
                }
                if (CCOhmResultslist != null && CCOhmResultslist.Count > 0)
                {
                    CCOhmResultslist.Clear();
                }
                GC.Collect();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-GetChartResultsEx:" + ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 获取电阻测试结果
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        private List<Result> GetOhmResults(string channelNumber, string cardNumber)
        {
            try
            {
                var results = new List<Result>();

                Cache.Instance.HistorySates.ForEach(s =>
                {
                    if (s.Number.Equals(channelNumber) && s.CardNumber.Equals(cardNumber))
                        {
                        Result result = new Result();
                        result.Seconds = (int)s.ActualDuration;
                        result.Value = s.Ohm;
                        results.Add(result);
                    }
                });

                return results;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-GetOhmResults:" + ex.Message);
                return null;
            }

        }
        /// <summary>
        /// 获取电阻测试结果
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        private List<Result> GetOhmResultsEx(string channelNumber, string cardNumber, string datetime)
        {
            try
            {
                var results = new List<Result>();
                string path = @"\" + cardNumber + @"\" + channelNumber;
                string filename = datetime + ".dat";
                List<ChannelHistory> list = HistroyDBManager.GetHistoryFromFile(path, filename);
                list.ForEach(s =>
                {
                    if (s.Number.Equals(channelNumber) && s.CardNumber.Equals(cardNumber))
                    {
                        Result result = new Result();
                        result.Seconds = (int)s.ActualDuration;
                        result.Value = s.Ohm;
                        results.Add(result);
                    }
                });
                if (list != null && list.Count > 0)
                {
                    list.Clear();
                }
                return results;
            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("ResultPageVM-GetOhmResultsEx:" + ex.Message);
                return null;
            }

        }
        /// <summary>
        /// 导出按钮点击时触发事件
        /// </summary>
        /// <param name="sender">命令绑定的对象</param>
        /// <param name="e">命令相关参数</param>
        public void OnExport(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var dialog = new Microsoft.Win32.SaveFileDialog()
                {
                    DefaultExt = "csv",
                    Filter = "csv文件(*.csv)|*.csv",
                    FilterIndex = 1
                };
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        //var dtVoltage = ResultDsConvert.ToDataTable(VoltageResults, ResultDsConvert.VOLTAGE);
                        //var dtElectricity = ResultDsConvert.ToDataTable(ElectricityResults, ResultDsConvert.ELECTRICITY);
                        //var dtOhm = ResultDsConvert.ToDataTable(OhmResults, ResultDsConvert.OHM);
                        //var ds = new DataSet();
                        //ds.Tables.Add(dtVoltage);
                        //ds.Tables.Add(dtElectricity);
                        //ds.Tables.Add(dtOhm);

                        //var sheetNames = new List<string>() 
                        //{ 
                        //    "电压", "电流", "电阻"
                        //};

                        //NPOIHelper.ExportDStoExcel(ds, sheetNames, dialog.FileName);
                        //MessageBoxWindow.ShowDialog("导出成功。");
                        DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\Data\");
                        ArrayList list = GetAll(dir);
                        string csvPath = dialog.FileName;
                        bool isWriteTitle = true;//是否写列头，只有第一个循环写列头
                        if (list.Count > 0)
                        {
                            foreach (string item in list)
                            {
                                //生成CSV文件
                                System.Diagnostics.Debug.WriteLine(item);
                                CSVFileHelper.SaveCSV(isWriteTitle, item, csvPath);
                                isWriteTitle = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxWindow.ShowDialog("错误", "导出失败。" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("ResultPageVM-OnExport:" + ex.Message);
            }

        }
        ArrayList GetAll(DirectoryInfo dir)//搜索文件夹中的文件
        {
            try
            {
                ArrayList FileList = new ArrayList();
                FileList.Clear();
                FileInfo[] allFile = dir.GetFiles();
                foreach (FileInfo fi in allFile)
                {
                    FileList.Add(fi.FullName);
                }

                DirectoryInfo[] allDir = dir.GetDirectories();
                foreach (DirectoryInfo d in allDir)
                {
                    ArrayList list = GetAll(d);
                    if (list.Count > 0)
                    {
                        FileList.AddRange(list);
                    }
                }
                return FileList;
            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("ResultPageVM-GetAll:" + ex.Message);
                return null;
            }

        }
        private List<Result> InitDemoResults()
        {
            var startDt = new DateTime(2015, 12, 22, 17, 30, 0);
            var results = new List<Result>();
            int i = 0;
            long tick = DateTime.Now.Ticks;
            for (var dt = startDt; dt < startDt.AddMinutes(30); dt = dt.AddSeconds(1))
            {
                Random ran = new Random((int)tick * i);
                results.Add(new Result
                {
                    Value = ran.NextDouble(),
                    Seconds = i
                });
                tick += (new Random((int)tick).Next(978));
                i++;
            }
            return results;
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        public override void Init()
        {
            if (Inited)
            {
                return;
            }
            base.Init();

            //var voltageDemoResults = InitDemoResults();
            //VoltageResults = new ObservableCollection<Result>(voltageDemoResults);
            //var electricityDemoResults = InitDemoResults();
            //ElectricityResults = new ObservableCollection<Result>(electricityDemoResults);
            //var ohmDemoResults = InitDemoResults();
            //OhmResults = new ObservableCollection<Result>(ohmDemoResults);
            Cards = new ObservableCollection<Card>();
            Cache.Instance.Cards.ForEach(c => Cards.Add(c));

            VoltageResults = new ObservableCollection<Result>();
            ElectricityResults = new ObservableCollection<Result>();
            OhmResults = new ObservableCollection<Result>();


            return;
            //voltageResults.Add(new Result
            //{
            //    Value = 1.0,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 0)
            //});
            //voltageResults.Add(new Result
            //{
            //    Value = 1.5,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 2)
            //});
            //voltageResults.Add(new Result
            //{
            //    Value = 1.2,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 4)
            //});
            //voltageResults.Add(new Result
            //{
            //    Value = 0.8,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 8)
            //});
            //voltageResults.Add(new Result
            //{
            //    Value = 0.5,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 10)
            //});


            //electricityResults.Add(new Result
            //{
            //    Value = 0.1,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 0)
            //});
            //electricityResults.Add(new Result
            //{
            //    Value = 0.2,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 2)
            //});
            //electricityResults.Add(new Result
            //{
            //    Value = 0.5,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 4)
            //});
            //electricityResults.Add(new Result
            //{
            //    Value = 0.05,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 8)
            //});
            //electricityResults.Add(new Result
            //{
            //    Value = 0.3,
            //    Seconds = new DateTime(2015, 12, 22, 17, 30, 10)
            //});
        }

        public void OnBtnPre_Click(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Common.SelectedDateTime = Common.SelectedDateTime.AddDays(-1);
                List<Result> listE = new List<Result>();
                List<Result> listV = new List<Result>();
                List<Result> listO = new List<Result>();
                List<Result> listCCO = new List<Result>();
                List<Result> listZXCC = new List<Result>();
                bool bRet = GetChartResultsEx(ref listE, ref listV, ref listO, ref listCCO, ref listZXCC);
                if (bRet == false)
                {

                    VoltageResults = new ObservableCollection<Result>();

                    ElectricityResults = new ObservableCollection<Result>();

                    OhmResults = new ObservableCollection<Result>();

                    CCOhmResults = new ObservableCollection<Result>();

                    ZXCCOhmResults = new ObservableCollection<Result>();
                    return;
                }
                VoltageResults = null;
                VoltageResults = new ObservableCollection<Result>(listV);
                ElectricityResults = null;
                ElectricityResults = new ObservableCollection<Result>(listE);
                OhmResults = null;
                OhmResults = new ObservableCollection<Result>(listO);
                CCOhmResults = null;
                CCOhmResults = new ObservableCollection<Result>(listCCO);
                ZXCCOhmResults = null;
                ZXCCOhmResults = new ObservableCollection<Result>(listZXCC);

                if (listE != null && listE.Count > 0)
                {
                    listE.Clear();
                }
                if (listV != null && listV.Count > 0)
                {
                    listV.Clear();
                }
                if (listO != null && listO.Count > 0)
                {
                    listO.Clear();
                }
                if (listCCO != null && listCCO.Count > 0)
                {
                    listCCO.Clear();
                }
                if (listZXCC != null && listZXCC.Count > 0)
                {
                    listZXCC.Clear();
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-OnBtnPre_Click:" + ex.Message);
            }

        }

        public void OnBtnNext_Click(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Common.SelectedDateTime = Common.SelectedDateTime.AddDays(1);
                List<Result> listE = new List<Result>();
                List<Result> listV = new List<Result>();
                List<Result> listO = new List<Result>();
                List<Result> listCCO = new List<Result>();
                List<Result> listZXCC = new List<Result>();
                bool bRet = GetChartResultsEx(ref listE, ref listV, ref listO, ref listCCO, ref listZXCC);
                if (bRet == false)
                {
                    VoltageResults = new ObservableCollection<Result>();
                    ElectricityResults = new ObservableCollection<Result>();
                    OhmResults = new ObservableCollection<Result>();
                    CCOhmResults = new ObservableCollection<Result>();
                    ZXCCOhmResults = new ObservableCollection<Result>();
                    return;
                }
                VoltageResults = null;
                VoltageResults = new ObservableCollection<Result>(listV);
                ElectricityResults = null;
                ElectricityResults = new ObservableCollection<Result>(listE);
                OhmResults = null;
                OhmResults = new ObservableCollection<Result>(listO);
                CCOhmResults = new ObservableCollection<Result>(listCCO);
                ZXCCOhmResults = new ObservableCollection<Result>(listZXCC);
                if (listE != null && listE.Count > 0)
                {
                    listE.Clear();
                }
                if (listV != null && listV.Count > 0)
                {
                    listV.Clear();
                }
                if (listO != null && listO.Count > 0)
                {
                    listO.Clear();
                }
                if (listCCO != null && listCCO.Count > 0)
                {
                    listCCO.Clear();
                }
                if (listZXCC != null && listZXCC.Count > 0)
                {
                    listZXCC.Clear();
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-OnBtnNext_Click:" + ex.Message);
            }

        }
        //第一页
        public void OnDtpSelectChanged(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                HistoryTimer.CurrentPage = 0;
                List<Result> listE = new List<Result>();
                List<Result> listV = new List<Result>();
                List<Result> listO = new List<Result>();
                List<Result> listCCO = new List<Result>();
                List<Result> listZXCC = new List<Result>();
                bool bRet = GetChartResultsEx(ref listE, ref listV, ref listO, ref listCCO, ref listZXCC);
                if (bRet == false)
                {
                    VoltageResults = new ObservableCollection<Result>();
                    ElectricityResults = new ObservableCollection<Result>();
                    OhmResults = new ObservableCollection<Result>();
                    CCOhmResults = new ObservableCollection<Result>();
                    ZXCCOhmResults = new ObservableCollection<Result>();
                    return;
                }
                VoltageResults = null;
                VoltageResults = new ObservableCollection<Result>(listV);
                ElectricityResults = null;
                ElectricityResults = new ObservableCollection<Result>(listE);
                OhmResults = null;
                OhmResults = new ObservableCollection<Result>(listO);
                CCOhmResults = new ObservableCollection<Result>(listCCO);

                ZXCCOhmResults = new ObservableCollection<Result>(listZXCC);
                if (listE != null && listE.Count > 0)
                {
                    listE.Clear();
                }
                if (listV != null && listV.Count > 0)
                {
                    listV.Clear();
                }
                if (listO != null && listO.Count > 0)
                {
                    listO.Clear();
                }
                if (listCCO != null && listCCO.Count > 0)
                {
                    listCCO.Clear();
                }
                if (listZXCC != null && listZXCC.Count > 0)
                {
                    listZXCC.Clear();
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteInfoLog("ResultPageVM-OnDtpSelectChanged:" + ex.Message);
            }

        }
        //将数据文件转移到历史文件夹中，命名采用年月日十分秒
        public void BtnMoveDataToHistory(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                HistoryTimer.SaveHisTimer.Stop();
                HistoryTimer.ClearCacheTimer.Stop();
                string oldpath = Directory.GetCurrentDirectory() + @"\Data";
                string newpath = Directory.GetCurrentDirectory() + @"\History\" + DateTime.Now.ToString("yyyyMMddHHmmss");
                Common.CopyDir(oldpath, newpath);
                Common.DeleteDir(oldpath);
                System.Windows.Forms.MessageBox.Show("数据转移成功");
                HistoryTimer.SaveHisTimer.Start();
                HistoryTimer.ClearCacheTimer.Start();
            }
            catch (Exception ex)
            {

                LogHelper.WriteInfoLog("ResultPageVM-BtnMoveDataToHistory:" + ex.Message);
            }

        }
        /// <summary>
        /// 接受到通知时的处理
        /// </summary>
        /// <param name="sender">通知者对象</param>
        /// <param name="e">通知者发送通知时的事件参数</param>
        public override void Receive(object sender, Observer.NotifyEventArgs e)
        {
            switch (e.MessageTypes)
            {
                case LifeTester.Observer.MessageTypes.UPDATE_DZ_SKP1://SKP
                    DZ1 = e.ParamObject.ToString();
                    break;
                case LifeTester.Observer.MessageTypes.UPDATE_DZ_SKP2://SKP
                    DZ2 = e.ParamObject.ToString();
                    break;
                case LifeTester.Observer.MessageTypes.UPDATE_ZX_SKP1://SKP
                    ZX1 = e.ParamObject.ToString();
                    break;
                case LifeTester.Observer.MessageTypes.UPDATE_ZX_SKP2://SKP
                    ZX2 = e.ParamObject.ToString();
                    break;

                default:
                    break;
            }
        }
    }
}
