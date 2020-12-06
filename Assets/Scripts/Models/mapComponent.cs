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
        /// 组件ID
        /// </summary>
        public int mapComponentId;

        /// <summary>
        /// 组件类型
        /// </summary>
        public int mapComponentType;

        /// <summary>
        /// 描述
        /// </summary>
        public string discription;

        /// <summary>
        /// 组件资源名称
        /// </summary>
        public string componentResourceName;

        /// <summary>
        /// 材质1
        /// </summary>
        public int matResources1;

        /// <summary>
        /// 材质2
        /// </summary>
        public int matResources2;

        /// <summary>
        /// 材质3
        /// </summary>
        public int matResources3;



    }
}
