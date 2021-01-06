using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    /// <summary>
    /// 小球移动速度削弱等级表
    /// </summary>
    [Serializable]
    public class ballSkills
    {
        /// <summary>
        /// 削弱等级
        /// </summary>
        public int level;

        /// <summary>
        /// 速度削减
        /// </summary>
        public double speedDown;


        /// <summary>
        /// 能力升级花费
        /// </summary>
        public int price;
    }
}
