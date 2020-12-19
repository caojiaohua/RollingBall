﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class goldUpgrade
    {
        /// <summary>
        /// 金币技能等级
        /// </summary>
        public int goldLevel;

        /// <summary>
        /// 小球行走单位路程（0.01%）获得金币
        /// </summary>
        public float distanceIncome;

        /// <summary>
        /// 击杀一个敌人可以获得的金币
        /// </summary>
        public float killEnemyIncome;
        
        /// <summary>
        /// 升级消耗
        /// </summary>
        public int price;
    }
}
