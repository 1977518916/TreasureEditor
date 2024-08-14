using System;
using Runtime.Data;
using Runtime.Extensions;
using Runtime.Manager;
using Runtime.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class LevelArea : MonoBehaviour
    {
        private TMP_Dropdown dropdown, typeDropDown;
        private TMP_InputField amount;
        private LevelData levelData;
        private TimesData currentData => levelData.timesDatas[dropdown.value];
        private void Awake()
        {
            dropdown = transform.FindGet<TMP_Dropdown>("LevelDrop");
            typeDropDown = transform.FindGet<TMP_Dropdown>("TimesInfo/Type");
            amount = transform.FindGet<TMP_InputField>("TimesInfo/Amount");
            transform.FindGet<Button>("DeleteButton").onClick.AddListener(DeleteTimes);
            transform.FindGet<Button>("AddButton").onClick.AddListener(AddTimes);
            transform.FindGet<Button>("Delete").onClick.AddListener(Delete);
            transform.FindGet<Button>("Save").onClick.AddListener(Save);
            Init();
        }

        private void Init()
        {
            levelData = ReadWriteManager.Level.GetLevelData();
            DataManager.levelData = levelData;
            typeDropDown.options.Clear();

            ShowLevelData();
            dropdown.onValueChanged.AddListener(ShowTimeData);

            foreach (EnemyTypeEnum value in Enum.GetValues(typeof(EnemyTypeEnum)))
            {
                typeDropDown.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
            }
            typeDropDown.onValueChanged.AddListener(value => currentData.enemyType = (EnemyTypeEnum)value);
            ShowTimeData(dropdown.value);

            amount.onValueChanged.AddListener(content => currentData.amount = int.Parse(content));
        }

        private void ShowTimeData(int index)
        {
            amount.SetTextWithoutNotify(currentData.amount.ToString());
            typeDropDown.value = (int)currentData.enemyType;
        }

        private void ShowLevelData()
        {
            dropdown.options.Clear();
            int i = 1;
            foreach (TimesData unused in levelData.timesDatas)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData($"第{i++}波"));
            }
        }

        private void DeleteTimes()
        {
            levelData.timesDatas.RemoveAt(dropdown.value);
            ShowLevelData();
        }

        private void AddTimes()
        {
            levelData.timesDatas.Add(new TimesData());
            ShowLevelData();
        }

        private void Save()
        {
            ReadWriteManager.Level.SaveLevelData(levelData);
        }

        private void Delete()
        {
            ReadWriteManager.Level.SaveLevelData(null);
            levelData.timesDatas.Clear();
            levelData.timesDatas.Add(new TimesData());
            ShowLevelData();
        }
    }
}