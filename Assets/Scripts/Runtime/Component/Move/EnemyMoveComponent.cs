using Tao_Framework.Core.Event;
using UnityEngine;
using UnityTimer;

/// <summary>
/// 敌人移动组件
/// </summary>
public class EnemyMoveComponent : MoveComponent
{
    public RectTransform EntityTransform { get; set; }

    public float MoveSpeed { get; set; }

    public Vector2 MoveDirection { get; set; }
    
    public bool ContinueMove { get; set; }

    /// <summary>
    /// 是否停止移动
    /// </summary>
    private bool isStopMove;
    
    private EnemyEntity entity;
    
    /// <summary>
    /// 目标
    /// </summary>
    private RectTransform target;
    
    /// <summary>
    /// 点检测组件
    /// </summary>
    private PointDetectComponent pointDetectComponent;
    
    /// <summary>
    /// 定时器
    /// </summary>
    private Timer timer;
    
    public EnemyMoveComponent(EnemyEntity entity,RectTransform target, RectTransform entityTransform, float moveSpeed)
    {
        this.entity = entity;
        this.target = target;
        MoveSpeed = moveSpeed;
        EntityTransform = entityTransform;
        ContinueMove = true;
        if (target != null) 
        {
            MoveDirection = (this.target.position - EntityTransform.position).normalized;
        }

        EventMgr.Instance.RegisterEvent<Entity>(this.entity.EntityId, GameEvent.ReplaceTarget, ReplaceTarget);
    }
    
    public void Tick(float time)
    {
        pointDetectComponent ??= entity.GetSpecifyComponent<PointDetectComponent>(ComponentType.DetectComponent);
        ContinueMove = !pointDetectComponent.IsVeryClose();
        Move(time);
    }
    
    public void Release()
    {
        EventMgr.Instance.RemoveEvent(entity.EntityId, GameEvent.ReplaceTarget);
        timer?.Cancel();
        timer = null;
    }
    
    public void Move(float time)
    {
        if (!ContinueMove) return;
        if (isStopMove) return;
        EntityTransform.Translate(MoveDirection * MoveSpeed * time);
    }
    
    private void ReplaceTarget(Entity e)
    {
        if (e == null) return;
        target = e.GetSpecifyComponent<MoveComponent>(ComponentType.MoveComponent).EntityTransform;
        MoveDirection = (this.target.position - EntityTransform.position).normalized;
    }

    /// <summary>
    /// 停止移动
    /// </summary>
    /// <param name="time"></param>
    public void StopMove(float time)
    {
        isStopMove = true;
        timer?.Cancel();
        timer = Timer.Register(time, () => isStopMove = false);
    }
}
