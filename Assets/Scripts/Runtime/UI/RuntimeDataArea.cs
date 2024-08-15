using System;
using Runtime.Data;
using Runtime.Extensions;
using Runtime.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class RuntimeDataArea : MonoBehaviour
    {

        private void Awake()
        {
            DataManager.GameData = ReadWriteManager.Read("GameRunTimeData", new GameData());
            
            transform.FindAdd<ToggleData>("Units/Self").Action = invicible => DataManager.GameData.isInvicibleSelf = invicible;
            transform.FindAdd<ToggleData>("Units/Enemy").Action = invicible => DataManager.GameData.isInvicibleEnemy = invicible;
            transform.FindAdd<ToggleData>("Other/NumberShow").Action = show => DataManager.GameData.isShowNumber = show;
        }

        private class ToggleData : MonoBehaviour
        {
            public UnityAction<bool> Action;
            private void Start()
            {
                var t = transform.GetComponentInChildren<Toggle>();
                t.onValueChanged.AddListener(Action);
            }
        }
    }
}