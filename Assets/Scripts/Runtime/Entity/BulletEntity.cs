using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class BulletEntity : MonoBehaviour, Entity
{
    public long EntityId { get; set; }
    public EntityType EntityType { get; set; }

    public List<IComponent> AllComponentList { get; set; }

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
    /// 多少次触发检测会销毁子弹
    /// </summary>
    private int triggerDeadCount;

    /// <summary>
    /// 当前触发次数
    /// </summary>
    private int currentTriggerCount;

    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
        EntityType = EntityType.BulletEntity;
        AllComponentList = new List<IComponent>();
    }

    /// <summary>
    /// 初始化子弹
    /// </summary>
    public void InitBullet(EntityType targetType, int hurt, int triggerCount, RectTransform bulletPos, RectTransform parent)
    {
        bulletHurt = hurt;
        targetEntityType = targetType;
        triggerDeadCount = triggerCount;
        GetComponent<RectTransform>().SetParent(parent);
        GetComponent<RectTransform>().position = bulletPos.position;
        GenerateAttackBox();
    }

    public void Release()
    {
        AllComponentList.Clear();
        Destroy(this.gameObject);
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
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }

    /// <summary>
    /// 生成攻击检测包围盒
    /// </summary>
    private void GenerateAttackBox()
    {
        if(targetEntityType == EntityType.HeroEntity) return;
        var box = gameObject.AddComponent<BoxCollider2D>();
        box.isTrigger = true;
        box.size = new Vector2(50f, 50f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var entity = other.GetComponent<Entity>();
        if(entity == null) return;
        if(entity.EntityType != targetEntityType) return;
        switch(targetEntityType)
        {
            case EntityType.HeroEntity:
                if(IsHeroEntity(entity))
                    HurtEntity(entity);
                break;
            case EntityType.EnemyEntity:
                if(IsEnemyEntity(entity))
                    HurtEntity(entity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        currentTriggerCount++;
        if(currentTriggerCount == triggerDeadCount)
        {
            EntitySystem.Instance.ReleaseEntity(EntityId);
        }
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
}