using System;
using Tao_Framework.Core.Event;
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

    private Vector2 ThisTransform;

    /// <summary>
    /// 子弹初始位置
    /// </summary>
    private Vector2 startLocation;

    /// <summary>
    /// 从多远距离开始计时
    /// </summary>
    private float startTimeDirection;

    /// <summary>
    /// 子弹移动组件
    /// </summary>
    public BulletMoveComponent(RectTransform transform, float moveSpeed, Vector2 moveDirection, BulletMoveType moveType, float direction)
    {
        this.EntityTransform = transform;
        this.MoveSpeed = moveSpeed;
        this.MoveDirection = moveDirection;
        startTimeDirection = direction;
        startLocation = transform.anchoredPosition;
        ContinueMove = true;
        this.moveType = moveType;
        ThisTransform = new Vector2(EntityTransform.position.x, EntityTransform.position.y);
    }
    
    public void Tick(float time)
    {
        Move(time);
        if (IsExceedStartTimeDirection())
        {
            EventMgr.Instance.TriggerEvent(GameEvent.ExceedDeadDirection, EntityTransform.GetComponent<BulletEntity>().EntityId);
        }
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
        Vector3 direction = MoveDirection - ThisTransform;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        EntityTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        EntityTransform.position += EntityTransform.right * MoveSpeed * Time.deltaTime;
        // Vector2 targetDir = MoveDirection - ThisTransform;
        // EntityTransform.up = targetDir.normalized;
        // EntityTransform.Translate(-EntityTransform.right * MoveSpeed * time);
    }
    
    /// <summary>
    /// 是否超出开始倒计时死亡的距离
    /// </summary>
    /// <returns></returns>
    private bool IsExceedStartTimeDirection()
    {
        return Vector2.Distance(startLocation, EntityTransform.anchoredPosition) >= startTimeDirection;
    }
}
