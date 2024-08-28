using Runtime.Data;
using Runtime.Manager;
using Spine.Unity;
using Tao_Framework.Core.Singleton;
using UnityTimer;

namespace Runtime.System
{
    public class SkillSystem : MonoSingleton<SkillSystem>
    {
        private Timer timer;
//todo 英雄技能监听释放从这里触发
        public void ShowSkill(float triggerTime, SkillData skillData, HeroEntity entity)
        {
            Timer.Register(triggerTime, () =>
            {
                var skeleton = GetSkeletonGraphic(entity);
                switch(skillData.skillMoveType)
                {
                    case SkillMoveType.Bullet:
                        MakeBullet(skeleton,entity);
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
            bulletEntity.InitBullet(EntityType.EnemyEntity, heroData.atk, 9999, 
                entity.GetFireLocation(), BattleManager.Instance.GetBulletParent());

        }
    }
}