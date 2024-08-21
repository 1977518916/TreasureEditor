using UnityEngine;

/// <summary>
/// 敌对检测   改为距离检测 射线检测作废
/// </summary>
public class HostileDetectComponent : DetectComponent
{
    public string TargetTag { get; set; }
    
    public LayerMask LayerMask { get; set; }
    
    public DetectRangeType DetectRangeType { get; set; }
    
    /// <summary>
    /// 英雄实体
    /// </summary>
    private Entity entity;
    
    public HostileDetectComponent(string targetTag, LayerMask layerMask, DetectRangeType type, Entity entity, RectTransform entityRectTransform)
    {
        TargetTag = targetTag;
        LayerMask = layerMask;
        DetectRangeType = type;
        this.entity = entity;
    }
    
    public void Tick(float time)
    {
        
    }
}
