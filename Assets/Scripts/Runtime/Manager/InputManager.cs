using System;
using Tao_Framework.Core.Singleton;
using UnityEngine;

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
            if(!Input.GetKeyDown(KeyCode.Alpha1))
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
    }
}