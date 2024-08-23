using System;
using System.Collections.Generic;
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
    
    /// <summary>
    /// 英雄数据
    /// </summary>
    private HeroData data;
    
    /// <summary>
    /// 英雄对象
    /// </summary>
    private GameObject heroObj;

    /// <summary>
    /// 攻击位置
    /// </summary>
    private RectTransform attackFireLocation;
    
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
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }

    /// <summary>
    /// 初始化英雄
    /// </summary>
    /// <param name="heroData"></param>
    /// <param name="hero"></param>
    /// <param name="fireLocation"> 开火口 </param>
    /// <param name="index"></param>
    public void InitHero(HeroData heroData, GameObject hero, RectTransform fireLocation, int index)
    {
        this.locationIndex = index;
        data = heroData;
        heroObj = hero;
        isSurvive = true;
        attackFireLocation = fireLocation;
        AllComponentList = new List<IComponent>();
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

    public RectTransform GetFireLocation()
    {
        return attackFireLocation;
    }
}
