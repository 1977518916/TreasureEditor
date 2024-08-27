using Tao_Framework.Core.Event;
using UnityEngine;

/// <summary>
/// 延迟死亡组件
/// </summary>
public class DelayedDeadComponent : DeadComponent
{
    public long EntityID { get; set; }

    /// <summary>
    /// 是否开始死亡计时
    /// </summary>
    private bool isStartDeadTime;

    /// <summary>
    /// 延迟死亡时间
    /// </summary>
    private float delayedDeadTime;
    
    /// <summary>
    /// 当前死亡计时
    /// </summary>
    private float currentDeadTime;

    /// <summary>
    /// 管理对应需要释放的实体
    /// </summary>
    private readonly Entity deadEntity;
    
    /// <summary>
    /// 最大延迟死亡时间 并非指的是延迟死亡时间的最大限制  而是,如果开始倒计时死亡的条件一直未满足,则这个时间到达满足时,开始计时死亡
    /// </summary>
    private const float MaxDelayedDeadTime = 8f;
    
    /// <summary>
    /// 当前最大延迟死亡时间计时
    /// </summary>
    private float currentMaxDelayedDeadTime;
    
    /// <summary>
    /// 延迟死亡时间
    /// </summary>
    /// <param name="deadTime"></param>
    /// <param name="entity"></param>
    public DelayedDeadComponent(float deadTime, Entity entity)
    {
        delayedDeadTime = deadTime;
        isStartDeadTime = false;
        deadEntity = entity;
        EventMgr.Instance.RegisterEvent<long>(deadEntity.EntityId, GameEvent.ExceedDeadDirection, StartDeadTime);
    }
    
    public void Tick(float time)
    {
        IsExceedMaxDeadTime(time);
        if (!isStartDeadTime) return;
        currentDeadTime += time;
        if (!(currentDeadTime >= delayedDeadTime)) return;
        EntitySystem.Instance.ReleaseEntity(deadEntity.EntityId);
    }
    
    public void Release()
    {
        EventMgr.Instance.RemoveEvent(deadEntity.EntityId, GameEvent.ExceedDeadDirection);
    }
    
    public void Dead()
    {
        
    }

    /// <summary>
    /// 是否超出最大死亡时间
    /// </summary>
    private void IsExceedMaxDeadTime(float time)
    {
        currentMaxDelayedDeadTime += time;
        if (currentMaxDelayedDeadTime >= MaxDelayedDeadTime)
        {
            isStartDeadTime = true;
        }
    }

    /// <summary>
    /// 超出死亡距离 开始死亡倒计时
    /// </summary>
    private void StartDeadTime(long entityId)
    {
        if (deadEntity.EntityId != entityId) return;
        isStartDeadTime = true;
    }
}
