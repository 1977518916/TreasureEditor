using UnityEngine;

/// <summary>
/// 移动组件
/// </summary>
public interface MoveComponent : IComponent
{
    /// <summary>
    /// 实体移动组件
    /// </summary>
    public RectTransform EntityTransform { get; set; }

    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed { get; set; }

    /// <summary>
    /// 移动方向 或者 目标点
    /// </summary>
    public Vector2 MoveDirection { get; set; }

    /// <summary>
    /// 移动函数
    /// </summary>
    public void Move(float time);
    
    /// <summary>
    /// 是否继续移动
    /// </summary>
    public bool ContinueMove { get; set; }
}
