using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeTester.Model
{
    /// <summary>
    /// 校验接口
    /// </summary>
    public interface IVerify
    {
        /// <summary>
        /// 校验数据
        /// </summary>
        /// <returns></returns>
        bool Verify(double val);
    }

    /// <summary>
    /// 电阻判决设置
    /// </summary>
    public class OhmSetting
    {
        public const double MAX_VAL = 4d;
        public const double MIN_VAL = 1000d;

        public OhmSetting()
        {
            Max = Min = double.MaxValue;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public double Min { get; set; }
    }
}
