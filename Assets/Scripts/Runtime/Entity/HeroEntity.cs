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
    
    /// <summary>
    /// 英雄数据
    /// </summary>
    private HeroData data;
    
    /// <summary>
    /// 英雄对象
    /// </summary>
    private GameObject heroObj;
    
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
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
    public void InitHero(HeroData heroData, GameObject hero)
    {
        data = heroData;
        heroObj = hero;
    }

    /// <summary>
    /// 设置英雄位置
    /// </summary>
    private void SetHeroLocation(int locationIndex)
    {
        BattleManager.Instance.SetPrefabLocation(heroObj, locationIndex);
    }
}
