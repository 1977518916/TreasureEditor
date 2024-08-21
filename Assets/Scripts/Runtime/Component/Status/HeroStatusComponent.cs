using DG.Tweening;
using Tao_Framework.Core.Event;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 英雄状态
/// </summary>
public class HeroStatusComponent : StatusComponent
{
    /// <summary>
    /// 血条父对象
    /// </summary>
    private readonly GameObject hpParent;

    /// <summary>
    /// 攻击CD进度条背景
    /// </summary>
    private readonly GameObject attackCBg;

    /// <summary>
    /// 血条
    /// </summary>
    private readonly Image hp;

    /// <summary>
    /// 攻击CD
    /// </summary>
    private readonly Image attackCd;

    /// <summary>
    /// 英雄实体
    /// </summary>
    private readonly HeroEntity heroEntity;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="hpParent"> 血条父对象 </param>
    /// <param name="attackCBg"> 攻击CD条父对象 </param>
    /// <param name="hp"> 血条 </param>
    /// <param name="attackCd"> 攻击CD </param>
    /// <param name="entity"> 英雄实体对象 </param>
    public HeroStatusComponent(GameObject hpParent, GameObject attackCBg, Image hp, Image attackCd, HeroEntity entity)
    {
        this.hpParent = hpParent;
        this.attackCBg = attackCBg;
        this.hp = hp;
        this.attackCd = attackCd;
        heroEntity = entity;
        ResetStatus();
        EventMgr.Instance.RegisterEvent<long>(GameEvent.AttackStartCd, WaitAttackCd);
    }
    
    public void Tick(float time)
    {
        
    }

    /// <summary>
    /// 隐藏血条
    /// </summary>
    private void HideHp()
    {
        hpParent.SetActive(false);
    }

    /// <summary>
    /// 显示血条
    /// </summary>
    private void ShowHp()
    {
        hpParent.SetActive(true);
    }

    /// <summary>
    /// 隐藏攻击CD进度条
    /// </summary>
    private void HideAttackCd()
    {
        attackCBg.SetActive(false);
    }

    /// <summary>
    /// 显示攻击CD进度条
    /// </summary>
    private void ShowAttackCd()
    {
        attackCBg.SetActive(true);
    }

    /// <summary>
    /// 等待攻击CD
    /// </summary>
    private void WaitAttackCd(long entityId)
    {
        if (entityId != heroEntity.EntityId) return;
        ShowAttackCd();
        attackCd.fillAmount = 0;
        attackCd.DOFillAmount(1, 3f).onComplete += () =>
        {
            // 通知继续攻击的事件
            EventMgr.Instance.TriggerEvent(GameEvent.AttackEndCd, heroEntity.EntityId);
        };
    }

    /// <summary>
    /// 受击 血量扣到指定进度
    /// </summary>
    public void Hit(float value)
    {
        hp.DOFillAmount(value, 0.25f).onComplete+= () =>
        {
            if (hp.fillAmount > 0) return;
            EventMgr.Instance.TriggerEvent(GameEvent.EntityDead, heroEntity.EntityId);
        };
    }

    /// <summary>
    /// 重置状态
    /// </summary>
    private void ResetStatus()
    {
        ShowHp();
        HideAttackCd();
        attackCd.fillAmount = 0;
        hp.fillAmount = 1;
    }
}
