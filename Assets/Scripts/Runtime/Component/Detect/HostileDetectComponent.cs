using System;
using System.Collections;
using System.Collections.Generic;
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

    public void Tick(float time)
    {
        
    }
}
