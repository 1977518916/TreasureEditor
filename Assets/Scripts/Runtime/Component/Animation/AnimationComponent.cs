using Spine.Unity;

public interface AnimationComponent : IComponent
{
    /// <summary>
    /// 动画组件
    /// </summary>
    public SkeletonGraphic SkeletonGraphic { set; }
    
    /// <summary>
    /// 获取动画组件
    /// </summary>
    /// <returns></returns>
    public SkeletonGraphic GetSkeletonGraphic();

    /// <summary>
    /// 改变动画
    /// </summary>
    public void ChangeAnima(StateType stateType, bool isLoop);
}
