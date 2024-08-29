using Runtime.Manager;
using Runtime.System;
using Runtime.Utils;
using Tao_Framework.Core.Event;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Runtime.Component.Skill
{
    public class HeroSkillComponent : SkillComponent
    {
        public HeroEntity entity;
        public DataType.HeroPositionType positionType;
        public HeroSkillComponent(DataType.HeroPositionType positionType, HeroEntity heroEntity)
        {
            entity = heroEntity;
            this.positionType = positionType;
        }
        
        public void Tick(float time)
        {
        }
        public void Release()
        {
        }

        public void UseSkill(int id)
        {
            StateType stateType = id == 1 ? StateType.Skill_1 : StateType.Skill_2;
            entity.GetSpecifyComponent<StateMachineComponent>(ComponentType.StateMachineComponent).ChangeState(stateType);
            SkillSystem.Instance.ShowSkill(1, null, entity);
        }
    }
}