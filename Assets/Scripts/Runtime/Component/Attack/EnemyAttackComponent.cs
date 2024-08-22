using Runtime.Manager;
using UnityEngine;

/// <summary>
/// 敌人攻击
/// </summary>
public class EnemyAttackComponent : AttackComponent
{
    public bool IsInAttackInterval { get; set; }
    public float LastAttackTime { get; set; }
    public float AttackInterval { get; set; }
    
    private EnemyEntity entity;

    private readonly int hurt;
    
    /// <summary>
    /// 点检测组件
    /// </summary>
    private PointDetectComponent pointDetectComponent;

    public EnemyAttackComponent(float attackInterval, int hurt, EnemyEntity entity, PointDetectComponent pointDetectComponent)
    {
        AttackInterval = attackInterval;
        this.entity = entity;
        this.hurt = hurt;
        IsInAttackInterval = false;
        this.pointDetectComponent = pointDetectComponent;
    }

    public void Tick(float time)
    {
        if (IsInAttackInterval)
        {
            // 攻击间隔的时间减去当前时间 如果大于攻击间隔时间 则证明攻击间隔时间结束了 那么就需要退出攻击间隔状态  所以 IsInAttackInterval 此时等于 false
            IsInAttackInterval = !(Time.time - LastAttackTime >= AttackInterval);
        }

        if (pointDetectComponent.IsVeryClose())
        {
            Attack(1, pointDetectComponent.GetTarget().position);
        }
    }

    public void Release()
    {
        
    }
    
    public void Attack(float time, Vector2 point)
    {
        if (IsInAttackInterval) return;
        var bullet = Object.Instantiate(AssetsLoadManager.Load<GameObject>("Prefabs/Small_EnemyAttackBox"));
        var bulletEnemy = bullet.AddComponent<BulletEntity>();
        bulletEnemy.Init();
        bulletEnemy.InitBullet(EntityType.HeroEntity, hurt, 1, entity.GetComponent<RectTransform>(),
            BattleManager.Instance.GetBulletParent());
        entity.GetSpecifyComponent<EnemyStateMachineComponent>(ComponentType.StateMachineComponent)
            .TryChangeState(StateType.Attack);
        LastAttackTime = Time.time;
        IsInAttackInterval = true;
    }
}
