/// <summary>
/// 子弹特性类型
/// </summary>
public enum BulletAttributeType
{
    /// <summary>
    /// 穿透
    /// </summary>
    Penetrate,

    /// <summary>
    /// 反弹
    /// </summary>
    Rebound,

    /// <summary>
    /// 折射  
    /// </summary>
    Refraction,

    /// <summary>
    /// 爆炸  打中敌人后会产生爆炸物,爆炸物爆炸产生伤害
    /// </summary>
    Bomb,

    /// <summary>
    /// 回旋  理解成回旋镖就行了 打出去到最远距离会再回到英雄位置
    /// </summary>
    Boomerang,
    
    /// <summary>
    /// 分裂  打中敌人以后会分裂成多个
    /// </summary>
    Split
}