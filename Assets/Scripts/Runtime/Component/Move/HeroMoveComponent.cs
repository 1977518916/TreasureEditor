using UnityEngine;

public class HeroMoveComponent : MoveComponent
{
    /// <summary>
    /// 英雄暂时没有移动所以只需要位置组件即可
    /// </summary>
    /// <param name="entityTransform"></param>
    public HeroMoveComponent(RectTransform entityTransform)
    {
        EntityTransform = entityTransform;
    }

    public void Tick(float time)
    {
        
    }

    public RectTransform EntityTransform { get; set; }
    public float MoveSpeed { get; set; }
    public Vector2 MoveDirection { get; set; }
    public void Move(float time)
    {
        
    }

    public bool ContinueMove { get; set; }
}
