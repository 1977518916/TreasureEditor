using Runtime.Data;
using UnityEngine.UIElements;

namespace Runtime.Utils
{
    public static class TranslateUtil
    {
        public static string Translate(HeroType heroType)
        {
            switch(heroType)
            {
                case HeroType.HuangZhong:
                    return "黄忠";
                case HeroType.LiuShan:
                default:
                    return "刘禅";
            }
        }

        public static string Translate(BulletType bulletType)
        {
            switch(bulletType)
            {
                case BulletType.Test1:
                    return "测试子弹1";
                case BulletType.Test2:
                default:
                    return "测试子弹2";
            }
        }
    }
}