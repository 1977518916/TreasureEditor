﻿using System;
using System.Collections.Generic;
using Factories;
using Runtime.Component.Skill;
using Runtime.Data;
using Runtime.Manager;
using Runtime.Utils;
using Spine.Unity;
using Tao_Framework.Core.Event;
using Tao_Framework.Core.Singleton;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityTimer;

namespace Runtime.System
{
    public partial class SkillSystem : MonoSingleton<SkillSystem>
    {
        private Timer timer;
        private List<HeroEntity> heroEntities;

        private void Start()
        {
            heroEntities = EntitySystem.Instance.GetAllHeroEntity();
            EventMgr.Instance.RegisterEvent<KeyCode>(GetHashCode(), GameEvent.InvokeSkill, HandEvent);
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
                    Debug.Log($"按下{key}");
                    int keyCodeValue = (int)key + (key == KeyCode.Alpha0 ? 58 : 0) - 49;
                    //HeroSkillComponent skillComponent = GetHeroSkillOfPosition((DataType.HeroPositionType)(keyCodeValue / 2));
                    // if(skillComponent != null)
                    // {
                    //     int skillId = keyCodeValue % 2;
                    //     Debug.Log(TranslateUtil.TranslateUi(skillComponent.Entity.GetHeroData().modelType) + "释放技能" + skillId);
                    //     skillComponent.UseSkill(skillId);
                    // }
                    break;
            }
        }

        public void ShowSkill(float triggerTime, SkillData skillData, HeroEntity entity)
        {
            Timer.Register(triggerTime, () => { CreateSkill(skillData, entity); });
        }

        private void CreateSkill(SkillData skillData, HeroEntity entity)
        {
            PointDetectComponent pointDetectComponent = entity.GetSpecifyComponent<PointDetectComponent>(ComponentType.DetectComponent);
            if(!pointDetectComponent.GetTarget())
            {
                return;
            }
            switch(skillData.skillMoveType)
            {
                case SkillMoveType.Bullet:
                    CreateBullet(skillData, entity, pointDetectComponent);
                    break;
                case SkillMoveType.Fall:
                    CreateFall(skillData, entity, pointDetectComponent);
                    break;
                case SkillMoveType.Self:
                    CreateSelf(skillData, entity);
                    break;
                case SkillMoveType.Through:
                    CreateThrough(skillData, entity, pointDetectComponent);
                    break;
                case SkillMoveType.HideRange:
                    CreateHideRange(skillData, entity, pointDetectComponent);
                    break;
            }
        }

        private GameObject CreateSkillGo(HeroEntity entity, SkillData skillData)
        {
            GameObject go = new GameObject($"{skillData.key}");
            go.AddComponent<RectTransform>();
            go.transform.SetParent(entity.transform);
            SkillGameObjectFactory.Instance.Create(skillData, go.transform);
            return go;
        }
    }
}