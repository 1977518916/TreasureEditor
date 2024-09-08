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

    private readonly EnemyEntity entity;

    private readonly int hurt;
    
    /// <summary>
    /// 点检测组件
    /// </summary>
    private PointDetectComponent detect;

    public EnemyAttackComponent(float interval, int hurtValue, EnemyEntity e, PointDetectComponent detectComponent)
    {
        AttackInterval = interval;
        entity = e;
        hurt = hurtValue;
        IsInAttackInterval = false;
        detect = detectComponent;
    }

    public void Tick(float time)
    {
        if (IsInAttackInterval)
        {
            // 攻击间隔的时间减去当前时间 如果大于攻击间隔时间 则证明攻击间隔时间结束了 那么就需要退出攻击间隔状态  所以 IsInAttackInterval 此时等于 false
            IsInAttackInterval = !(Time.time - LastAttackTime >= AttackInterval);
        }

        if (detect.IsVeryClose())
        {
            Attack(1, detect.GetTarget().anchoredPosition);
        }
    }

    public void Release()
    {
        
    }
    
    public void Attack(float time, Vector2 point)
    {
        if (IsInAttackInterval) return;
        LastAttackTime = Time.time;
        IsInAttackInterval = true;
        
        entity.GetSpecifyComponent<EnemyStateMachineComponent>(ComponentType.StateMachineComponent).TryChangeState(StateType.Attack);
        var target = detect.GetTarget().GetComponent<HeroEntity>();
        var heroStatus = target.GetSpecifyComponent<HeroStatusComponent>(ComponentType.StatusComponent);
        heroStatus.Hit(DataManager.GetRuntimeData().isInvicibleSelf ? 1 : hurt);
    }
}
