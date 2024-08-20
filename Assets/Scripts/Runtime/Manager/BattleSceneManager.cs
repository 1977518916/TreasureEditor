using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Manager
{
    public static class BattleSceneManager
    {
        private const string BattleSceneName = "BattleScene";

        public static void LoadBattleScene()
        {
            SceneManager.LoadScene(BattleSceneName, LoadSceneMode.Additive);
        }

        public static void QuitBattle()
        {
            SceneManager.UnloadSceneAsync(BattleSceneName);
        }

    }
}