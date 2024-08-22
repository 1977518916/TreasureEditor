using Runtime.Manager;
using UnityEngine;

/// <summary>
/// 敌人攻击
/// </summary>
public class EnemyAttackComponent : AttackComponent
{
    public bool IsInAttackInterval { get; set; }
    public float LastAttackTime { get; set; }
    public float AttackInterval { get; set; }
    
    private EnemyEntity entity;

    private readonly int hurt;

    public EnemyAttackComponent(float attackInterval, int hurt, EnemyEntity entity)
    {
        AttackInterval = attackInterval;
        this.entity = entity;
        this.hurt = hurt;
    }

    public void Tick(float time)
    {
        if (IsInAttackInterval)
        {
            // 攻击间隔的时间减去当前时间 如果大于攻击间隔时间 则证明攻击间隔时间结束了 那么就需要退出攻击间隔状态  所以 IsInAttackInterval 此时等于 false
            IsInAttackInterval = !(LastAttackTime - Time.time >= AttackInterval);
        }
    }

    public void Release()
    {
        
    }

    public void Attack(float time, Vector2 point)
    {
        if (IsInAttackInterval) return;
        var bullet = AssetsLoadManager.Load<GameObject>("Prefabs/Small_EnemyAttackBox");
        var bulletEnemy = bullet.AddComponent<BulletEntity>();
        bulletEnemy.Init();
        bulletEnemy.InitBullet(EntityType.BulletEnemy, hurt, 1);
    }
}
