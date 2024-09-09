using System;
using DG.Tweening;
using Runtime.Manager;
using Tao_Framework.Core.Event;
using UnityEngine;

/// <summary>
/// 英雄攻击组件
/// </summary>
public class HeroAttackComponent : AttackComponent
{
    public bool IsInAttackInterval { get; set; }

    public float LastAttackTime { get; set; }

    public float AttackInterval { get; set; }
    public void Attack(float time, Vector2 point)
    {
        
    }

    /// <summary>
    /// 攻击最大次数 也就是每次CD结束以后恢复的攻击次数
    /// </summary>
    private int attackMaxCount;

    /// <summary>
    /// 射出的子弹数量
    /// </summary>
    private int bulletAmount;

    /// <summary>
    /// 攻击次数  每次攻击次数归0时进入攻击CD中
    /// </summary>
    private int attackCount;

    /// <summary>
    /// 攻击CD
    /// </summary>
    private float attackCd;

    /// <summary>
    /// 是否处于攻击CD
    /// </summary>
    private bool isInAttackCd;

    /// <summary>
    /// 攻击开始CD的时间
    /// </summary>
    private float attackStartCdTime;

    /// <summary>
    /// 英雄实体
    /// </summary>
    private readonly HeroEntity heroEntity;

    /// <summary>
    /// 点检测组件
    /// </summary>
    private PointDetectComponent pointDetectComponent;

    /// <summary>
    /// 停止攻击
    /// </summary>
    private bool stopAttack = false;
    
    /// <summary>
    /// 英雄攻击组件的构造函数
    /// </summary>
    /// <param name="attackCountValue"> 攻击次数总数 </param>
    /// <param name="bulletAmount"> 每次射出的子弹数量</param>
    /// <param name="attackInterval"> 攻击间隔时间 </param>
    /// <param name="attackCd"> 攻击CD </param>
    /// <param name="entity"> 英雄实体 </param>
    /// <param name="pointDetectComponent"></param>
    public HeroAttackComponent(int attackCountValue, int bulletAmount, float attackInterval, float attackCd, HeroEntity entity, PointDetectComponent pointDetectComponent)
    {
        attackMaxCount = attackCountValue;
        AttackInterval = attackInterval;
        attackCount = attackCountValue;
        heroEntity = entity;
        this.bulletAmount = bulletAmount;
        this.attackCd = attackCd;
        IsInAttackInterval = false;
        isInAttackCd = false;
        this.pointDetectComponent = pointDetectComponent;
    }

    public void Tick(float time)
    {
        if(IsInAttackInterval)
        {
            // 攻击间隔的时间减去当前时间 如果大于攻击间隔时间 则证明攻击间隔时间结束了 那么就需要退出攻击间隔状态  所以 IsInAttackInterval 此时等于 false
            IsInAttackInterval = !(Time.time - LastAttackTime >= AttackInterval);
        }

        if(isInAttackCd)
        {
            // 攻击开始计时CD的时间减去当前时间 如果大于攻击CD 则证明攻击CD结束了 那么就需要推出CD状态  所以 isInAttackCd 此时等于 false
            isInAttackCd = !(Time.time - attackStartCdTime >= attackCd);
            if(!isInAttackCd)
                attackCount = attackMaxCount;
        }

        if (pointDetectComponent.IsVeryClose() && !stopAttack) 
        {
            Attack(1, pointDetectComponent.GetTarget());
        }
    }

    public void Release()
    {

    }
    
    /// <summary>
    /// 改变停止攻击状态
    /// </summary>
    public void ChangeStopAttack()
    {
        stopAttack = !stopAttack;
    }

    /// <summary>
    /// 攻击
    /// </summary>
    private void Attack(float time, RectTransform target)
    {
        if (isInAttackCd) return;
        if (IsInAttackInterval) return;
        var stateMachine =
            heroEntity.GetSpecifyComponent<HeroStateMachineComponent>(ComponentType.StateMachineComponent);
        stateMachine.TryChangeState(StateType.Attack);

        MakeBullet(target.position);

        float deviation = 0.1f;
        for (int i = 1; i < bulletAmount; i++)
        {
            float rate = ((i + 1) / 0b10) * Mathf.Pow(-1, i);
            MakeBullet(GetOtherPoint(deviation * rate, target.position));
        }

        ReduceAttackCount();
    }

