using System.Collections;
using System.Collections.Generic;
using Runtime.Data;
using Runtime.Manager;
using UnityEngine;

public class BulletSplitAttribute : BulletAttribute
{
    public BulletAttributeType AttributeType => BulletAttributeType.Split;
    
    /// <summary>
    /// 分裂的数量
    /// </summary>
    private int splitCount;
    
    /// <summary>
    /// 英雄数据
    /// </summary>
    private readonly HeroData data;

    /// <summary>
    /// 子弹实体
    /// </summary>
    private readonly BulletEntity bulletEntity;

    /// <summary>
    /// 方向
    /// </summary>
    private readonly Vector2 direction;
    
    public BulletSplitAttribute(int count, BulletEntity entity, HeroData data,Vector2 location ,Vector2 direction)
    {
        this.data = data;
        splitCount = count;
        bulletEntity = entity;
        this.direction = direction;
    }

    public void Tick(float time)
    {
        
    }

    public void Release()
    {
        
    }

    public void Execute()
    {
        float deviation = 10f;
        for (var i = 0; i < splitCount; i++)
        {
            var rate = ((i + 1) / 0b10) * Mathf.Pow(-1, i);
            Split(GetOtherPoint(deviation * rate,
                bulletEntity.GetSpecifyComponent<BulletMoveComponent>(ComponentType.MoveComponent).EntityTransform
                    .anchoredPosition, direction));
        }

        EntitySystem.Instance.ReleaseEntity(bulletEntity.EntityId);
    }

    private Vector2 GetOtherPoint(float angle, Vector2 location, Vector2 target)
    {
        Vector2 AB = target - location;
        float lengthAB = AB.magnitude;
        float lengthAC = Mathf.Cos(Mathf.Deg2Rad * angle) * lengthAB;
        Vector2 unitAB = AB / lengthAB;
        float cosAngle = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sinAngle = Mathf.Sin(angle * Mathf.Deg2Rad);
        Matrix4x4 rotationMatrix = new Matrix4x4(
            new Vector4(cosAngle, -sinAngle, 0, 0),
            new Vector4(sinAngle, cosAngle, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );

        Vector2 rotatedVector = rotationMatrix.MultiplyVector(unitAB * lengthAC);
        return location + new Vector2(rotatedVector.x, rotatedVector.y);
    }

    private void Split(Vector2 target)
    {
        var bulletGo = AssetsLoadManager.LoadBullet(data.modelType, LayerMask.NameToLayer("BattleUI"));
        var splitBullet = EntitySystem.Instance.CreateEntity<BulletEntity>(EntityType.BulletEntity, bulletGo);
        splitBullet.MoveObject = bulletGo.transform.GetChild(0).gameObject;
        var bulletHurt = DataManager.GetRuntimeData().isInvicibleEnemy ? 1 : data.atk;
        splitBullet.InitBullet(EntityType.EnemyEntity, bulletHurt, BulletAttributeType.Penetrate,
            bulletEntity.GetSpecifyComponent<BulletMoveComponent>(ComponentType.MoveComponent).EntityTransform.anchoredPosition,
            BattleManager.Instance.GetBulletParent());
        splitBullet.AllComponentList.Add(new BulletMoveComponent(splitBullet.GetComponent<RectTransform>(), target,
            800f, BulletMoveType.RectilinearMotion, 2000f));
        splitBullet.AllComponentList.Add(new DelayedDeadComponent(3f, bulletEntity));
        splitBullet.AllComponentList.Add(new BulletPenetrateAttribute(2, splitBullet));
        splitBullet.AllComponentList.Add(new DelayedDeadComponent(1.5f, splitBullet));
    }
}
