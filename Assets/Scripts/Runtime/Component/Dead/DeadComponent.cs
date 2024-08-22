using UnityEngine;

/// <summary>
/// 死亡组件
/// </summary>
public interface DeadComponent : IComponent
{
    /// <summary>
    /// 实体ID
    /// </summary>
    public long EntityID { get; set; }

    /// <summary>
    /// 死亡接口
    /// </summary>
    public void Dead();
}
