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
            SceneManager.sceneLoaded += LoadScene;
        }

        public static void QuitBattle()
        {
            SceneManager.sceneLoaded -= LoadScene;
            EntitySystem.Instance.Destroy(() => { SceneManager.UnloadSceneAsync(BattleSceneName); });
            CommonUi.Instance.gameObject.SetActive(true);
        }

        public static void LoadScene(Scene scene, LoadSceneMode mode)
        {
            if(scene.buildIndex == 1)
            {
                EventMgr.Instance.TriggerEvent(GameEvent.EnterBattle);
            }
        }
    }
}