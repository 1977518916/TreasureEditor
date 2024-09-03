using System;
using System.Collections.Generic;
using Runtime.Component.Skill;
using Runtime.Data;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

/// <summary>
/// 英雄实体
/// </summary>
public class HeroEntity : MonoBehaviour, Entity
{
    [ShowInInspector]
    [ReadOnly]
    public long EntityId { get; set; }

    public EntityType EntityType { get; set; }

    public List<IComponent> AllComponentList { get; set; }
    public bool ReadyRelease { get; set; }

    /// <summary>
    /// 英雄数据
    /// </summary>
    private HeroData data;

    /// <summary>
    /// 攻击位置
    /// </summary>
    private Vector2 fireLocation;
    
    /// <summary>
    /// 是否存活
    /// </summary>
    private bool isSurvive;
    
    /// <summary>
    /// 位置索引
    /// </summary>
    private int locationIndex;
    
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
        EntityType = EntityType.HeroEntity;
        AllComponentList = new List<IComponent>();
        ReadyRelease = false;
    }
    
    public void Release()
    {
        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }
        
        AllComponentList.Clear();
    }

    /// <summary>
    /// 获取指定组件
    /// </summary>
    /// <param name="componentType"></param>
    /// <returns></returns>
    public T GetSpecifyComponent<T>(ComponentType componentType) where T: IComponent
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
            ComponentType.AnimationComponent => component is AnimationComponent,
            ComponentType.StateMachineComponent => component is StateMachineComponent,
            ComponentType.DeadComponent => component is DeadComponent,
            ComponentType.SkillComponent => component is SkillComponent,
            ComponentType.Attribute => component is AttributeComponent,
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }

    /// <summary>
    /// 初始化英雄
    /// </summary>
    /// <param name="heroData"></param>
    /// <param name="fire"></param>
    /// <param name="index"></param>
    public void InitHero(HeroData heroData, Vector2 fire, int index)
    {
        locationIndex = index;
        data = heroData;
        isSurvive = true;
        this.fireLocation = fire;
    }

    /// <summary>
    /// 获取位置索引
    /// </summary>
    /// <returns></returns>
    public int GetLocationIndex()
    {
        return locationIndex;
    }

    /// <summary>
    /// 获取英雄数据
    /// </summary>
    /// <returns></returns>
    public HeroData GetHeroData()
    {
        return data;
    }

    public bool GetIsSurvive()
    {
        return isSurvive;
    }

    public void UpdateSurvive(bool survive)
    {
        isSurvive = survive;
    }
    
    public Vector2 GetFireLocation()
    {
        return fireLocation;
    }
}
