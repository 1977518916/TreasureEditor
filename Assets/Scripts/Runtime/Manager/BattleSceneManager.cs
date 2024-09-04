using Runtime.UI;
using Tao_Framework.Core.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Manager
{
    public static class BattleSceneManager
    {
        private const string BattleSceneName = "BattleScene";

        public static void LoadBattleScene()
        {
            CommonUi.Instance.gameObject.SetActive(false);
            SceneManager.LoadScene(BattleSceneName, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += (arg0, mode) =>
            {
                if (arg0.buildIndex == 1)
                {
                    EventMgr.Instance.TriggerEvent(GameEvent.EnterBattle);
                }
            };
        }

        public static void QuitBattle()
        {
            EntitySystem.Instance.Destroy(() =>
            {
                SceneManager.UnloadSceneAsync(BattleSceneName);
            });
            CommonUi.Instance.gameObject.SetActive(true);
        }

    }
}