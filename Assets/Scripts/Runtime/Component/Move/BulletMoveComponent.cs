using System;
using Tao_Framework.Core.Event;
using UnityEngine;

public class BulletMoveComponent : MoveComponent
{
    /// <summary>
    /// 自身坐标组件
    /// </summary>
    public RectTransform EntityTransform { get; set; }
    
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed { get; set; }
    
    /// <summary>
    /// 移动方向
    /// </summary>
    public Vector2 MoveDirection { get; set; }

    /// <summary>
    /// 移动类型
    /// </summary>
    private readonly BulletMoveType moveType;
    
    /// <summary>
    /// 是否继续移动
    /// </summary>
    public bool ContinueMove { get; set; }
    
    /// <summary>
    /// 子弹初始位置
    /// </summary>
    private Vector2 startLocation;

    /// <summary>
    /// 从多远距离开始计时
    /// </summary>
    private float startTimeDirection;
    
    /// <summary>
    /// 目标屏幕位置  最终其实转换为了本地位置在使用
    /// </summary>
    private Vector2 targetScreenLocation;

    /// <summary>
    /// 自身屏幕位置  最终其实转换为了本地位置在使用
    /// </summary>
    private Vector2 sourceScreenLocation;

    /// <summary>
    /// 子弹移动组件
    /// </summary>
    public BulletMoveComponent(RectTransform transform, Vector2 target, float moveSpeed, BulletMoveType moveType, float direction)
    {
        EntityTransform = transform;
        MoveSpeed = moveSpeed;
        startTimeDirection = direction;
        startLocation = transform.anchoredPosition;
        ContinueMove = true;
        this.moveType = moveType;
        var worldCamera = GameManager.Instance.BattleCanvas.worldCamera;
        sourceScreenLocation = EntityTransform.ScreenToLocalPoint(worldCamera);
        targetScreenLocation = EntityTransform.ScreenToLocalPoint(worldCamera, target);
        MoveDirection = targetScreenLocation - sourceScreenLocation;
    }

    public void Tick(float time)
    {
        Move(time);
        if (IsExceedStartTimeDirection())
        {
            EventMgr.Instance.TriggerEvent(GameEvent.ExceedDeadDirection,
                EntityTransform.GetComponent<BulletEntity>().EntityId);
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
            case BulletMoveType.RectilinearMotion:
                RectilinearMotion();
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
    private void RectilinearMotion()
    {
        Vector2 direction = targetScreenLocation - sourceScreenLocation;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        EntityTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        EntityTransform.anchoredPosition3D += new Vector3((EntityTransform.right * MoveSpeed * Time.deltaTime).x,
            (EntityTransform.right * MoveSpeed * Time.deltaTime).y, 0f);
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