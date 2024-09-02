using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class BulletEntity : MonoBehaviour, Entity
{
    public long EntityId { get; set; }
    public EntityType EntityType => EntityType.BulletEntity;

    public List<IComponent> AllComponentList { get; set; }
    public bool ReadyRelease { get; set; }

    /// <summary>
    /// 移动时播放的动画物体
    /// </summary>
    public GameObject MoveObject { get; set; }

    /// <summary>
    /// 爆破时的动画物体(可能为空)
    /// </summary>
    public GameObject BoomObject { get; set; }

    /// <summary>
    /// 目标实体类型  对哪种目标造成伤害
    /// </summary>
    public EntityType targetEntityType;

    /// <summary>
    /// 子弹伤害
    /// </summary>
    private int bulletHurt;
    
    /// <summary>
    /// 子弹特性
    /// </summary>
    private BulletAttributeType attributeType;
    
    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
        AllComponentList = new List<IComponent>();
        ReadyRelease = false;
    }

    /// <summary>
    /// 初始化子弹
    /// </summary>
    public void InitBullet(EntityType targetType, int hurt, BulletAttributeType bulletAttributeType, Vector2 bulletLocation, RectTransform parent)
    {
        bulletHurt = hurt;
        targetEntityType = targetType;
        attributeType = bulletAttributeType;
        GetComponent<RectTransform>().SetParent(parent);
        GetComponent<RectTransform>().position = bulletLocation;
        GenerateAttackBox();
    }

    public void Release()
    {
        foreach (var iComponent in AllComponentList)
        {
            iComponent.Release();
        }
        AllComponentList.Clear();
        Destroy(gameObject);
    }
    
    public T GetSpecifyComponent<T>(ComponentType componentType) where T : IComponent
    {
        foreach (var iComponent in AllComponentList)
        {
            if(IsSpecifyComponent(iComponent, componentType))
            {
                return (T)iComponent;
            }
        }

        return default;
    }
    
    public bool IsSpecifyComponent(IComponent component, ComponentType componentType)
    {
        return componentType switch
        {
            ComponentType.AttackComponent => component is AttackComponent,
            ComponentType.MoveComponent => component is MoveComponent,
            ComponentType.RayComponent => false,
            ComponentType.StatusComponent => component is StatusComponent,
            ComponentType.DetectComponent => component is DetectComponent,
            ComponentType.AnimationComponent => component is AnimationComponent,
            ComponentType.StateMachineComponent => component is StateMachineComponent,
            ComponentType.DeadComponent => component is DeadComponent,
            ComponentType.Attribute => component is AttributeComponent,
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }
    
    /// <summary>
    /// 生成攻击检测包围盒
    /// </summary>
    private void GenerateAttackBox()
    {
        var box = gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
        box.size = new Vector2(50f, 50f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (attributeType)
        {
            case BulletAttributeType.Penetrate:
                Penetrate(other);
                break;
            case BulletAttributeType.Rebound:
                Rebound(other);
                break;
            case BulletAttributeType.Refraction:
                Refraction(other);
                break;
            // case BulletAttributeType.Bomb:
            //     break;
            case BulletAttributeType.Split:
                Split(other);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// 是否伤害实体
    /// </summary>
    private bool IsHurtEntity(Collider2D other)
    {
        var entity = other.GetComponent<Entity>();
        if (entity == null) return false;
        if (entity.EntityType != targetEntityType) return false;
        switch(targetEntityType)
        {
            case EntityType.HeroEntity:
                if(IsHeroEntity(entity))
                    HurtEntity(entity);
                return true;
            case EntityType.EnemyEntity:
                if (IsEnemyEntity(entity) || IsBoss(entity)) 
                    HurtEntity(entity);
                return true;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Penetrate(Collider2D other)
    {
        if (IsHurtEntity(other))
            GetSpecifyComponent<BulletPenetrateAttribute>(ComponentType.Attribute)?.Execute();
    }

    private void Rebound(Collider2D other)
    {
        IsHurtEntity(other);
        if (other.CompareTag("Boundary")) 
        {
            GetSpecifyComponent<BulletReboundAttribute>(ComponentType.Attribute)?.Execute();
        }
    }

    private void Refraction(Collider2D other)
    {
        if (IsHurtEntity(other))
            GetSpecifyComponent<BulletRefractionAttribute>(ComponentType.Attribute)?.Execute();
    }

    private void Split(Collider2D other)
    {
        if (IsHurtEntity(other))
            GetSpecifyComponent<BulletSplitAttribute>(ComponentType.Attribute)?.Execute();
    }

    /// <summary>
    /// 伤害实体
    /// </summary>
    private void HurtEntity(Entity entity)
    {
        switch(targetEntityType)
        {
            case EntityType.None:
                break;
            case EntityType.HeroEntity:
                entity.GetSpecifyComponent<HeroStatusComponent>(ComponentType.StatusComponent).Hit(bulletHurt);
                break;
            case EntityType.EnemyEntity:
                entity.GetSpecifyComponent<EnemyStatusComponent>(ComponentType.StatusComponent).Hit(bulletHurt);
                break;
            case EntityType.BulletEntity:
                entity.GetSpecifyComponent<EnemyStatusComponent>(ComponentType.StatusComponent).Hit(bulletHurt);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// 是否是英雄实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private bool IsHeroEntity(Entity entity)
    {
        return entity is HeroEntity;
    }

    /// <summary>
    /// 是否是敌人实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private bool IsEnemyEntity(Entity entity)
    {
        return entity is EnemyEntity;
    }   
    
    /// <summary>
    /// 是否是Boss
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private bool IsBoss(Entity entity)
    {
        return entity as BossEntity;
    }
}