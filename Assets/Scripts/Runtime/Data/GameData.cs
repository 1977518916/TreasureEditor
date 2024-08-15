using System;

namespace Runtime.Data
{
    [Serializable]
    public class GameData
    {
        /// <summary>
        /// 是否显示伤害飘字
        /// </summary>
        public bool isShowNumber = true;
        /// <summary>
        /// 我方无敌
        /// </summary>
        public bool isInvicibleSelf = false;
        /// <summary>
        /// 敌人无敌
        /// </summary>
        public bool isInvicibleEnemy = false;
    }
}