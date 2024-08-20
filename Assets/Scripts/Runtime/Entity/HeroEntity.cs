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
    private Transform attackFireLocation;   
    
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
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
            _ => throw new ArgumentOutOfRangeException(nameof(componentType), componentType, null)
        };
    }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 初始化英雄
    /// </summary>
    /// <param name="heroData"></param>
    /// <param name="hero"></param>
    /// <param name="fireLocation"> 开火口 </param>
    public void InitHero(HeroData heroData, GameObject hero, Transform fireLocation)
    {
        data = heroData;
        heroObj = hero;
        attackFireLocation = fireLocation;
    }
    
    /// <summary>
    /// 获取英雄数据
    /// </summary>
    /// <returns></returns>
    public HeroData GetHeroData()
    {
        return data;
    }

    /// <summary>
    /// 设置英雄位置
    /// </summary>
    private void SetHeroLocation(int locationIndex)
    {
        BattleManager.Instance.SetPrefabLocation(heroObj, locationIndex);
    }

    public Transform GetFireLocation()
    {
        return attackFireLocation;
    }
    
    
}
