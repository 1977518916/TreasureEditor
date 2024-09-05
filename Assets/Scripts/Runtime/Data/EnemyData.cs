using System;

namespace Runtime.Data
{
    [Serializable]
    public class EnemyData : UnitData
    {
        /// <summary>
        /// 模型类型
        /// </summary>
        public EntityModelType modelType;

        /// <summary>
        /// 移动速度
        /// </summary>
        public float speed = 30;

        /// <summary>
        /// 模型大小
        /// </summary>
        public float modelScale = 1f;
    }
}