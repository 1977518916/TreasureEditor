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
    /// 目标位置
    /// </summary>
    private RectTransform targetLocation;

    private Vector2 targetScreenLocation;

    private Vector2 sourceScreenLocation;
    
    /// <summary>
    /// 子弹移动组件
    /// </summary>
    public BulletMoveComponent(RectTransform transform, RectTransform target, float moveSpeed, BulletMoveType moveType,
        float direction)
    {
        EntityTransform = transform;
        MoveSpeed = moveSpeed;
        startTimeDirection = direction;
        targetLocation = target;
        startLocation = transform.anchoredPosition;
        ContinueMove = true;
        this.moveType = moveType;
        ThisTransform = new Vector2(EntityTransform.anchoredPosition.x, EntityTransform.anchoredPosition.y);
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(EntityTransform,
            RectTransformUtility.WorldToScreenPoint(GameManager.Instance.BattleCanvas.worldCamera,
                targetLocation.position), GameManager.Instance.BattleCanvas.worldCamera, out targetScreenLocation);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(EntityTransform,
            RectTransformUtility.WorldToScreenPoint(GameManager.Instance.BattleCanvas.worldCamera,
                EntityTransform.position), GameManager.Instance.BattleCanvas.worldCamera, out sourceScreenLocation);
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