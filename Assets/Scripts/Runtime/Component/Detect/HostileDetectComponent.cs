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
    private HeroEntity heroEntity;

    public HostileDetectComponent(string targetTag, LayerMask layerMask, DetectRangeType type, HeroEntity heroEntity)
    {
        TargetTag = targetTag;
        LayerMask = layerMask;
        DetectRangeType = type;
        this.heroEntity = heroEntity;
        //IRayComponent = new SquareRayComponent();
    }

    public void Tick(float time)
    {
        
    }
}
