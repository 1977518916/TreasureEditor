using System.Collections.Generic;
using Runtime.Data;
using Runtime.Extensions;
using Runtime.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class RuntimeDataArea : MonoBehaviour
    {
        private Toggle self, enemy, numberShow;
        private List<Toggle> hideToggles = new List<Toggle>();
        private GameData gameData;
        private void Awake()
        {
            gameData = DataManager.GetRuntimeData();

            self = transform.FindGetInChildren<Toggle>("Units/Self");
            self.onValueChanged.AddListener(invicible => { gameData.isInvicibleSelf = invicible; });

            enemy = transform.FindGetInChildren<Toggle>("Units/Enemy");
            enemy.onValueChanged.AddListener(invicible => gameData.isInvicibleEnemy = invicible);

            numberShow = transform.FindGetInChildren<Toggle>("Other/NumberShow");
            numberShow.onValueChanged.AddListener(show => gameData.isShowNumber = show);

            for(int i = 0; i < 5; i++)
            {
                int index = i;
                Toggle toggle = transform.FindGetInChildren<Toggle>($"Other/Hero{i + 1}Hide");
                hideToggles.Add(toggle);
                toggle.onValueChanged.AddListener(hide =>
                {
                    List<HeroEntity> entities = EntitySystem.Instance.GetAllHeroEntity();
                    if(index > entities.Count - 1)
                    {
                        return;
                    }
                    entities[index].gameObject.SetActive(!hide);
                });
            }

            transform.FindGet<Button>("Save").onClick.AddListener(() => { ReadWriteManager.Write("GameRunTimeData", JsonUtility.ToJson(gameData)); });
        }

        private void OnEnable()
        {
            Refresh();

        }

        private void Refresh()
        {
            self.isOn = gameData.isInvicibleSelf;


            enemy.isOn = gameData.isInvicibleEnemy;
            
            numberShow.isOn = gameData.isShowNumber;
            
            List<HeroEntity> entities = EntitySystem.Instance.GetAllHeroEntity();
            for(int i = 0; i < hideToggles.Count; i++)
            {
                hideToggles[i].isOn = i <= entities.Count - 1 && !entities[i].gameObject.activeSelf;
            }
        }
    }
}