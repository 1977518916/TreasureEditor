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
    
    
    public void Init()
    {
        
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
}