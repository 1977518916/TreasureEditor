using Tools;
using UnityEngine;

public class BulletEntity : MonoBehaviour, Entity
{
    public long EntityId { get; set; }

    /// <summary>
    /// 移动时播放的动画物体
    /// </summary>
    public GameObject MoveObject { get; set; }
    /// <summary>
    /// 爆破时的动画物体(可能为空)
    /// </summary>
    public GameObject BoomObject { get; set; }

    public void Init()
    {
        EntityId = GlobalOnlyID.GetGlobalOnlyID();
    }


}