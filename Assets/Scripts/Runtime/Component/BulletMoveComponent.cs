using System;
using UnityEngine;

public class BulletMoveComponent : MoveComponent
{
    public Transform EntityTransform { get; set; }

    public float MoveSpeed { get; set; }

    public Vector2 MoveDirection { get; set; }

    /// <summary>
    /// 移动类型
    /// </summary>
    private readonly BulletMoveType moveType;

    /// <summary>
    /// 子弹移动组件
    /// </summary>
    public BulletMoveComponent(Transform transform, float moveSpeed, Vector2 moveDirection, BulletMoveType moveType)
    {
        this.EntityTransform = transform;
        this.MoveSpeed = moveSpeed;
        this.MoveDirection = moveDirection;
        this.moveType = moveType;
    }

    /// <summary>
    /// 移动处理w
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
        EntityTransform.up = MoveDirection;
        EntityTransform.Translate(EntityTransform.up * time * MoveSpeed);
    }
}
