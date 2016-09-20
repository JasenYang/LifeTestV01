using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace LifeTester.Model
{
    public class SerialConfig
    {
        public string CardNumber { get; set; }

        /// <summary>
        /// 获取或设置端口的通讯，包括但不是限于所有可用的 COM 端口。
        /// Gets or sets the port for communications, including but not limited to all available COM ports.
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 获取或设置串行波特率。
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 获取或设置每个字节的数据位的标准长度。
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// 获取或设置 System.IO.Ports.SerialPort 输入缓冲区的大小。
        /// </summary>
        public int ReadBufferSize { get; set; }

        /// <summary>
        ///获取或设置读取的操作未完成时发生超时之前的毫秒数
        /// </summary>
        public int ReadTimeout { get; set; }

        /// <summary>
        /// 获取或设置每个字节的停止位数标准数目。
        /// </summary>
        public StopBits StopBits { get; set; }

        /// <summary>
        /// 获取或设置串行端口输出缓冲区的大小。
        /// </summary>
        public int WriteBufferSize { get; set; }

        /// <summary>
        ///获取或设置写入操作未完成时发生超时之前的毫秒数
        /// operation does not finish.
        /// </summary>
        public int WriteTimeout { get; set; }

        /// <summary>
        ///  获取或设置奇偶校验检查协议。
        /// </summary>
        public Parity Parity { get; set; }
    }
}
