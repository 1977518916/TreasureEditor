using System.Collections;
using System.Collections.Generic;
using Runtime.Data;
using Runtime.Manager;
using UnityEngine;

public partial class EntitySystem
{
    /// <summary>
    /// 当前所有英雄数据
    /// </summary>
    private readonly List<HeroData> currentAllHeroData = new List<HeroData>();
    
    /// <summary>
    /// 关卡数据
    /// </summary>
    private LevelData levelData;
    
    /// <summary>
    /// Boss数据
    /// </summary>
    private BossData bossData;

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitData()
    {
        currentAllHeroData.AddRange(DataManager.GetHeroDataList());
        levelData = DataManager.GetLevelData();
        bossData = DataManager.GetLevelData().BossData;
    }
    
    
}
