using System;
using System.Collections.Generic;
using Runtime.Component.Skill;
using Runtime.Data;
using Runtime.Manager;
using Runtime.Utils;
using Spine.Unity;
using Tao_Framework.Core.Event;
using Tao_Framework.Core.Singleton;
using UnityEngine;
using UnityEngine.Animations;
using UnityTimer;

namespace Runtime.System
{
    public class SkillSystem : MonoSingleton<SkillSystem>
    {
        private Timer timer;
        private List<HeroEntity> heroEntities;

        private void Start()
        {
            heroEntities = EntitySystem.Instance.GetAllHeroEntity();
            EventMgr.Instance.RegisterEvent<KeyCode>(GetHashCode(), GameEvent.InvokeSkill, HandEvent);
        }


        private void HandEvent(KeyCode key)
        {
            switch(key)
            {
                case KeyCode.Alpha0:
                case KeyCode.Alpha1:
                case KeyCode.Alpha2:
                case KeyCode.Alpha3:
                case KeyCode.Alpha4:
                case KeyCode.Alpha5:
                case KeyCode.Alpha6:
                case KeyCode.Alpha7:
                case KeyCode.Alpha8:
                case KeyCode.Alpha9:
                    Debug.Log($"按下{key}");
                    int keyCodeValue = (int)key + (key == KeyCode.Alpha0 ? 58 : 0) - 49;
                    HeroSkillComponent skillComponent = GetHeroSkillOfPosition((DataType.HeroPositionType)(keyCodeValue / 2));
                    if(skillComponent != null)
                    {
                        int skillId = keyCodeValue % 2;
                        Debug.Log(TranslateUtil.TranslateUi(skillComponent.entity.GetHeroData().modelType) + "释放技能" + skillId);
                        skillComponent.UseSkill(skillId);
                    }
                    break;
            }
        }

        private HeroSkillComponent GetHeroSkillOfPosition(DataType.HeroPositionType positionType)
        {
            foreach (HeroEntity heroEntity in heroEntities)
            {
                HeroSkillComponent skillComponent = heroEntity.GetSpecifyComponent<HeroSkillComponent>(ComponentType.SkillComponent);
                if(skillComponent.positionType == positionType)
                {
                    return skillComponent;
                }
            }
            return null;
        }


        public void ShowSkill(float triggerTime, SkillData skillData, HeroEntity entity)
        {
            Timer.Register(triggerTime, () =>
            {
                skillData = new SkillData();
                var skeleton = GetSkeletonGraphic(entity);
                switch(skillData.skillMoveType)
                {
                    case SkillMoveType.Bullet:
                        MakeBullet(skeleton, entity);
                        break;
                }
            });
        }

        private SkeletonGraphic GetSkeletonGraphic(HeroEntity entity)
        {
            //todo 获取技能表现，暂时没有用子弹代替
            return AssetsLoadManager.LoadBulletSkeletonOfEnum(EntityModelType.CaiWenJi);
        }

        private void MakeBullet(SkeletonGraphic skeleton, HeroEntity entity)
        {
            HeroData heroData = entity.GetHeroData();
            BulletEntity bulletEntity = skeleton.gameObject.AddComponent<BulletEntity>();
            bulletEntity.InitBullet(EntityType.EnemyEntity, heroData.atk, BulletAttributeType.Penetrate, entity.GetFireLocation(), BattleManager.Instance.GetBulletParent());
        }

        private void MakeCure(bool isMult)
        {
            foreach (HeroEntity heroEntity in heroEntities)
            {
                
            }
        }
    }
}