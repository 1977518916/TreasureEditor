using UnityEngine;

/// <summary>
/// 影响远程攻击组件
/// </summary>
public class EnemyRemoteAttackComponent : AttackComponent
{
    public bool IsInAttackInterval { get; set; }

    public float LastAttackTime { get; set; }

    public float AttackInterval { get; set; }
    
    /// <summary>
    /// 伤害值
    /// </summary>
    private readonly int hurt;
    
    /// <summary>
    /// 攻击组件持有者
    /// </summary>
    private EnemyEntity entity;
    
    /// <summary>
    /// 点检测组件
    /// </summary>
    private PointDetectComponent detect;

    /// <summary>
    /// 敌人远程攻击组件
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="hurtValue"></param>
    /// <param name="e"></param>
    /// <param name="detectComponent"></param>
    public EnemyRemoteAttackComponent(float interval, int hurtValue, EnemyEntity e, PointDetectComponent detectComponent)
    {
        AttackInterval = interval;
        hurt = hurtValue;
        entity = e;
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
        
    }
}
