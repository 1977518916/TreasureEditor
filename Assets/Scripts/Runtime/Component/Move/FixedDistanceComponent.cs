using UnityEngine;
using UnityTimer;

public class FixedDistanceComponent : MoveComponent
{
    public void Tick(float time)
    {
        Move(time);
    }

    public void Release()
    {
        timer?.Cancel();
        timer = null;
    }

    public RectTransform EntityTransform { get; set; }
    public float MoveSpeed { get; set; }
    public bool ContinueMove { get; set; }
    public Vector2 MoveDirection { get; set; }
    private readonly Vector2 targetLocation;
    /// <summary>
    /// 定时器
    /// </summary>
    private Timer timer;
    /// <summary>
    /// 是否停止移动
    /// </summary>
    private bool isStopMove;
    /// <summary>
    /// 固定距离移动  用于只会移动一段固定距离的逻辑
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="moveSpeed"></param>
    /// <param name="moveDirection"></param>
    /// <param name="targetLocation"></param>
    public FixedDistanceComponent(RectTransform transform, float moveSpeed, Vector2 moveDirection, Vector2 targetLocation)
    {
        EntityTransform = transform;
        MoveSpeed = moveSpeed;
        MoveDirection = moveDirection;
        this.targetLocation = targetLocation;
        ContinueMove = true;
    }

    public void Move(float time)
    {
        if (!ContinueMove) return;
        if (isStopMove) return;
        EntityTransform.anchoredPosition3D +=
            new Vector3(MoveDirection.x * MoveSpeed * time, MoveDirection.y * MoveSpeed * time, 0f);
        if (Vector2.Distance(EntityTransform.anchoredPosition, targetLocation) <= 1f)
        {
            ContinueMove = false;
        }
    }

    /// <summary>
    /// 是否还在继续移动
    /// </summary>
    /// <returns></returns>
    public bool IsContinueMove()
    {
        return ContinueMove;
    }
    
    /// <summary>
    /// 停止移动
    /// </summary>
    /// <param name="time"></param>
    public void StopMove(float time)
    {
        isStopMove = true;
        timer?.Cancel();
        timer = Timer.Register(time, () => isStopMove = false);
    }
}
