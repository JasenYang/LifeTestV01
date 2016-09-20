using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LifeTester.Model;

namespace LifeTester.Util
{
    /// <summary>
    /// 提供将结果数据转换成Dataset的方法
    /// </summary>
    public static class ResultDsConvert
    {
        /// <summary>
        /// 电压
        /// </summary>
        public const int VOLTAGE = 0;
        /// <summary>
        /// 电流
        /// </summary>
        public const int ELECTRICITY = 1;
        /// <summary>
        /// 欧姆
        /// </summary>
        public const int OHM = 2;

        /// <summary>
        /// 根据类型将结果转换成DataTable对象
        /// </summary>
        /// <param name="results">结果集合</param>
        /// <param name="type">类型</param>
        /// <returns>返回生成的DataTable对象</returns>
        public static DataTable ToDataTable(IList<Result> results, int type)
        {
            var fieldAndColDic = new Dictionary<string, string>();
            switch (type)
            {
                case VOLTAGE:
                    fieldAndColDic.Add("Value", "电压值(V)");
                    break;
                case ELECTRICITY:
                    fieldAndColDic.Add("Value", "电流值(A)");
                    break;
                case OHM:
                    fieldAndColDic.Add("Value", "电阻值(Ω)");
                    break;
                default:
                    break;
            }
            fieldAndColDic.Add("SecondsShow", "测试时间");

            var dt = ToDataTable(results, fieldAndColDic);
            return dt;
        }

        /// <summary>
        /// 根据结果与字段和列名字典生成DataTable对象
        /// </summary>
        /// <param name="results">结果集合</param>
        /// <param name="fieldAndColDic">字段和列名字典</param>
        /// <returns>返回生成的DataTable对象</returns>
        public static DataTable ToDataTable(IList<Result> results, Dictionary<string, string> fieldAndColDic)
        {
            var dt = new DataTable();
            foreach (var key in fieldAndColDic.Keys)
            {
                var dc = new DataColumn(fieldAndColDic[key], typeof(string));
                dt.Columns.Add(dc);
            }

            foreach (var r in results)
            {
                var dr = dt.NewRow();
                foreach (var key in fieldAndColDic.Keys)
                {
                    dr[fieldAndColDic[key]] = r.GetType().GetProperty(key).GetValue(r, null);
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
