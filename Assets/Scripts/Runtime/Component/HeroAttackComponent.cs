using Tao_Framework.Core.Event;
using UnityEngine;

/// <summary>
/// 英雄攻击组件
/// </summary>
public class HeroAttackComponent : AttackComponent
{
    /// <summary>
    /// 子弹
    /// </summary>
    private readonly GameObject bullet;

    /// <summary>
    /// 攻击次数
    /// </summary>
    private int attackCount;
    
    /// <summary>
    /// 英雄实体
    /// </summary>
    private HeroEntity heroEntity;

    /// <summary>
    /// 英雄攻击组件的构造函数
    /// </summary>
    /// <param name="bulletValue"></param>
    /// <param name="attackCountValue"></param>
    /// <param name="entity"></param>
    private HeroAttackComponent(GameObject bulletValue, int attackCountValue, HeroEntity entity)
    {
        bullet = bulletValue;
        attackCount = attackCountValue;
        heroEntity = entity;
    }

    /// <summary>
    /// 攻击
    /// </summary>
    /// <param name="point"> 目标点 </param>
    public void Attack(Vector2 point)
    {
        var bulletObj = Object.Instantiate(bullet);
        
    }

    /// <summary>
    /// 减少攻击次数
    /// </summary>
    private void ReduceAttackCount()
    {
        attackCount -= 1;
        if (attackCount <= 0)
        {
            // 发送 攻击CD开始事件  并传入实体ID 以便于知道是谁
            EventMgr.Instance.TriggerEvent(GameEvent.AttackStartCd, heroEntity.EntityId);
        }
    }
}
