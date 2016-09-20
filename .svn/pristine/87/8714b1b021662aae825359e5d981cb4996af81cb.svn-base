using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeTester.Model
{
    public class Instruction
    {
        public const string START_CHANNEL = "01";      //启动某通道
        public const string STOP_CHANNEL = "02";       //停止某通道
        public const string RES_CHANNEL = "FF";        //需要回复
        public const string NO_RES_CHANNEL = "00";     //不需要回复
       

        public Instruction()
        {
            Header = "AA";
            InstType = InstType.Control;
            End = "FF";
        }

        /// <summary>
        /// 数据头
        /// </summary>
        public string Header { get; private set; }

        /// <summary>
        /// 板卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 控制/查询
        /// </summary>
        public InstType InstType { get; set; }

        /// <summary>
        /// 数据尾
        /// </summary>
        public string End { get; private set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Cmd { get; set; }

        public override string ToString()
        {
            StringBuilder sbInst = new StringBuilder();
            sbInst.AppendFormat("{0}{1}{2}", Header,
                CardNumber.Length == 1 ? "0" + CardNumber : CardNumber, InstType == InstType.Control ? "01" : "02");
            //数据头+板卡号+指令类型
            sbInst.Append(Cmd);//数据头+板卡号+指令类型+10个通道的控制字
            sbInst.Append(End);//数据头+板卡号+指令类型+10个通道的控制字+结尾
            return sbInst.ToString();//指令转化成可以下发的数据内容
        }
    }

    /// <summary>
    /// 指令类型
    /// </summary>
    public enum InstType
    {
        /// <summary>
        /// 控制指令
        /// </summary>
        Control,
        /// <summary>
        /// 查询指令
        /// </summary>
        Query,
    }
}
