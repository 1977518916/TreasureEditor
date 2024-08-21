using UnityEngine;

public class EnemyAttackComponent : AttackComponent
{
    public bool IsInAttackInterval { get; set; }
    public float LastAttackTime { get; set; }
    public float AttackInterval { get; set; }

    public EnemyAttackComponent()
    {
        
    }

    public void Tick(float time)
    {
        
    }

    public void Attack(float time, Vector2 point)
    {
        
    }
}
