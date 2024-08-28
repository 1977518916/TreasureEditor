using Runtime.Manager;
using Runtime.Utils;
using Tao_Framework.Core.Event;
using UnityEngine;

namespace Runtime.Component.Skill
{
    public class HeroSkillComponent : SkillComponent
    {
        private HeroEntity entity;
        private DataType.HeroPositionType positionType;
        public HeroSkillComponent(DataType.HeroPositionType positionType, HeroEntity heroEntity)
        {
            entity = heroEntity;
            this.positionType = positionType;
            RegisterSkill();
        }

        private void RegisterSkill()
        {
            EventMgr.Instance.RegisterEvent<KeyCode>(entity.EntityId, GameEvent.InvokeSkill, HandEvent);
        }

        public void Tick(float time)
        {
        }
        public void Release()
        {
            EventMgr.Instance.RemoveEvent(entity.EntityId, GameEvent.InvokeSkill);
        }

        private void HandEvent(KeyCode key)
        {
            switch(key)
            {
                case KeyCode.Alpha0:
                case KeyCode.Alpha1:
                case KeyCode.Alpha2:
                case KeyCode.Alpha3:
                case KeyCode.Alpha4:
                case KeyCode.Alpha5:
                case KeyCode.Alpha6:
                case KeyCode.Alpha7:
                case KeyCode.Alpha8:
                case KeyCode.Alpha9:
                    int keyCodeValue = (int)key + (key == KeyCode.Alpha0 ? 10 : 0);
                    int skillId = keyCodeValue - (int)positionType * 2 - (int)KeyCode.Alpha1 + 1;
                    if(skillId is < 3 and > 0)
                    {
                        Debug.Log(TranslateUtil.TranslateUi(entity.GetHeroData().heroTypeEnum) + "释放技能" + skillId);
                        UseSkill(skillId);
                    }
                    break;
            }
        }

        public void UseSkill(int id)
        {
            StateType stateType = id == 1 ? StateType.Skill_1 : StateType.Skill_2;
            entity.GetSpecifyComponent<StateMachineComponent>(ComponentType.StateMachineComponent).ChangeState(stateType);
        }
    }
}