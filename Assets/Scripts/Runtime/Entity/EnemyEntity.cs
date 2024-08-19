using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    public long EntityId { get; set; }
    public List<IComponent> AllComponentList { get; set; }

    public void Init()
    {
        
    }
    
    /// <summary>
    /// 获取指定组件
    /// </summary>
    /// <param name="componentType"></param>
    /// <returns></returns>
    public IComponent GetSpecifyComponent(ComponentType componentType)
    {
        foreach (var iComponent in AllComponentList)
        {
            if (IsSpecifyComponent(iComponent, componentType))
            {
                return iComponent;
            }
        }

        return null;
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
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }
}
