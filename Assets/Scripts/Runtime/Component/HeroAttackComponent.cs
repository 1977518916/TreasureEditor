using Runtime.Manager;
using Tao_Framework.Core.Event;
using UnityEngine;

/// <summary>
/// 英雄攻击组件
/// </summary>
public class HeroAttackComponent : AttackComponent
{

    public float AttackInterval { get; set; }

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
    private readonly HeroEntity heroEntity;

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
    public void Attack(float time, Vector2 point)
    {
        var bulletObj = Object.Instantiate(bullet);
        // 这里需要传入一个子弹的爆炸后的特效,可能是没有的
        //bulletObj.GetComponent<BulletEntity>().BoomObject = AssetsLoadManager.LoadBullet(heroEntity.GetHeroData());
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
