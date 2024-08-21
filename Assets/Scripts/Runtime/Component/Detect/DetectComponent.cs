using UnityEngine;

/// <summary>
/// 检测组件
/// </summary>
public interface DetectComponent : IComponent
{
    /// <summary>
    /// 目标标签
    /// </summary>
    public string TargetTag { get; set; }

    /// <summary>
    /// 检测的层
    /// </summary>
    public LayerMask LayerMask { get; set; }

    /// <summary>
    /// 检测范围类型
    /// </summary>
    public DetectRangeType DetectRangeType { get; set; }
}
