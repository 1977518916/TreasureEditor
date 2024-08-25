using Runtime.Extensions;
using Runtime.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class CommonUi : MonoBehaviour
    {
        private void Awake()
        {
            DataManager.InitData();
            transform.FindGet<Button>("EnterLevel").onClick.AddListener(BattleSceneManager.LoadBattleScene);
        }
    }
}