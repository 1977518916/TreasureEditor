using Runtime.Data;
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
        public readonly HeroEntity Entity;
        //public DataType.HeroPositionType positionType;
        public HeroSkillComponent(HeroEntity heroEntity)
        {
            Entity = heroEntity;
        }

        public void Tick(float time)
        {
        }
        public void Release()
        {
        }

        public void UseSkill(int id)
        {
            StateType stateType = id == 0 ? StateType.Skill_1 : StateType.Skill_2;
            SkillData skillData = id == 0 ? Entity.GetHeroData().skillData1 : Entity.GetHeroData().skillData2;
            //避免缓存的技能数据不是最新的
            skillData = DataManager.SkillStruct.GetSkillDataOfKey(skillData.key);
            Entity.GetSpecifyComponent<StateMachineComponent>(ComponentType.StateMachineComponent).ChangeState(stateType);
            SkillSystem.Instance.ShowSkill(1, skillData, Entity);
        }
    }
}