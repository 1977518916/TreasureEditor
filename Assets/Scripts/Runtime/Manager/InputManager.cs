using System;
using Tao_Framework.Core.Event;
using Tao_Framework.Core.Singleton;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Manager
{
    public class InputManager : MonoSingleton<InputManager>
    {
        public Canvas canvas;
        private GameObject runDataUI;
        private void Update()
        {
            TryQuitBattle();
            TryShowHideRunTimeData();
            TryUseSkill();
            AllEnemyEntityDead();
        }

        private void TryQuitBattle()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                BattleSceneManager.QuitBattle();
            }
        }
        
        private void TryShowHideRunTimeData()
        {
            if(!Input.GetKeyDown(KeyCode.P))
            {
                return;
            }
            if(runDataUI == null)
            {
                runDataUI = Instantiate(AssetsLoadManager.Load<GameObject>("Prefabs/RunTimeSetting"), canvas.transform);
                return;
            }
            runDataUI.SetActive(!runDataUI.activeSelf);
        }
        
        /// <summary>
        /// 所有敌人死亡
        /// </summary>
        private void AllEnemyEntityDead()
        {
            if (!Input.GetKeyDown(KeyCode.J)) return;
            DataManager.GameData.isInvicibleEnemy = false;
            foreach (var enemy in EntitySystem.Instance.GetAllEnemyEntity())
            {
                enemy.GetSpecifyComponent<EnemyStatusComponent>(ComponentType.StatusComponent).Hit(int.MinValue);
            }

            LevelManager.Instance.StopMakeEnemy();
        }

        private void TryUseSkill()
        {
            for(int i = (int)KeyCode.Alpha0; i <= (int)KeyCode.Alpha9; i++)
            {
                if(!Input.GetKeyDown((KeyCode)i)) continue;
                EventMgr.Instance.TriggerEvent(GameEvent.InvokeSkill, (KeyCode)i);
                break;
            }
        }
    }
}