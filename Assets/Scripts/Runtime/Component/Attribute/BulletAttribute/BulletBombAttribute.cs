using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 子弹爆炸效果
/// </summary>
public class BulletBombAttribute : BulletAttribute
{
    public BulletAttributeType AttributeType { get; }
    
    /// <summary>
    /// 子弹实体
    /// </summary>
    private BulletEntity entity;
    
    /// <summary>
    /// 子弹爆炸物
    /// </summary>
    private GameObject bulletBomb;
    
    /// <summary>
    /// 位置
    /// </summary>
    private Vector2 location;
    
    /// <summary>
    /// 目标标签
    /// </summary>
    private string targetTag;
    
    /// <summary>
    /// 伤害值
    /// </summary>
    private int hurtValue;

    public BulletBombAttribute(GameObject bomb, Vector2 location, string targetTag, int hurtValue)
    {
        bulletBomb = bomb;
        this.location = location;
        this.targetTag = targetTag;
        this.hurtValue = hurtValue;
    }

    public void Tick(float time)
    {
        
    }

    public void Release()
    {
        
    }
    
    public void Execute()
    {
        EntitySystem.Instance.ReleaseEntity(entity.EntityId);
        var bombObject = Object.Instantiate(bulletBomb, BattleManager.Instance.BulletDerivativeParent);
        bombObject.GetComponent<RectTransform>().anchoredPosition = location;
        var boomEntity = bombObject.AddComponent<BoomEntity>();
        boomEntity.Init(targetTag, hurtValue);
    }
}
