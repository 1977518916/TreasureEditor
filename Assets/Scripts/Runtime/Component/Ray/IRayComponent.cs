using UnityEngine;

/// <summary>
/// 射线组件
/// </summary>
public interface IRayComponent
{
    /// <summary>
    /// 是否检测到物体
    /// </summary>
    /// <returns></returns>
    public bool IsObjectDetected(string targetTag, LayerMask layerMask);
    
    /// <summary>
    /// 获取检测到的物体
    /// </summary>
    /// <returns></returns>
    public Collider2D GetObjectDetected();
}
