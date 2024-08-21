using System;
using System.Collections.Generic;
using QFSW.QC;
using Runtime.Manager;
using Tao_Framework.Core.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoSingleton<BattleManager>
{
    /// <summary>
    /// 战斗背景
    /// </summary>
    public Image battleBG;
    
    /// <summary>
    /// 战斗底座父对象
    /// </summary>
    public GameObject BattleCoordinates;
    
    /// <summary>
    /// 所有战斗底座
    /// </summary>
    public List<RectTransform> BattleBaseList = new List<RectTransform>();
    
    /// <summary>
    /// 所有英雄血条和CD条
    /// </summary>
    public List<RectTransform> HeroStatusList = new List<RectTransform>();
    
    /// <summary>
    /// 英雄根节点预制体
    /// </summary>
    public GameObject HeroRootPrefab;
    
    /// <summary>
    /// 英雄父对象
    /// </summary>
    public RectTransform HeroParent;

    /// <summary>
    /// 敌人的父对象
    /// </summary>
    public RectTransform EnemyParent;
    
    #region Command

    /// <summary>
    /// 生成一个场景中的预制体对象并放置在特定底座位置
    /// 生成的预制体 最好是 英雄对象
    /// </summary>
    /// <param name="root"></param>
    /// <param name="value"></param>
    [Command]
    public void SetPrefabLocation(GameObject root, int value)
    {
        root.GetComponent<RectTransform>().position = new Vector3(BattleBaseList[value].position.x, BattleBaseList[value].position.y + 10f);
    }
    
    /// <summary>
    /// 展示战斗底座
    /// </summary>
    /// <param name="isShow"></param>
    [Command]
    public void ShowBattleBase(bool isShow)
    {
        BattleCoordinates.SetActive(isShow);
    }

    /// <summary>
    /// 设置战斗背景
    /// </summary>
    [Command]
    public void SetBattleBG(int value)
    {
        if (value > 6) return;
        battleBG.sprite = AssetsLoadManager.Load<Sprite>(DataManager.MapTexturePath + value);
    }

    /// <summary>
    /// 获取英雄状态UI对象
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public (GameObject HpBg, GameObject CdBg, Image Hp, Image Cd) GetHeroStatus(int value)
    {
        if (value > 4) return default;
        var heroStatus = HeroStatusList[value];
        var hpBg = heroStatus.Find("HP_BG").gameObject;
        var cdBg = heroStatus.Find("AttackCD_BG").gameObject;
        var hp = heroStatus.Find("HP_BG/HP").GetComponent<Image>();
        var cd = heroStatus.Find("AttackCD_BG/AttackCD").GetComponent<Image>();
        return (hpBg, cdBg, hp, cd);
    }
    
    /// <summary>
    /// 获取开火点
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public RectTransform GetFirePoint(int index)
    {
        return BattleBaseList[index].Find("FirePoint").GetComponent<RectTransform>();
    }

    #endregion

    private void OnDestroy()
    {
        //DestroyInstance();
    }
}
