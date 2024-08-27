using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Component.Position;
using Tools;
using UnityEngine;

public class EnemyEntity : MonoBehaviour, Entity
{
    public long EntityId { get; set; }
    public EntityType EntityType { get; set; }
    public List<IComponent> AllComponentList { get; set; }
    public bool ReadyRelease { get; set; }

    public bool IsSurvive { get; private set; }

    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
        EntityType = EntityType.EnemyEntity;
        AllComponentList = new List<IComponent>();
        IsSurvive = true;
        ReadyRelease = false;
    }

    public void Release()
    {
        GetComponent<Collider2D>().enabled = false;
        foreach (var iComponent in AllComponentList)
        {
            iComponent?.Release();
        }
        Destroy(gameObject);
    }
    
    public void SetColliderEnabled(bool isEnabled)
    {
        GetComponent<Collider2D>().enabled = isEnabled;
    }

    public void SetSurvive(bool survive)
    {
        IsSurvive = survive;
    }

    /// <summary>
    /// 获取指定组件
    /// </summary>
    /// <param name="componentType"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 检测是否是指定组件
    /// </summary>
    /// <param name="component"></param>
    /// <param name="componentType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public bool IsSpecifyComponent(IComponent component, ComponentType componentType)
    {
        return componentType switch
        {
            ComponentType.AttackComponent => component is AttackComponent,
            ComponentType.MoveComponent => component is MoveComponent,
            ComponentType.RayComponent => false,
            ComponentType.StatusComponent => component is StatusComponent,
            ComponentType.DetectComponent => component is DetectComponent,
            ComponentType.RandomPositionComponent => component is RandomPositionComponent,
            ComponentType.AnimationComponent => component is AnimationComponent,
            ComponentType.StateMachineComponent => component is StateMachineComponent,
            ComponentType.DeadComponent => component is DeadComponent,
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }
}