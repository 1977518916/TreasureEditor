using System;
using Runtime.Data;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Runtime.Utils
{
    public static class TranslateUtil
    {

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

        public static string TranslateUi(EnemyType enemyType)
        {
            switch(enemyType)
            {
                case EnemyType.Buff:
                    return "buff";
                case EnemyType.Melee:
                    return "近战";
                case EnemyType.Remote:
                default:
                    return "远程";
            }
        }

        public static string TranslateUi(MapTypeEnum mapTypeEnum)
        {
            switch(mapTypeEnum)
            {
                case MapTypeEnum.Other:
                    return $"外部地图";
                default:
                    return $"地图{(int)mapTypeEnum + 1}";
            }
        }
        
        public static string TranslateUi(BulletType bulletType)
        {
            switch(bulletType)
            {
                case BulletType.NoEntity:
                    return "攻击动画";
                case BulletType.Self:
                    return "自身子弹";
                case BulletType.CustomBullet:
                    return "自定义子弹";
            }

            return "";
        }

        public static string TranslateUi(BulletAttributeType attributeType)
        {
            return attributeType switch
            {
                BulletAttributeType.Penetrate => "穿透",
                BulletAttributeType.Rebound => "反弹",
                BulletAttributeType.Refraction => "弹射",
                //BulletAttributeType.Bomb => "爆炸",
                BulletAttributeType.Split => "分裂",
                _ => throw new ArgumentOutOfRangeException(nameof(attributeType), attributeType, null)
            };
        }

        public static string TranslateUi(EntityModelType modelType)
        {
            switch(modelType)
            {
                case EntityModelType.CaiWenJi:
                    return "蔡文姬";
                case EntityModelType.CaoFang:
                    return "曹芳";
                case EntityModelType.DaQiao:
                    return "大乔";
                case EntityModelType.FaZheng:
                    return "法正";
                case EntityModelType.GanNing:
                    return "甘宁";
                case EntityModelType.GaoShun:
                    return "高顺";
                case EntityModelType.GuanYinPing:
                    return "关银屏";
                case EntityModelType.GuanYu:
                    return "关羽";
                case EntityModelType.GuoJia:
                    return "郭嘉";
                case EntityModelType.HuangGai:
                    return "黄盖";
                case EntityModelType.HuangZhong:
                    return "黄忠";
                case EntityModelType.JiangWei:
                    return "姜维";
                case EntityModelType.JiaXu:
                    return "贾诩";
                case EntityModelType.LingTong:
                    return "凌统";
                case EntityModelType.LuJi:
                    return "陆绩";
                case EntityModelType.LuSu:
                    return "鲁肃";
                // case EntityModelType.LvBu:
                //     return "吕布";
                case EntityModelType.LvMeng:
                    return "吕蒙";
                case EntityModelType.MaDai:
                    return "马岱";
                case EntityModelType.MaSu:
                    return "马谡";
                case EntityModelType.PangTong:
                    return "庞统";
                // case EntityModelType.SunShangXiang:
                //     return "孙尚香";
                case EntityModelType.TaiShiCi:
                    return "太史慈";
                case EntityModelType.WenChou:
                    return "文丑";
                case EntityModelType.XiaoQiao:
                    return "小乔";
                // case EntityModelType.XuChu:
                //     return "许褚";
                case EntityModelType.XunYou:
                    return "荀攸";
                // case EntityModelType.XunYu:
                //     return "荀彧";
                // case EntityModelType.XuShu:
                //     return "徐庶";
                case EntityModelType.YuJin:
                    return "于禁";
                // case HeroTypeEnum.ZhangFei:
                //     return "张飞";
                case EntityModelType.ZhangJiao:
                    return "张角";
                case EntityModelType.ZhaoYun:
                    return "赵云";
                case EntityModelType.ZhenJi:
                    return "甄姬";
                case EntityModelType.ZhouYu:
                    return "周瑜";
                case EntityModelType.ZhuGeJin:
                    return "诸葛瑾";
                case EntityModelType.ZhuGeLiang:
                    return "诸葛亮";
                case EntityModelType.ZhuRong:
                    return "祝融";
                case EntityModelType.DongZhuo:
                    return "董卓";
                // case EntityModelType.QingLong:
                //     return "青龙";
                case EntityModelType.Null:
                default:
                    return "无";
            }
        }
    }
}