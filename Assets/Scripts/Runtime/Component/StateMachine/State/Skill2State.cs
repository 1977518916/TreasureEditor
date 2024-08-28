public class Skill2State : IState
{

    public StateType StateType => StateType.Skill_1;
    public AnimationComponent AnimationComponent { get; set; }
    public void Init(AnimationComponent animationComponent)
    {
        AnimationComponent = animationComponent;
    }
    public void Enter(StateMachineComponent stateMachine)
    {
        AnimationComponent.ChangeAnima(StateType.Skill_2, false, () => { stateMachine.ChangeState(stateMachine.LastState); });
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