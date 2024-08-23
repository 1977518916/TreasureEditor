using Runtime.Data;

namespace Runtime.Utils
{
    public static class TranslateUtil
    {
        public static string TranslateUi(HeroTypeEnum heroTypeEnum)
        {
            switch(heroTypeEnum)
            {
                case HeroTypeEnum.CaiWenJi:
                    return "蔡文姬";
                case HeroTypeEnum.CaoFang:
                    return "曹芳";
                case HeroTypeEnum.DaQiao:
                    return "大乔";
                case HeroTypeEnum.FaZheng:
                    return "法正";
                case HeroTypeEnum.GanNing:
                    return "甘宁";
                case HeroTypeEnum.GaoShun:
                    return "高顺";
                case HeroTypeEnum.GuanYinPing:
                    return "关银屏";
                case HeroTypeEnum.GuanYu:
                    return "关羽";
                case HeroTypeEnum.GuoJia:
                    return "郭嘉";
                case HeroTypeEnum.HuangGai:
                    return "黄盖";
                case HeroTypeEnum.HuangZhong:
                    return "黄忠";
                case HeroTypeEnum.JiangWei:
                    return "姜维";
                case HeroTypeEnum.JiaXu:
                    return "贾诩";
                case HeroTypeEnum.LingTong:
                    return "凌统";
                case HeroTypeEnum.LuJi:
                    return "陆绩";
                case HeroTypeEnum.LuSu:
                    return "鲁肃";
                // case HeroTypeEnum.LvBu:
                //     return "吕布";
                case HeroTypeEnum.LvMeng:
                    return "吕蒙";
                case HeroTypeEnum.MaDai:
                    return "马岱";
                case HeroTypeEnum.MaSu:
                    return "马谡";
                case HeroTypeEnum.PangTong:
                    return "庞统";
                // case HeroTypeEnum.SunShangXiang:
                //     return "孙尚香";
                case HeroTypeEnum.TaiShiCi:
                    return "太史慈";
                case HeroTypeEnum.WenChou:
                    return "文丑";
                case HeroTypeEnum.XiaoQiao:
                    return "小乔";
                // case HeroTypeEnum.XuChu:
                //     return "许褚";
                case HeroTypeEnum.XunYou:
                    return "荀攸";
                // case HeroTypeEnum.XunYu:
                //     return "荀彧";
                // case HeroTypeEnum.XuShu:
                //     return "徐庶";
                case HeroTypeEnum.YuJin:
                    return "于禁";
                // case HeroTypeEnum.ZhangFei:
                //     return "张飞";
                case HeroTypeEnum.ZhangJiao:
                    return "张角";
                case HeroTypeEnum.ZhaoYun:
                    return "赵云";
                case HeroTypeEnum.ZhenJi:
                    return "甄姬";
                case HeroTypeEnum.ZhouYu:
                    return "周瑜";
                case HeroTypeEnum.ZhuGeJin:
                    return "诸葛瑾";
                case HeroTypeEnum.ZhuGeLiang:
                    return "诸葛亮";
                case HeroTypeEnum.ZhuRong:
                    return "祝融";
                case HeroTypeEnum.Null:
                default:
                    return "无";
            }
        }

        public static string TranslateUi(EnemyTypeEnum enemyTypeEnum)
        {
            switch(enemyTypeEnum)
            {
                case EnemyTypeEnum.DunBing:
                    return "盾兵";
                case EnemyTypeEnum.XiaoBing:
                    return "小兵";
                case EnemyTypeEnum.XiaoBing_Dao:
                    return "刀兵";
                case EnemyTypeEnum.XiaoBing_GongJian:
                    return "弓箭手";
                case EnemyTypeEnum.XiaoBing_Qi:
                    return "骑兵";
                case EnemyTypeEnum.XiaoBing_TouShiChe:
                    return "投石车";
                default:
                    return "";
            }
        }

        public static string TranslateUi(MapTypeEnum mapTypeEnum)
        {
            switch(mapTypeEnum)
            {
                default:
                    return $"地图{(int)mapTypeEnum + 1}";
            }
        }

        public static string TranslateUi(BulletType bulletType)
        {
            switch(bulletType)
            {
                case BulletType.Self:
                default:

                    return "自身子弹";
            }
        }
    }
}