using System.Collections;
using System.Collections.Generic;
using Tao_Framework.Core.Event;
using UnityEngine;

/// <summary>
/// 敌人分为 小怪和Boss  小怪是没有血条的只有受击次数 几下会死  现在默认一下会死
/// </summary>
public class EnemyStatusComponent : StatusComponent
{
    /// <summary>
    /// 受击次数   需要改成血量
    /// </summary>
    private int hpValue;
    
    /// <summary>
    /// 最大血量值
    /// </summary>
    private int maxHpValue;

    public EnemyStatusComponent(int maxHpValue)
    {
        this.maxHpValue = maxHpValue;
        hpValue = this.maxHpValue;
    }

    public void Tick(float time)
    {
        
    }

    /// <summary>
    /// 每调用一次会扣除一次受击次数并且会 进入 受击状态
    /// </summary>
    public void Hit(int value)
    {
        hpValue -= value;
        if (hpValue > 0) 
        {
            // 伤害飘字
            // 播放受击动画
            // 然后退出
            return;
        }
        // 死亡 播放死亡动画 关闭碰撞包围盒  死亡动画播放完以后直接删除实体
        EventMgr.Instance.TriggerEvent(GameEvent.Common);
    }
}
