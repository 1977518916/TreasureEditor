using Tao_Framework.Core.Event;
using UnityEngine;

/// <summary>
/// 敌人移动组件
/// </summary>
public class EnemyMoveComponent : MoveComponent
{
    public RectTransform EntityTransform { get; set; }

    public float MoveSpeed { get; set; }

    public Vector2 MoveDirection { get; set; }
    
    public bool ContinueMove { get; set; }

    private EnemyEntity entity;
    
    /// <summary>
    /// 目标
    /// </summary>
    private RectTransform target;
    
    private PointDetectComponent pointDetectComponent;
    
    public EnemyMoveComponent(EnemyEntity entity,RectTransform target, RectTransform entityTransform, float moveSpeed)
    {
        this.entity = entity;
        this.target = target;
        MoveSpeed = moveSpeed;
        EntityTransform = entityTransform;
        ContinueMove = true;
        MoveDirection = (this.target.position - EntityTransform.position).normalized;
        EventMgr.Instance.RegisterEvent<Entity>(GameEvent.ReplaceTarget, ReplaceTarget);
    }
    
    public void Tick(float time)
    {
        pointDetectComponent = entity.GetSpecifyComponent<PointDetectComponent>(ComponentType.DetectComponent);
        ContinueMove = !pointDetectComponent.IsVeryClose();
        Move(time);
    }
    
    public void Move(float time)
    {
        if (!ContinueMove) return;
        EntityTransform.Translate(MoveDirection * MoveSpeed * time);
    }

    private void ReplaceTarget(Entity e)
    {
        target = e.GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent).EntityTransform;
        MoveDirection = (this.target.position - EntityTransform.position).normalized;
    }
}
