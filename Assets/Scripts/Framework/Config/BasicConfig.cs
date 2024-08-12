namespace Framework.Config
{
    // 基础配置项
    public static partial class Config
    {
        /// <summary>
        /// 预制体路径
        /// </summary>
        public const string PrefabPath = "Prefabs\\";

        /// <summary>
        /// 对象池激活状态对象的父对象的名字
        /// </summary>
        public const string ObjectPoolActiveParent = "ObjectPoolActiveList";

        /// <summary>
        /// 对象池非激活状态对象的父对象的名字
        /// </summary>
        public const string ObjectPoolNoActiveParent = "ObjectPoolNoActiveParent";
    }
}