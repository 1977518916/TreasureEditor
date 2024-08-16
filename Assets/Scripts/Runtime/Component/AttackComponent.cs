
using UnityEngine;

public interface AttackComponent : IComponent
{
    /// <summary>
    /// 攻击  
    /// </summary>
    /// <param name="point">目标点</param>
    public void Attack(Vector2 point);
}
