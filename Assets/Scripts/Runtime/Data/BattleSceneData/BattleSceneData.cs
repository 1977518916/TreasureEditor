using System.Collections.Generic;
using Runtime.Data;
using UnityEngine;

/// <summary>
/// 战斗场景数据结构
/// </summary>
public struct BattleSceneData
{
    /// <summary>
    /// 本次使用的英雄列表   只会用前五个数据因为英雄数据最大为5     如果前五个数据内有null值   那么就代表那个位置没有英雄
    /// </summary>
    public List<GameObject> HeroList;
    
    /// <summary>
    /// 关卡数据
    /// </summary>
    public LevelData LevelData;
    
    /// <summary>
    /// 英雄数据  规则同英雄列表数据相同
    /// </summary>
    public List<HeroData> HeroDataList;
}