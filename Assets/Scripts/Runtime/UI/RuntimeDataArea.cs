using System;
using Runtime.Data;
using Runtime.Extensions;
using Runtime.Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class RuntimeDataArea : MonoBehaviour
    {
        private Toggle self, enemy, numberShow;
        private GameData gameData;
        private void Awake()
        {
            DataManager.GameData ??= ReadWriteManager.Read("GameRunTimeData", new GameData());
            gameData = DataManager.GameData;

            self = transform.FindGetInChildren<Toggle>("Units/Self");
            self.onValueChanged.AddListener(invicible => { gameData.isInvicibleSelf = invicible; });

            enemy = transform.FindGetInChildren<Toggle>("Units/Enemy");
            enemy.onValueChanged.AddListener(invicible => gameData.isInvicibleEnemy = invicible);

            numberShow = transform.FindGetInChildren<Toggle>("Other/NumberShow");
            numberShow.onValueChanged.AddListener(show => gameData.isShowNumber = show);

            transform.FindGet<Button>("Save").onClick.AddListener(() =>
            {
                ReadWriteManager.Write("GameRunTimeData", JsonUtility.ToJson(gameData));
            });
        }

        private void OnEnable()
        {
            self.isOn = gameData.isInvicibleSelf;


            enemy.isOn = gameData.isInvicibleEnemy;


            numberShow.isOn = gameData.isShowNumber;
        }
    }
}