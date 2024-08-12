using Runtime.Data;

namespace Runtime.Utils
{
    public static class TranslateUtil
    {
        public static string TranslateUi(HeroType heroType)
        {
            switch(heroType)
            {
                case HeroType.CaoHong:
                    return "曹洪";
                case HeroType.CaoZhang:
                    return "曹彰";
                case HeroType.ChenGong:
                    return "陈宫";
                case HeroType.ChengPu:
                    return "程普";
                case HeroType.ChengYuanZhi:
                    return "程远志";
                case HeroType.DaQiao:
                    return "大乔";
                case HeroType.FaZheng:
                    return "法正";
                case HeroType.GanNing:
                    return "甘宁";
                case HeroType.GaoShun:
                    return "高顺";
                case HeroType.GuanYinPing:
                    return "关银屏";
                case HeroType.GuanYu:
                    return "关羽";
                case HeroType.GuoJia:
                    return "郭嘉";
                case HeroType.HuangGai:
                    return "黄盖";
                case HeroType.HuangZhong:
                    return "黄忠";
                case HeroType.JiangWei:
                    return "姜维";
                case HeroType.JiaXu:
                    return "贾诩";
                case HeroType.LiDian:
                    return "李典";
                case HeroType.LingTong:
                    return "凌统";
                case HeroType.LiuBei:
                    return "刘备";
                case HeroType.LuJi:
                    return "陆绩";
                case HeroType.LuSu:
                    return "鲁肃";
                case HeroType.LvBu:
                    return "吕布";
                case HeroType.LvMeng:
                    return "吕蒙";
                case HeroType.MaDai:
                    return "马岱";
                case HeroType.MaSu:
                    return "马谡";
                case HeroType.PangTong:
                    return "庞统";
                case HeroType.SunShangXiang:
                    return "孙尚香";
                case HeroType.TaiShiCi:
                    return "太史慈";
                case HeroType.WenChou:
                    return "文丑";
                case HeroType.XiaHouDun:
                    return "夏侯惇";
                case HeroType.XiaoQiao:
                    return "小乔";
                case HeroType.XuChu:
                    return "许褚";
                case HeroType.XunYu:
                    return "荀彧";
                case HeroType.XuShu:
                    return "徐庶";
                case HeroType.YanLiang:
                    return "颜良";
                case HeroType.YuJin:
                    return "于禁";
                case HeroType.ZhangFei:
                    return "张飞";
                case HeroType.ZhangJiao:
                    return "张角";
                case HeroType.ZhaoYun:
                    return "赵云";
                case HeroType.ZhenJi:
                    return "甄姬";
                case HeroType.ZhouYu:
                    return "周瑜";
                case HeroType.ZhuGeLiang:
                    return "诸葛亮";
                case HeroType.ZhuRong:
                    return "祝融";
                case HeroType.LiuShan:
                default:
                    return "刘禅";
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