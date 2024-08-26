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
        public BossAttackComponent(float attackInterval, int hurt, Entity entity, RectTransform rectTransform, BulletType dataBulletType)
        {
            AttackInterval = attackInterval;
            this.entity = entity;
            this.hurt = hurt;
            IsInAttackInterval = false;
            this.rectTransform = rectTransform;
            atkType = dataBulletType;
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
            // 这里需要传入一个子弹的爆炸后的特效,可能是没有的
            entity.GetSpecifyComponent<StateMachineComponent>(ComponentType.StateMachineComponent).TryChangeState(StateType.Attack);
            var bulletEntity = AssetsLoadManager.LoadBullet(entityModelType);
            var bulletHurt = DataManager.GameData.isInvicibleSelf ? 1 : hurt;
            // 先初始化 再添加组件
            bulletEntity.InitBullet(EntityType.EnemyEntity, bulletHurt, 2, rectTransform,
                BattleManager.Instance.GetBulletParent());
            bulletEntity.AllComponentList.Add(new BulletMoveComponent(bulletEntity.GetComponent<RectTransform>(), 800f,
                GetAtkPosition(GetAtkedEntity()), BulletMoveType.SingleTargetMove));
            EntitySystem.Instance.AddEntity(bulletEntity.EntityId, bulletEntity);
            LastAttackTime = Time.time;
            IsInAttackInterval = true;
        }

        private void MeleeAttack()
        {
            entity.GetSpecifyComponent<EnemyStateMachineComponent>(ComponentType.StateMachineComponent).TryChangeState(StateType.Attack);
            GetAtkedEntity().GetSpecifyComponent<HeroStatusComponent>(ComponentType.StatusComponent).Hit(hurt);
            LastAttackTime = Time.time;
            IsInAttackInterval = true;
        }

        private HeroEntity GetAtkedEntity()
        {
            EntitySystem entitySystem = EntitySystem.Instance;
            return entitySystem.GetEntity(entitySystem.GetFrontRowHeroID()) as HeroEntity;
        }

        private Vector2 GetAtkPosition(HeroEntity hero)
        {
            return hero.transform.position;
        }
    }
}