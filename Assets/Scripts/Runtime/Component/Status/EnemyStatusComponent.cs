using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人分为 小怪和Boss  小怪是没有血条的只有受击次数 几下会死  现在默认一下会死
/// </summary>
public class EnemyStatusComponent : StatusComponent
{
    /// <summary>
    /// 受击次数
    /// </summary>
    private int hitCount;

    public EnemyStatusComponent(int hitCount)
    {
        this.hitCount = hitCount;
    }

    public void Tick(float time)
    {
        
    }
    
    /// <summary>
    /// 每调用一次会扣除一次受击次数并且会 进入 受击状态
    /// </summary>
    public void Hit()
    {
        hitCount -= 1;
        if (hitCount <= 0) 
        {
            // 死亡
        }
        else
        {
            // 受击
        }
    }
}
