using System.Collections.Generic;
using QFSW.QC;
using Runtime.Manager;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
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
    /// 生成一个场景中的预制体对象并放置在特定底座位置
    /// 生成的预制体 最好是 英雄对象
    /// </summary>
    /// <param name="root"></param>
    /// <param name="value"></param>
    [Command]
    private void SetPrefabLocation(GameObject root, int value)
    {
        var obj = Instantiate(root, transform);
        obj.GetComponent<RectTransform>().position = new Vector3(BattleBaseList[value].position.x, BattleBaseList[value].position.y + 10f);
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
}
