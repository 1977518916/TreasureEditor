using System;
using UnityEngine;

public class BulletMoveComponent : MoveComponent
{
    public RectTransform EntityTransform { get; set; }
    
    public float MoveSpeed { get; set; }
    
    public Vector2 MoveDirection { get; set; }
    
    /// <summary>
    /// 移动类型
    /// </summary>
    private readonly BulletMoveType moveType;
    
    public bool ContinueMove { get; set; }

    /// <summary>
    /// 子弹移动组件
    /// </summary>
    public BulletMoveComponent(RectTransform transform, float moveSpeed, Vector2 moveDirection, BulletMoveType moveType)
    {
        this.EntityTransform = transform;
        this.MoveSpeed = moveSpeed;
        this.MoveDirection = moveDirection;
        ContinueMove = true;
        this.moveType = moveType;
    }
    
    public void Tick(float time)
    {
        Move(time);
    }

    public void Release()
    {
        
    }

    /// <summary>
    /// 移动处理
    /// </summary>
    public void Move(float time)
    {
        switch (moveType)
        {
            case BulletMoveType.SingleTargetMove:
                SingleTargetMove(time);
                break;
            case BulletMoveType.ParabolaTargetMove:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    /// <summary>
    /// 单个子弹朝目标放心移动
    /// </summary>
    private void SingleTargetMove(float time)
    {
        Vector2 targetDir = MoveDirection - new Vector2(EntityTransform.position.x, EntityTransform.position.y);
        // float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        // EntityTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        // EntityTransform.Translate(targetDir.normalized * time * MoveSpeed);
        EntityTransform.up = targetDir.normalized;
        EntityTransform.Translate(-EntityTransform.right * MoveSpeed * time);
    }
}