    private Vector2 GetOtherPoint(float angle, Vector2 pointB)
    {
        Vector2 pointA = new Vector2(heroEntity.GetFireLocation().x, heroEntity.GetFireLocation().y);
        Vector2 AB = pointB - pointA;
        float lengthAB = AB.magnitude;
        float lengthAC = Mathf.Cos(Mathf.Deg2Rad * angle) * lengthAB;
        Vector2 unitAB = AB / lengthAB;
        float cosAngle = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sinAngle = Mathf.Sin(angle * Mathf.Deg2Rad);
        Matrix4x4 rotationMatrix = new Matrix4x4(
            new Vector4(cosAngle, -sinAngle, 0, 0),
            new Vector4(sinAngle, cosAngle, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );

        Vector2 rotatedVector = rotationMatrix.MultiplyVector(unitAB * lengthAC);
        return pointA + new Vector2(rotatedVector.x, rotatedVector.y);
    }

    private void MakeBullet(Vector2 targetPoint)
    {
        LastAttackTime = Time.time;
        IsInAttackInterval = true;
        var bulletGo = AssetsLoadManager.LoadBullet(heroEntity.GetHeroData().modelType, LayerMask.NameToLayer("BattleUI"));
        var bulletEntity = EntitySystem.Instance.CreateEntity<BulletEntity>(EntityType.BulletEntity, bulletGo);
        var bulletTran = bulletEntity.GetComponent<RectTransform>();
        var bulletHurt = DataManager.GameData.isInvicibleEnemy ? 1 : heroEntity.GetHeroData().atk;
        var fireLocation = heroEntity.GetFireLocation();
        var bulletParent = BattleManager.Instance.GetBulletParent();
        var bulletAttributeType = heroEntity.GetHeroData().bulletAttributeType;
        //--------------------- 初始化 ----------------------------------
        bulletEntity.InitBullet(EntityType.EnemyEntity, bulletHurt, bulletAttributeType, fireLocation, bulletParent);
        bulletEntity.MoveObject = bulletGo.transform.GetChild(0).gameObject;
        //--------------------- 添加组件 ---------------------------------
        var bulletMove = new BulletMoveComponent(bulletTran, targetPoint, 800f, BulletMoveType.RectilinearMotion, 2000f);
        var bulletDead = new DelayedDeadComponent(3f, bulletEntity);
        bulletEntity.AllComponentList.Add(bulletMove);
        bulletEntity.AllComponentList.Add(bulletDead);
        AddBulletAttribute(bulletEntity, heroEntity.GetHeroData().bulletAttributeType);
    }

    /// <summary>
    /// 添加子弹特性
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="attributeType"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void AddBulletAttribute(BulletEntity entity, BulletAttributeType attributeType)
    {
        switch (attributeType)
        {
            case BulletAttributeType.Penetrate:
                entity.AllComponentList.Add(new BulletPenetrateAttribute(2, entity));
                break;
            case BulletAttributeType.Rebound:
                entity.AllComponentList.Add(new BulletReboundAttribute(2, entity));
                break;
            case BulletAttributeType.Refraction:
                entity.AllComponentList.Add(new BulletRefractionAttribute(2, entity));
                break;
            //case BulletAttributeType.Bomb:
                //entity.AllComponentList.Add(new BulletBombAttribute());
                //break;
            case BulletAttributeType.Split:
                entity.AllComponentList.Add(new BulletSplitAttribute(3, entity, heroEntity.GetHeroData(),
                    entity.GetSpecifyComponent<BulletMoveComponent>(ComponentType.MoveComponent).EntityTransform.anchoredPosition,
                    entity.GetSpecifyComponent<BulletMoveComponent>(ComponentType.MoveComponent).MoveDirection));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(attributeType), attributeType, null);
        }
    }

    /// <summary>
    /// 减少攻击次数
    /// </summary>
    private void ReduceAttackCount()
    {
        attackCount -= 1;
        if(attackCount > 0) return;
        isInAttackCd = true;
        attackStartCdTime = Time.time;
        // 发送 攻击CD开始事件  并传入实体ID 以便于知道是谁
        EventMgr.Instance.TriggerEvent(GameEvent.AttackStartCd, heroEntity.EntityId);
    }
}