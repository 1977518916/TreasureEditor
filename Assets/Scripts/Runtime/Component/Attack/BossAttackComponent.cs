using Runtime.Data;
using Runtime.Manager;
using Tao_Framework.Core.Event;
using UnityEngine;

namespace Runtime.Component.Attack
{
    public class BossAttackComponent : IComponent
    {
        public bool IsInAttackInterval { get; set; }
        public float LastAttackTime { get; set; }
        public float AttackInterval { get; set; }

        private Entity entity;

        private RectTransform rectTransform;
        private readonly int hurt;
        FixedDistanceComponent distanceComponent;

        private EntityModelType entityModelType;
        private BulletType atkType;
        public BossAttackComponent(float attackInterval, int hurt, Entity entity, RectTransform rectTransform, BulletType dataBulletType, EntityModelType modelType)
        {
            AttackInterval = attackInterval;
            this.entity = entity;
            this.hurt = hurt;
            IsInAttackInterval = false;
            this.rectTransform = rectTransform;
            atkType = dataBulletType;
            entityModelType = modelType;
            distanceComponent = this.entity.GetSpecifyComponent<FixedDistanceComponent>(ComponentType.MoveComponent);
        }

        public void Tick(float time)
        {
            if(IsInAttackInterval)
            {
                // 攻击间隔的时间减去当前时间 如果大于攻击间隔时间 则证明攻击间隔时间结束了 那么就需要退出攻击间隔状态  所以 IsInAttackInterval 此时等于 false
                IsInAttackInterval = !(Time.time - LastAttackTime >= AttackInterval);
            }
            if(!distanceComponent.IsContinueMove())
            {
                Attack();
            }
        }

        public void Release()
        {

        }

        public void Attack()
        {
            if(IsInAttackInterval) return;
            if(atkType == BulletType.Self)
            {
                BulletAttack();
                return;
            }
            MeleeAttack();
        }

        private void BulletAttack()
        {
            LastAttackTime = Time.time;
            IsInAttackInterval = true;
            if(TryGetAtkedEntity(out var hero))
            {
                // 这里需要传入一个子弹的爆炸后的特效,可能是没有的
                entity.GetSpecifyComponent<StateMachineComponent>(ComponentType.StateMachineComponent).TryChangeState(StateType.Attack);
                var bulletGo = AssetsLoadManager.LoadBullet(entityModelType);
                var bulletEntity = EntitySystem.Instance.CreateEntity<BulletEntity>(EntityType.BulletEntity,bulletGo);
                var bulletAttributeType = BulletAttributeType.Penetrate;
                var bulletHurt = DataManager.GameData.isInvicibleSelf ? 1 : hurt;
                var bulletParent = BattleManager.Instance.BulletParent;
                bulletEntity.MoveObject = bulletGo.transform.GetChild(0).gameObject;
                bulletEntity.InitBullet(EntityType.HeroEntity, bulletHurt, bulletAttributeType, rectTransform.anchoredPosition, bulletParent);
                bulletEntity.AllComponentList.Add(new BulletMoveComponent(bulletEntity.GetComponent<RectTransform>(),
                    GetAtkPosition(hero), 800f, BulletMoveType.RectilinearMotion, 2000f));
                bulletEntity.AllComponentList.Add(new DelayedDeadComponent(3f, bulletEntity));
            }
        }

        private void MeleeAttack()
        {
            LastAttackTime = Time.time;
            IsInAttackInterval = true;
            if (TryGetAtkedEntity(out var hero))
            {
                entity.GetSpecifyComponent<EnemyStateMachineComponent>(ComponentType.StateMachineComponent).TryChangeState(StateType.Attack);
                hero.GetSpecifyComponent<HeroStatusComponent>(ComponentType.StatusComponent).Hit(DataManager.GameData.isInvicibleSelf ? 1 : hurt);
            }
        }

        private bool TryGetAtkedEntity(out HeroEntity heroEntity)
        {
            EntitySystem entitySystem = EntitySystem.Instance;
            long id = entitySystem.GetFrontRowHeroID();
            if(id == -1)
            {
                heroEntity = null;
                return false;
            }
            heroEntity = entitySystem.GetEntity(id) as HeroEntity;
            return true;
        }

        private Vector2 GetAtkPosition(HeroEntity hero)
        {
            return hero.transform.position;
        }
    }
}