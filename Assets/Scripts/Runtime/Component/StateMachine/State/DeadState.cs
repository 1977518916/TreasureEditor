using Spine.Unity;

public class DeadState : IState
{
    public StateType StateType => StateType.Dead;
    public SkeletonGraphic SkeletonGraphic { get; set; }

    public void Init(SkeletonGraphic skeletonGraphic)
    {
        
    }

    public void Enter()
    {

    }

    public void Tick()
    {

    }

    public void Exit()
    {

    }
}
