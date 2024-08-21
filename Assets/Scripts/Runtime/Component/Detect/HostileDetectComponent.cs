using UnityEngine;

/// <summary>
/// 敌对检测
/// </summary>
public class HostileDetectComponent : DetectComponent
{
    public string TargetTag { get; set; }
    
    public LayerMask LayerMask { get; set; }
    
    public DetectRangeType DetectRangeType { get; set; }
    
    public IRayComponent IRayComponent { get; set; }

    /// <summary>
    /// 英雄实体
    /// </summary>
    private Entity entity;

    public HostileDetectComponent(string targetTag, LayerMask layerMask, DetectRangeType type, Entity entity)
    {
        TargetTag = targetTag;
        LayerMask = layerMask;
        DetectRangeType = type;
        this.entity = entity;
        //IRayComponent = new SquareRayComponent();
    }

    public void Tick(float time)
    {
        
    }
}
