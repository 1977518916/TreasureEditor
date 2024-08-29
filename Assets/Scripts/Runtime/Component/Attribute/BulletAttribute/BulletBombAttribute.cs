using UnityEngine;

/// <summary>
/// 子弹爆炸效果
/// </summary>
public class BulletBombAttribute : BulletAttribute
{
    public BulletAttributeType AttributeType => BulletAttributeType.Bomb;
    
    /// <summary>
    /// 子弹爆炸物
    /// </summary>
    private GameObject bulletBomb;
    
    /// <summary>
    /// 位置
    /// </summary>
    private Vector2 location;

    public BulletBombAttribute(GameObject bomb, Vector2 location)
    {
        bulletBomb = bomb;
        this.location = location;
    }

    public void Tick(float time)
    {
        
    }

    public void Release()
    {
        
    }
    
    public void Execute()
    {
        var bombObject = Object.Instantiate(bulletBomb);
    }
}
