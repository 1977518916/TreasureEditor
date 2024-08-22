using System;
using Runtime.Data;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Runtime.Manager
{
    public static class AssetsLoadManager
    {
        private const string CharacterPath = "Character/";
        private const string EffectPath = "Effect/Spine/";
        public static GameObject LoadHero(HeroTypeEnum heroTypeEnum, Transform parent)
        {
            return LoadCharacterSkeletonOfEnum(heroTypeEnum, parent).GameObject();
        }

        public static GameObject LoadEnemy(EnemyTypeEnum enemyTypeEnum, Transform parent)
        {
            return LoadCharacterSkeletonOfEnum(enemyTypeEnum, parent).GameObject();
        }

        public static BulletEntity LoadBullet(HeroData heroData, Transform parent = null)
        {
            if(heroData.bulletType == BulletType.Self)
            {
                GameObject gameObject = new GameObject("bullet",typeof(RectTransform));
                gameObject.transform.SetParent(parent);
                HeroTypeEnum typeEnum = heroData.heroTypeEnum;
                var bulletEntity = gameObject.AddComponent<BulletEntity>();
                bulletEntity.Init();
                string path;
                switch(typeEnum)
                {
                    case HeroTypeEnum.CaiWenJi:
                    case HeroTypeEnum.CaoFang:
                    case HeroTypeEnum.DaQiao:
                    case HeroTypeEnum.GuanYinPing:
                    case HeroTypeEnum.JiangWei:
                    case HeroTypeEnum.JiaXu:
                    case HeroTypeEnum.TaiShiCi:
                    case HeroTypeEnum.WenChou:
                    case HeroTypeEnum.XunYou:
                    case HeroTypeEnum.YuJin:
                    case HeroTypeEnum.ZhuGeJin:
                    case HeroTypeEnum.ZhuRong:
                    case HeroTypeEnum.HuangGai:
                        path = $"{typeEnum.ToString().ToLower()}/fx_{typeEnum.ToString().ToLower()}_attack_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;
                    case HeroTypeEnum.FaZheng:
                        path = $"attack/{typeEnum.ToString()}/fx_{typeEnum.ToString()}_attack2_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        path = $"attack/{typeEnum.ToString()}/fx_{typeEnum.ToString()}_hit_skeletondata";
                        bulletEntity.BoomObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;
                    case HeroTypeEnum.GanNing:
                        path = $"attack/{typeEnum.ToString()}/fx_{typeEnum.ToString()}1_attack_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;
                    case HeroTypeEnum.GaoShun:
                    case HeroTypeEnum.GuanYu:
                    case HeroTypeEnum.LvBu:
                    case HeroTypeEnum.LvMeng:
                        path = $"attack/{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_attack_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;
                    case HeroTypeEnum.GuoJia:
                    case HeroTypeEnum.LuSu:
                    case HeroTypeEnum.MaDai:
                    case HeroTypeEnum.ZhaoYun:
                    case HeroTypeEnum.ZhenJi:
                        path = $"attack/{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_attack_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        path = $"attack/{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_hit_skeletondata";
                        bulletEntity.BoomObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;
                    case HeroTypeEnum.ZhangJiao:
                    case HeroTypeEnum.ZhouYu:
                    case HeroTypeEnum.ZhuGeLiang:
                        path = $"attack/{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_attack_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        path = $"attack/{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_hit_skeletondata";
                        bulletEntity.BoomObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;
                    case HeroTypeEnum.LingTong:
                        path = $"attack/{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_attack_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        path = $"{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_hit_skeletondata";
                        bulletEntity.BoomObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;
                    case HeroTypeEnum.XiaoQiao:
                        path = $"attack/{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_attack_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        path = $"{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_hit_skeletondata";
                        bulletEntity.BoomObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;
                    case HeroTypeEnum.LuJi:
                        path = $"{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_attack_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        path = $"{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_hit_skeletondata";
                        bulletEntity.BoomObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;
                    case HeroTypeEnum.SunShangXiang: //资源路径最奇怪的一个
                        path = CharacterPath + $"Fx{typeEnum.ToString()}_attack_skeletondata";
                        bulletEntity.MoveObject = LoadSkeletonGraphic(path, parent).GameObject();
                        break;
                    case HeroTypeEnum.HuangZhong:
                    case HeroTypeEnum.PangTong:
                    default:
                        path = $"{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_attack_skeletondata";
                        bulletEntity.MoveObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        path = $"{typeEnum.ToString()}/Fx_{typeEnum.ToString()}_hit_skeletondata";
                        bulletEntity.BoomObject = LoadBulletSkeletonOfEnum(heroData.heroTypeEnum, path, gameObject.transform).GameObject();
                        break;

                }
                bulletEntity.transform.localScale = Vector3.one;
                bulletEntity.transform.localPosition = Vector3.zero;
                return bulletEntity;
            }
            return null;
        }

        public static Sprite LoadBg(MapTypeEnum mapTypeEnum)
        {
            return Resources.Load<Sprite>(DataManager.MapTexturePath + ((int)mapTypeEnum + 1));
        }

        public static SkeletonGraphic LoadSkeletonGraphic(string path, Transform parent = null)
        {
            SkeletonDataAsset asset = Load<SkeletonDataAsset>(path);
            SkeletonGraphic skeletonGraphic = SkeletonGraphic.NewSkeletonGraphicGameObject(asset, parent, Graphic.defaultGraphicMaterial);
            return skeletonGraphic;
        }

        private static SkeletonGraphic LoadCharacterSkeletonOfEnum(Enum @enum, Transform parent)
        {
            string path = CharacterPath + $"{@enum.ToString()}/{@enum.ToString()}_skeletondata";
            SkeletonGraphic skeletonAnimation = LoadSkeletonGraphic(path, parent);
            skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
            skeletonAnimation.name = @enum.ToString();
            return skeletonAnimation;
        }

        private static SkeletonGraphic LoadBulletSkeletonOfEnum(Enum @enum, string path, Transform parent = null)
        {
            string p = EffectPath + path;
            SkeletonGraphic skeletonAnimation = LoadSkeletonGraphic(p, parent);
            skeletonAnimation.name = @enum.ToString();
            skeletonAnimation.MatchRectTransformWithBounds();
            skeletonAnimation.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            return skeletonAnimation;
        }



        public static T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}