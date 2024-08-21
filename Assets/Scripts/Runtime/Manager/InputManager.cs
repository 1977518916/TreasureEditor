using System;
using Tao_Framework.Core.Singleton;
using UnityEngine;

namespace Runtime.Manager
{
    public class InputManager : MonoSingleton<InputManager>
    {

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                BattleSceneManager.QuitBattle();
            }
        }
    }
}