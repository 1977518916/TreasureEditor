using DG.Tweening;
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
    
    /// <summary>
    /// 目标
    /// </summary>
    private readonly RectTransform target;

    private PointDetectComponent pointDetectComponent;
    
    public EnemyMoveComponent(RectTransform target, RectTransform entityTransform, float moveSpeed)
    {
        this.target = target;
        MoveSpeed = moveSpeed;
        EntityTransform = entityTransform;
        MoveDirection = (this.target.position - EntityTransform.position).normalized;
    }
    
    public void Tick(float time)
    {
        //pointDetectComponent = EntitySystem.Instance.GetEntity()
        Move(time);
    }
    
    public void Move(float time)
    {
        if (ContinueMove) return;
        EntityTransform.Translate(MoveDirection * MoveSpeed * time);
    }
}
