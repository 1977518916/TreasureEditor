public class Skill1State : IState
{
    public StateType StateType => StateType.Skill_1;
    public AnimationComponent AnimationComponent { get; set; }
    public void Init(AnimationComponent animationComponent)
    {
        AnimationComponent = animationComponent;
    }
    public void Enter(StateMachineComponent stateMachine)
    {
        AnimationComponent.ChangeAnima(StateType.Skill_1_Guide, false, () =>
        {
            AnimationComponent.ChangeAnima(StateType.Skill_1, false, () =>
            {
                if(AnimationComponent.HasAnimation(StateType.Skill_1_End))
                {
                    AnimationComponent.ChangeAnima(StateType.Skill_1_End,false,() =>
                    {
                        stateMachine.ChangeState(stateMachine.LastState);   
                    });
                }
                else
                {
                    stateMachine.ChangeState(stateMachine.LastState);   
                }
            });
        });
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