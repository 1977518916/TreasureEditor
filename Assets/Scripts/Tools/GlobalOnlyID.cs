using UnityEngine;

namespace Tools
{
    public static class GlobalOnlyID
    {
        /// <summary>
        /// 获取全局唯一ID
        /// </summary>
        /// <returns></returns>
        public static long GetGlobalOnlyID()
        {
            // 使用当前时间戳作为基础
            long timestamp = (long)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;

            // 生成随机数作为后缀
            int randomSuffix = Random.Range(10000, 99999); // 生成一个五位数的随机数

            // 合并时间戳和随机数以生成唯一标识符
            long uniqueNumericIdentifier = timestamp * 100000 + randomSuffix; // 将时间戳左移五位，然后加上随机数

            // 将唯一数字标识符转换为字符串并输出
            //Debug.Log("全局唯一ID是: " + uniqueNumericIdentifier);

            return uniqueNumericIdentifier;
        }
    }
}
