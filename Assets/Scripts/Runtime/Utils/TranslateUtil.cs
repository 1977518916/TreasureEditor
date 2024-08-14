using Runtime.Data;

namespace Runtime.Utils
{
    public static class TranslateUtil
    {
        public static string TranslateUi(HeroTypeEnum heroTypeEnum)
        {
            switch(heroTypeEnum)
            {
                case HeroTypeEnum.CaoHong:
                    return "曹洪";
                case HeroTypeEnum.CaoZhang:
                    return "曹彰";
                case HeroTypeEnum.ChenGong:
                    return "陈宫";
                case HeroTypeEnum.ChengPu:
                    return "程普";
                case HeroTypeEnum.ChengYuanZhi:
                    return "程远志";
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
                case HeroTypeEnum.LiDian:
                    return "李典";
                case HeroTypeEnum.LingTong:
                    return "凌统";
                case HeroTypeEnum.LiuBei:
                    return "刘备";
                case HeroTypeEnum.LuJi:
                    return "陆绩";
                case HeroTypeEnum.LuSu:
                    return "鲁肃";
                case HeroTypeEnum.LvBu:
                    return "吕布";
                case HeroTypeEnum.LvMeng:
                    return "吕蒙";
                case HeroTypeEnum.MaDai:
                    return "马岱";
                case HeroTypeEnum.MaSu:
                    return "马谡";
                case HeroTypeEnum.PangTong:
                    return "庞统";
                case HeroTypeEnum.SunShangXiang:
                    return "孙尚香";
                case HeroTypeEnum.TaiShiCi:
                    return "太史慈";
                case HeroTypeEnum.WenChou:
                    return "文丑";
                case HeroTypeEnum.XiaHouDun:
                    return "夏侯惇";
                case HeroTypeEnum.XiaoQiao:
                    return "小乔";
                case HeroTypeEnum.XuChu:
                    return "许褚";
                case HeroTypeEnum.XunYu:
                    return "荀彧";
                case HeroTypeEnum.XuShu:
                    return "徐庶";
                case HeroTypeEnum.YanLiang:
                    return "颜良";
                case HeroTypeEnum.YuJin:
                    return "于禁";
                case HeroTypeEnum.ZhangFei:
                    return "张飞";
                case HeroTypeEnum.ZhangJiao:
                    return "张角";
                case HeroTypeEnum.ZhaoYun:
                    return "赵云";
                case HeroTypeEnum.ZhenJi:
                    return "甄姬";
                case HeroTypeEnum.ZhouYu:
                    return "周瑜";
                case HeroTypeEnum.ZhuGeLiang:
                    return "诸葛亮";
                case HeroTypeEnum.ZhuRong:
                    return "祝融";
                case HeroTypeEnum.LiuShan:
                default:
                    return "刘禅";
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

        public static string TranslateUi(BulletType bulletType)
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