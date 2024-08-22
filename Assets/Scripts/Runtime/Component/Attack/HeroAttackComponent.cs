using DG.Tweening;
using Runtime.Manager;
using Tao_Framework.Core.Event;
using UnityEngine;

/// <summary>
/// 英雄攻击组件
/// </summary>
public class HeroAttackComponent : AttackComponent
{
    public bool IsInAttackInterval { get; set; }
    
    public float LastAttackTime { get; set; }

    public float AttackInterval { get; set; }
    
    /// <summary>
    /// 攻击最大次数 也就是每次CD结束以后恢复的攻击次数
    /// </summary>
    private int attackMaxCount;
    
    /// <summary>
    /// 攻击次数  每次攻击次数归0时进入攻击CD中
    /// </summary>
    private int attackCount;
    
    /// <summary>
    /// 攻击CD
    /// </summary>
    private float attackCd;
    
    /// <summary>
    /// 是否处于攻击CD
    /// </summary>
    private bool isInAttackCd;
    
    /// <summary>
    /// 攻击开始CD的时间
    /// </summary>
    private float attackStartCdTime;
    
    /// <summary>
    /// 英雄实体
    /// </summary>
    private readonly HeroEntity heroEntity;
    
    /// <summary>
    /// 点检测组件
    /// </summary>
    private PointDetectComponent pointDetectComponent;
    
    /// <summary>
    /// 英雄攻击组件的构造函数
    /// </summary>
    /// <param name="attackCountValue"> 攻击次数总数 </param>
    /// <param name="entity"> 英雄实体 </param>
    /// <param name="attackInterval"> 攻击间隔时间 </param>
    /// <param name="attackCd"> 攻击CD </param>
    /// <param name="pointDetectComponent"></param>
    public HeroAttackComponent(int attackCountValue, float attackInterval, float attackCd, HeroEntity entity, PointDetectComponent pointDetectComponent)
    {
        attackMaxCount = attackCountValue;
        AttackInterval = attackInterval;
        attackCount = attackCountValue;
        heroEntity = entity;
        this.attackCd = attackCd;
        IsInAttackInterval = false;
        isInAttackCd = false;
        this.pointDetectComponent = pointDetectComponent;
    }
    
    public void Tick(float time)
    {
        if (IsInAttackInterval)
        {
            // 攻击间隔的时间减去当前时间 如果大于攻击间隔时间 则证明攻击间隔时间结束了 那么就需要退出攻击间隔状态  所以 IsInAttackInterval 此时等于 false
            IsInAttackInterval = !(Time.time - LastAttackTime >= AttackInterval);
        }
        
        if (isInAttackCd)
        {
            // 攻击开始计时CD的时间减去当前时间 如果大于攻击CD 则证明攻击CD结束了 那么就需要推出CD状态  所以 isInAttackCd 此时等于 false
            isInAttackCd = !(Time.time - attackStartCdTime >= attackCd);
            if (!isInAttackCd)
                attackCount = attackMaxCount;
        }
        
        if (pointDetectComponent.IsVeryClose())
        {
            Attack(1, pointDetectComponent.GetTarget().position);
        }
    }
    
    public void Release()
    {
        
    }
    
    /// <summary>
    /// 攻击
    /// </summary>
    public void Attack(float time, Vector2 point)
    {
        if (isInAttackCd) return;
        if (IsInAttackInterval) return;
        // 这里需要传入一个子弹的爆炸后的特效,可能是没有的
        heroEntity.GetSpecifyComponent<HeroStateMachineComponent>(ComponentType.StateMachineComponent)
            .TryChangeState(StateType.Attack);
        var bulletEntity = AssetsLoadManager.LoadBullet(heroEntity.GetHeroData());
        bulletEntity.GetComponent<RectTransform>().parent = BattleManager.Instance.GetBulletParent();
        bulletEntity.GetComponent<RectTransform>().position = heroEntity.GetFireLocation().position;
        bulletEntity.AllComponentList.Add(new BulletMoveComponent(bulletEntity.GetComponent<RectTransform>(), 150f,
            point, BulletMoveType.SingleTargetMove));
        bulletEntity.InitBullet(EntityType.EnemyEntity, heroEntity.GetHeroData().atk);
        EntitySystem.Instance.AddEntity(bulletEntity.EntityId, bulletEntity);
        LastAttackTime = Time.time;
        IsInAttackInterval = true;
        ReduceAttackCount();
    }
    
    /// <summary>
    /// 减少攻击次数
    /// </summary>
    private void ReduceAttackCount()
    {
        attackCount -= 1;
        if (attackCount > 0) return;
        isInAttackCd = true;
        attackStartCdTime = Time.time;
        // 发送 攻击CD开始事件  并传入实体ID 以便于知道是谁
        EventMgr.Instance.TriggerEvent(GameEvent.AttackStartCd, heroEntity.EntityId);
    }
}
