using Tao_Framework.Core.Event;
using UnityEngine;

/// <summary>
/// 点检测
/// </summary>
public class PointDetectComponent : DetectComponent
{
    public string TargetTag { get; set; }
    public LayerMask LayerMask { get; set; }
    public DetectRangeType DetectRangeType { get; set; }
    public IRayComponent IRayComponent { get; set; }

    private long targetEntityId;

    private RectTransform thisRectTransform;

    private RectTransform target;

    private Entity entity;

    private EntityType targetEntityType;

    public PointDetectComponent(long entityId, Entity entity, RectTransform transform, EntityType targetEntityType)
    {
        targetEntityId = entityId;
        this.entity = entity;
        this.targetEntityType = targetEntityType;
        thisRectTransform = transform;
        target = EntitySystem.Instance.GetEntity(targetEntityId)
            .GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent).EntityTransform;
        EventMgr.Instance.RegisterEvent<long>(GameEvent.EntityDead, TargetDead);
    }
    
    public void Tick(float time)
    {
        
    }
    
    private void TargetDead(long targetId)
    {
        targetEntityId = EntitySystem.Instance.ReplaceTarget(targetEntityType);
    }
    
    public Entity GetTargetEntity()
    {
        return targetEntityId == -1 ? null : EntitySystem.Instance.GetEntity(targetEntityId);
    }

    /// <summary>
    /// 是否离目标非常近  离目标非常近就开始攻击并停止移动
    /// </summary>
    /// <returns></returns>
    public bool IsVeryClose()
    {
        return Vector2.Distance(target.position, thisRectTransform.position) < 120f;
    }
    
    /// <summary>
    /// 获取目标
    /// </summary>
    /// <returns></returns>
    public RectTransform GetTarget()
    {
        return target;
    }
}
