using System;
using System.Collections.Generic;
using Runtime.Component.Position;
using Runtime.Data;
using Tools;
using UnityEngine;

public class BossEntity : MonoBehaviour, Entity
{
    public long EntityId { get; set; }
    public EntityType EntityType { get; private set; }
    public List<IComponent> AllComponentList { get; set; }
    public bool ReadyRelease { get; set; }

    /// <summary>
    /// 是否存活
    /// </summary>
    public bool IsSurvive { get; private set; }

    /// <summary>
    /// Boss数据
    /// </summary>
    private BossData data;
    
    /// <summary>
    /// 实体位置
    /// </summary>
    private RectTransform rectTransform;
    
    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
        EntityType = EntityType.EnemyEntity;
        AllComponentList = new List<IComponent>();
        IsSurvive = true;
        ReadyRelease = false;
    }
    
    public void InitBoss(BossData bossData)
    {
        rectTransform = GetComponent<RectTransform>();
        this.data = bossData;
    }
    
    public void Release()
    {
        foreach (var iComponent in AllComponentList)
        {
            iComponent?.Release();
        }
        GetComponent<Collider2D>().enabled = false;
        AllComponentList.Clear();
    }

    public void SetSurvive(bool survive)
    {
        IsSurvive = survive;
    }

    public void SetColliderEnabled(bool isEnabled)
    {
        GetComponent<Collider2D>().enabled = isEnabled;
    }

    public RectTransform GetEntityTransform()
    {
        return rectTransform;
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
            ComponentType.DeadComponent => component is DeadComponent,
            ComponentType.RandomPositionComponent => component is RandomPositionComponent,
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }
}