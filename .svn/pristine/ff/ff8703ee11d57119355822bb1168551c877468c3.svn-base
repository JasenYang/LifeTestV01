using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using LifeTester.Model;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

namespace LifeTester.Util
{
    /// <summary>
    /// 提供通用的方法
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 根据表达式树获取指定类型对象属性的名称
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">对象的某个属性</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>返回获取到的对象属性名称</returns>
        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            string propertyName = string.Empty;
            if (expression.Body is MemberExpression)
            {
                propertyName = ((MemberExpression)expression.Body).Member.Name;
            }
            else if (expression.Body is UnaryExpression)
            {
                propertyName = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;
            }
            return propertyName;
        }
        public static DateTime SelectedDateTime { get; set; }//用于记录曲线选中的当前日期
        public static ChannelHistory SelectedChannel { get; set; }//用于记录曲线选中的当前channel

        /// <summary>
        /// 将整个文件夹复制到目标文件夹中。
        /// </summary>
        /// <param name="srcPath">源文件夹</param>
        /// <param name="aimPath">目标文件夹</param>
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (Directory.Exists(file))
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    // 否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
            catch
            {
                Console.WriteLine("无法复制!");
            }
        }

        /// <summary>
        /// 将整个文件夹删除。
        /// </summary>
        /// <param name="aimPath">目标文件夹</param>
        public static void DeleteDir(string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(aimPath);
                string[] fileList = Directory.GetFileSystemEntries(aimPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Delete该目录下面的文件
                    if (Directory.Exists(file))
                    {
                        DeleteDir(aimPath + Path.GetFileName(file));
                    }
                    // 否则直接Delete文件
                    else
                    {
                        File.Delete(aimPath + Path.GetFileName(file));
                    }
                }
                //删除文件夹
                //System.IO .Directory .Delete (aimPath,true);
            }
            catch
            {
                Console.WriteLine("无法删除!");
            }
        }

    }
    public class CSVFileHelper
    {
        /// <summary>
        /// 将源文件数据保存成CSV文件
        /// </summary>
        /// <param name="sourcePath">用于读取的原二进制序列化文件</param>
        /// <param name="targetPath">CSV文件，用于保存数据</param>
        public static void SaveCSV(bool isWriteHeader, string sourcePath, string targetPath)
        {
            //从给定的路径读取二进制文件，转换为list
            BinaryFormatter bf = new BinaryFormatter();
            List<ChannelHistory> list = new List<ChannelHistory>();
            //反序列化
            using (FileStream fs = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                //进行反序列化(并且要用前面被序列化的对象的类型接受反序列化的结果)
                ChannelHistory[] tmp = (ChannelHistory[])bf.Deserialize(fs);
                do
                {
                    list.AddRange(tmp);
                    try
                    {
                        tmp = bf.Deserialize(fs) as ChannelHistory[];
                    }
                    catch (Exception ex)
                    {
                        tmp = null;
                    }

                } while (tmp != null && tmp.Length > 0);
            }
            if (list.Count==0)
            {
                return;
            }
            FileInfo fi = new FileInfo(targetPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            //先拷贝成一个临时文件，然后再生成，生成后再把临时文件删除，方式别的程序再写时影响这里的读操作
            //todo
            FileStream fs2 = new FileStream(targetPath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            StreamWriter sw = new StreamWriter(fs2, System.Text.Encoding.UTF8);
            StringBuilder data = new StringBuilder();
            //写出列名称
            if (isWriteHeader)
            {
                data.AppendLine(string.Format("{0},{1},{2},{3},{4}", "板卡号", "通道", "电流", "电压", "电阻"));
            }
            //写出各行数据

            for (int i = 0; i < list.Count; i++)
            {
                data.AppendLine(string.Format("{0},{1},{2},{3},{4}",list[i].CardNumber,list[i].Number,list[i].Electricity,list[i].Voltage,list[i].Ohm));
            }
            sw.WriteLine(data);
            sw.Close();
            fs2.Close();
        }

        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSV(string filePath)
        {
            Encoding encoding = System.Text.Encoding.Default; //Encoding.ASCII;//
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs, encoding);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                //strLine = Common.ConvertStringUTF8(strLine, encoding);
                //strLine = Common.ConvertStringUTF8(strLine);

                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            sr.Close();
            fs.Close();
            return dt;
        }
    }
}
