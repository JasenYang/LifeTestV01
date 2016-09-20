using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public static class LogHelper
{
    private static object obj = new object();
    public static string logfilename { get; set; }
    /// <summary>
    /// 是否写入日志
    /// </summary>
    public static bool isWriteLog = true;


    /// <summary>
    /// 写消息日志
    /// </summary>
    /// <param name="_logstr"></param>
    public static void WriteInfoLog(string _logstr)
    {
        if (!isWriteLog)
        {
            return;
        }
        try
        {
            lock (obj)
            {

                string _filename = string.Empty;
                if (string.IsNullOrEmpty(logfilename))
                {
                    _filename = DateTime.Now.ToString("yyyyMMdd_HH");
                }
                else
                {
                    _filename = DateTime.Now.ToString("yyyyMMdd_HH") + "_" + logfilename;
                }
                using (StreamWriter sw = new StreamWriter(ExistPath("logs/Info/") + _filename + ".Log", true, Encoding.UTF8))
                {
                   // sw.WriteLine("=============================================================分割线=============================================================");
                    sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]:") + _logstr);
                }
            }

        }
        catch (Exception ex)
        {
            LogHelper.WriteErrLog(ex.Message);
            throw;
        }

    }
    /// <summary>
    /// 写消息日志
    /// </summary>
    /// <param name="_logstr"></param>
    /// <param name="_fileName">文件名称</param>
    public static void WriteInfoLog(string _logstr, string _fileName)
    {
      
        using (StreamWriter sw = new StreamWriter( ExistPath("logs/Info/") + _fileName + "_" + DateTime.Now.ToString("yyyyMMddHH") + ".Log", true, Encoding.UTF8))
        {
            sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + "Service:" + _logstr);
        }
    }
    /// <summary>
    /// 写错误日志日志
    /// </summary>
    /// <param name="_logstr"></param>
    public static void WriteErrLog(string _logstr)
    {

        using (StreamWriter sw = new StreamWriter(ExistPath("logs/Error/") + DateTime.Now.ToString("yyyyMMdd_HH") + ".Log", true, Encoding.UTF8))
        {
            sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + _logstr);
        }
    }

    /// <summary>
    /// 判断日志文件夹是否存在，如果存在，返回文件夹路径，如果不存在创建文件夹
    /// </summary>
    /// <param name="_path">文件夹路径</param>
    /// <returns></returns>
    public static string ExistPath(string _path)
    {
        string exePath = System.AppDomain.CurrentDomain.BaseDirectory;
        if (!Directory.Exists(exePath + _path))
        {
            return Directory.CreateDirectory(exePath + _path).FullName;
        }
        return exePath + _path;

    }


}

