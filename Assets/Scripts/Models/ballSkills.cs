using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    /// <summary>
    /// 小球能力升级配置表
    /// </summary>
    [Serializable]
    public class ballSkills
    {
        /// <summary>
        /// 小球能力等级
        /// </summary>
        public int level;

        /// <summary>
        /// 速度（cm/s）
        /// </summary>
        public double speed;
        /// <summary>
        /// 力量
        /// </summary>
        public double power;
        /// <summary>
        /// 能力升级花费
        /// </summary>
        public int price;
    }
}
