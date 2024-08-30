using UnityEngine;

/// <summary>
/// 子弹反弹特性
/// </summary>
public class BulletReboundAttribute : BulletAttribute
{
    /// <summary>
    /// 特性类型
    /// </summary>
    public BulletAttributeType AttributeType => BulletAttributeType.Rebound;
    
    /// <summary>
    /// 子弹实体
    /// </summary>
    private BulletEntity bulletEntity;

    /// <summary>
    /// 反弹次数
    /// </summary>
    private int reboundCount;

    public BulletReboundAttribute(int count, BulletEntity entity)
    {
        reboundCount = count;
        bulletEntity = entity;
    }

    public void Tick(float time)
    {
        
    }

    public void Release()
    {
        
    }
    
    public void Execute()
    {
        reboundCount -= 1;
        var move = bulletEntity.GetSpecifyComponent<BulletMoveComponent>(ComponentType.MoveComponent);
        move.MoveDirection = new Vector2(-move.MoveDirection.x, move.MoveDirection.y);
        if (reboundCount == 0)
            EntitySystem.Instance.ReleaseEntity(bulletEntity.EntityId);
    }
}
