using System;
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

    private long targetEntityId = -1;

    private RectTransform thisRectTransform;

    private RectTransform target;

    private Entity entity;

    private EntityType targetEntityType;
    
    /// <summary>
    /// 检测的距离
    /// </summary>
    private float distance;
    
    public PointDetectComponent(Entity entity, RectTransform transform, EntityType targetEntityType, float distance)
    {
        this.entity = entity;
        this.distance = distance;
        this.targetEntityType = targetEntityType;
        thisRectTransform = transform;
        EventMgr.Instance.RegisterEvent<long>(GameEvent.EntityDead, TargetDead);
    }

    public void Tick(float time)
    {
        if (EntitySystem.Instance.GetTargetTypeSurviveEntity(targetEntityType))
        {
            switch (targetEntityType)
            {
                case EntityType.None:
                    break;
                case EntityType.HeroEntity:
                    targetEntityId = EntitySystem.Instance.GetSurviveHeroID();
                    break;
                case EntityType.EnemyEntity:
                    targetEntityId = EntitySystem.Instance.GetSurviveEnemyID();
                    break;
                case EntityType.BulletEnemy:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GetTargetRectTransform();
        }
    }

    public void Release()
    {
        EventMgr.Instance.RemoveEvent(GameEvent.EntityDead);
    }

    private void TargetDead(long targetId)
    {
        if (EntitySystem.Instance.GetEntityType(targetId) != targetEntityType) return;
        targetEntityId = EntitySystem.Instance.ReplaceTarget(targetEntityType);
        var targetEntity = GetTargetEntity();
        if (targetEntity != null)
        {
            target = targetEntity.GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent).EntityTransform;
        }
    }
    
    private void GetTargetRectTransform()
    {
        if (targetEntityId != -1 && target == null)  
        {
            target = GetTargetEntity().GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent).EntityTransform;
        }
    }
    
    private Entity GetTargetEntity()
    {
        return targetEntityId == -1 ? null : EntitySystem.Instance.GetEntity(targetEntityId);
    }
    
    /// <summary>
    /// 是否离目标非常近  离目标非常近就开始攻击并停止移动  敌人目前距离是120f
    /// </summary>
    /// <returns></returns>
    public bool IsVeryClose()
    {
        if (targetEntityId == -1) return false;
        return Vector2.Distance(target.position, thisRectTransform.position) < distance;
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
