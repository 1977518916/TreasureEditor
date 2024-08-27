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
    /// 当前计时
    /// </summary>
    private float currentTime;

    /// <summary>
    /// 管理对应需要释放的实体
    /// </summary>
    private readonly Entity deadEntity;

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
    }
    
    public void Tick(float time)
    {
        if (!isStartDeadTime) return;
        currentTime += time;
        if (!(currentTime >= delayedDeadTime)) return;
        deadEntity.Release();
        isStartDeadTime = false;
    }
    
    public void Release()
    {
        
    }
    
    public void Dead()
    {
        
    }
}
