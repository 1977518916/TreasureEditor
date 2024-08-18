using UnityEngine;

public interface AttackComponent : IComponent
{
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float AttackInterval { get; set; }

    /// <summary>
    /// 攻击  
    /// </summary>
    /// <param name="time"> 时间 </param>
    /// <param name="point">目标点</param>
    public void Attack(float time,Vector2 point);
}
