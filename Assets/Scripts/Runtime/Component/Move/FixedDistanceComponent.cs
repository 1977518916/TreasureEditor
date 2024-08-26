using UnityEngine;

public class FixedDistanceComponent : MoveComponent
{
    public void Tick(float time)
    {
        Move(time);
    }

    public void Release()
    {
        
    }

    public RectTransform EntityTransform { get; set; }
    public float MoveSpeed { get; set; }
    public bool ContinueMove { get; set; }
    public Vector2 MoveDirection { get; set; }
    private readonly Vector2 targetLocation;
    /// <summary>
    /// 固定距离移动  用于只会移动一段固定距离的逻辑
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="moveSpeed"></param>
    /// <param name="moveDirection"></param>
    /// <param name="targetLocation"></param>
    public FixedDistanceComponent(RectTransform transform, float moveSpeed, Vector2 moveDirection, Vector2 targetLocation)
    {
        EntityTransform = transform;
        MoveSpeed = moveSpeed;
        MoveDirection = moveDirection;
        this.targetLocation = targetLocation;
        ContinueMove = true;
    }

    public void Move(float time)
    {
        if (!ContinueMove) return;
        EntityTransform.Translate(MoveDirection * MoveSpeed * time);
        if (Vector2.Distance(EntityTransform.position, targetLocation) <= 1f)
        {
            ContinueMove = false;
        }
    }

    /// <summary>
    /// 是否还在继续移动
    /// </summary>
    /// <returns></returns>
    public bool IsContinueMove()
    {
        return ContinueMove;
    }
}
