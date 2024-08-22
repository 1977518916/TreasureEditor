using Spine.Unity;

public class SkillState : IState
{
    public StateType StateType => StateType.Skill;
    public AnimationComponent AnimationComponent { get; set; }
    
    public void Init(AnimationComponent animationComponent)
    {
        AnimationComponent = animationComponent;
    }
    
    public void Enter(StateMachineComponent stateMachineComponent)
    {

    }

    public void Tick()
    {
        
    }

    public void Exit()
    {
        
    }
    public int Priority()
    {
        return 9;
    }
}
