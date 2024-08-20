using UnityEngine;

/// <summary>
/// 方形范围检测组件
/// </summary>
public class SquareRayComponent : IRayComponent
{
    private Vector2 point;

    private Vector2 size;

    private float angle;
    
    private Collider2D hitObject;

    public SquareRayComponent(Vector2 point, Vector2 size, float angle)
    {
        this.point = point;
        this.size = size;
        this.angle = angle;
    }
    
    public bool IsObjectDetected(string targetTag, LayerMask layerMask)
    {
        var hit = Physics2D.OverlapBox(point, size, angle, layerMask);
        if (!hit.gameObject.CompareTag(targetTag)) return false;
        hitObject = hit;
        return true;
    }

    public Collider2D GetObjectDetected()
    {
        return hitObject;
    }
    
    /// <summary>
    /// 获取中心点和大小
    /// </summary>
    /// <returns></returns>
    public (Vector2, Vector2) GetPointAndSize()
    {
        return (point, size);
    }
}
