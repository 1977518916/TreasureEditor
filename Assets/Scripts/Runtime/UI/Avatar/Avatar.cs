using System;
using System.Collections;
using DG.Tweening;
using Tao_Framework.Core.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 英雄图标控制
/// </summary>
public class Avatar : MonoBehaviour
{
    /// <summary>
    /// 英雄图标背景
    /// </summary>
    public Image avatarBg;

    /// <summary>
    /// 英雄图标
    /// </summary>
    public Image avatarSprite;

    /// <summary>
    /// 职业图标背景
    /// </summary>
    public Image careerIconBg;

    /// <summary>
    /// 职业图标
    /// </summary>
    public Image careerIcon;

    /// <summary>
    /// 等级文本
    /// </summary>
    public TextMeshProUGUI levelTMP;

    /// <summary>
    /// 技能图标
    /// </summary>
    public Image skillIcon;

    /// <summary>
    /// 技能次数
    /// </summary>
    public TextMeshProUGUI skillCount;
    
    /// <summary>
    /// 技能CD
    /// </summary>
    public Image skillCd;

    /// <summary>
    /// 血条
    /// </summary>
    public Image lifeBar;

    /// <summary>
    /// 技能能量积攒
    /// </summary>
    public Image skillBar;

    /// <summary>
    /// 英雄图标数据
    /// </summary>
    private AvatarData Data;
    
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="data"></param>
    public void Init(AvatarData data)
    {
        avatarBg.sprite = data.AvatarBg;
        avatarSprite.sprite = data.AvatarSprite;
        careerIconBg.sprite = data.CareerIconBg;
        careerIcon.sprite = data.CareerIcon;
        levelTMP.text = $"{data.Level}";
        skillIcon.sprite = data.SkillIcon;
        skillCount.text = $"{data.SkillCount}";
        skillCd.fillAmount = 1;
        lifeBar.fillAmount = 1;
        skillBar.fillAmount = 0;
    }
    
    /// <summary>
    /// 增加英雄等级
    /// </summary>
    public void AddAvatarLevel()
    {
        Data.Level = Data.Level + 1 > 5 ? 5 : Data.Level + 1;
        levelTMP.text = $"{Data.Level}";
    }
    
    /// <summary>
    /// 重置英雄等级
    /// </summary>
    public void ResetAvatarLevel()
    {
        Data.Level = 1;
        levelTMP.text = $"{Data.Level}";
    }
    
    /// <summary>
    /// 减少技能次数
    /// </summary>
    public void ReduceSkillCount()
    {
        var value = Convert.ToInt32(skillCount.text);
        value -= 1;
        if (value >= 1)
        {
            skillCount.text = $"{value}";
        }
        else
        {
            EventMgr.Instance.TriggerEvent(GameEvent.AttackStartCd);
            skillCount.text = $"9";
            skillCount.color = Color.clear;
            skillCd.DOFillAmount(0f, 3f).onComplete += SkillCdEnd;
        }
    }
    
    /// <summary>
    /// 技能CD结束
    /// </summary>
    private void SkillCdEnd()
    {
        skillCount.color = Color.white;
        EventMgr.Instance.TriggerEvent(GameEvent.AttackEndCd);
    }
}

/// <summary>
/// 英雄图标数据
/// </summary>
public struct AvatarData
{
    /// <summary>
    /// 英雄图标背景
    /// </summary>
    public Sprite AvatarBg;

    /// <summary>
    /// 英雄图标图片
    /// </summary>
    public Sprite AvatarSprite;

    /// <summary>
    /// 职业图标背景
    /// </summary>
    public Sprite CareerIconBg;

    /// <summary>
    /// 职业图标
    /// </summary>
    public Sprite CareerIcon;

    /// <summary>
    /// 等级
    /// </summary>
    public int Level;

    /// <summary>
    /// 技能图标
    /// </summary>
    public Sprite SkillIcon;
    
    /// <summary>
    /// 技能次数  默认是9
    /// </summary>
    public int SkillCount;
}