using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    /// <summary>
    /// 道具类
    /// </summary>
    [Serializable]
    public class prop
    {
        /// <summary>
        /// 道具ID;
        /// </summary>
        public int propId;

        /// <summary>
        /// 道具模型资源名称
        /// </summary>
        public string propSourceName;

        /// <summary>
        /// 描述
        /// </summary>
        public string discription;
        /// <summary>
        ///  购买限额数量/天
        /// </summary>
        public int purchaseLimitNum;

        /// <summary>
        /// 用金币购买的价格
        /// </summary>
        public int gold_Price;

        /// <summary>
        /// 用钻石购买的价格
        /// </summary>
        public int diamond_Price;
    }
}
