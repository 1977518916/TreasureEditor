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
    private EntityType entityType;

    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
        EntityType = EntityType.BulletEnemy;
        AllComponentList = new List<IComponent>();
    }
    
    public void Release()
    {
        foreach (var iComponent in AllComponentList)
        {
            iComponent.Release();
        }

        Destroy(this.gameObject);
    }
    
    public T GetSpecifyComponent<T>(ComponentType componentType) where T : IComponent
    {
        foreach (var iComponent in AllComponentList)
        {
            if (IsSpecifyComponent(iComponent, componentType))
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
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<Entity>();
        switch (entityType)
        {
            case EntityType.HeroEntity:
                if (IsHeroEntity(entity))
                    HurtEntity(entity);
                break;
            case EntityType.EnemyEntity:
                if (IsEnemyEntity(entity)) 
                    HurtEntity(entity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    /// <summary>
    /// 伤害实体
    /// </summary>
    private void HurtEntity(Entity entity)
    {
        foreach (var component in entity.AllComponentList)
        {
            if (component is StatusComponent)
            {
                // 扣血
            }

            break;
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