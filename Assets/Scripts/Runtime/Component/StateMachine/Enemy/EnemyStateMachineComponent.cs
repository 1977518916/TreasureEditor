using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 敌人状态机
/// </summary>
public class EnemyStateMachineComponent : StateMachineComponent
{
    public IState CurrentState { get; set; }
    
    public IState LastState { get; set; }
    
    public List<StateConvert> StateConvertList { get; set; }
    
    /// <summary>
    /// 敌人实体
    /// </summary>
    private EnemyEntity enemyEntity;
    
    /// <summary>
    /// 动画组件
    /// </summary>
    private AnimationComponent animationComponent;
    
    public void Init(Entity entity, IState initState, List<StateConvert> stateConverts)
    {
        this.enemyEntity = (EnemyEntity)entity;
        animationComponent = entity.GetSpecifyComponent<AnimationComponent>(ComponentType.AnimationComponent);
        CurrentState = initState;
        StateConvertList = stateConverts;
        CurrentState.Init(animationComponent.GetSkeletonGraphic());
    }

    public void ChangeState(IState changeState)
    {
        LastState = CurrentState;
        CurrentState = changeState;
        CurrentState.Init(animationComponent.GetSkeletonGraphic());
        CurrentState.Enter();
    }
    
    public void TryChangeState(IState changeState)
    {
        foreach (var stateConvert in from stateConvert in StateConvertList
                 where stateConvert.CurrentState == CurrentState.StateType
                 from state in stateConvert.ChangeState.Where(state => state.StateType == changeState.StateType)
                 select stateConvert)
        {
            ChangeState(changeState);
        }
    }
    
    public void Tick(float time)
    {
        CurrentState.Tick();
    }
}
