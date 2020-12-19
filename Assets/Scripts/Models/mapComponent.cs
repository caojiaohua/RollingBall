using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    /// <summary>
    /// 地图组件类
    /// </summary>
    [Serializable]
    public class mapComponent
    {
        /// <summary>
        /// 组件类型
        /// </summary>
        public int mapComponentType;

        /// <summary>
        /// 描述
        /// </summary>
        public string discription;

        /// <summary>
        /// 组件难度
        /// </summary>
        public int componentDifficulty;

        /// <summary>
        /// 组件资源名称
        /// </summary>
        public string componentResourceName;

    }
}
