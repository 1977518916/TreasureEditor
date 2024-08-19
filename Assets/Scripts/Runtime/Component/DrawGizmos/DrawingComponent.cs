using System;
using UnityEngine;

/// <summary>
/// 绘制组件
/// </summary>
public class DrawingComponent : MonoBehaviour, IComponent
{
    private DetectComponent detectComponent;
    
    private void Start()
    {
        detectComponent = (DetectComponent)GetComponent<Entity>().GetSpecifyComponent(ComponentType.DetectComponent);
    }
    
    public void Tick(float time)
    {
        
    }
    
    private void OnDrawGizmos()
    {
        switch (detectComponent.DetectRangeType)
        {
            case DetectRangeType.Square:
                DrawSquare();
                break;
            case DetectRangeType.Round:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    /// <summary>
    /// 绘制矩形
    /// </summary>
    private void DrawSquare()
    {
        var data = ((detectComponent as HostileDetectComponent)?.IRayComponent as SquareRayComponent)?.GetPointAndSize();
        if (data != null) Gizmos.DrawCube(data.Value.Item1, data.Value.Item2);
    }
}
