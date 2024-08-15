using System;
using System.Collections.Generic;
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
        private TMP_Dropdown dropdown, typeDropDown, mapDropDown;
        private TMP_InputField amount;
        private LevelData levelData;
        private Transform enemyParent;
        private Image bg;
        private TimesData CurrentData => levelData.timesDatas[dropdown.value];
        private void Awake()
        {
            dropdown = transform.FindGet<TMP_Dropdown>("LevelDrop");
            typeDropDown = transform.FindGet<TMP_Dropdown>("TimesInfo/Type");
            mapDropDown = transform.FindGet<TMP_Dropdown>("MapDrop");
            amount = transform.FindGet<TMP_InputField>("TimesInfo/Amount");
            enemyParent = transform.Find("TimesInfo/EnemyParent");
            bg = transform.FindGet<Image>("bg");
            transform.FindGet<Button>("DeleteButton").onClick.AddListener(DeleteTimes);
            transform.FindGet<Button>("AddButton").onClick.AddListener(AddTimes);
            transform.FindGet<Button>("Delete").onClick.AddListener(Delete);
            transform.FindGet<Button>("Save").onClick.AddListener(Save);
            Init();
        }

        private void Init()
        {
            levelData = ReadWriteManager.Level.GetLevelData();
            DataManager.LevelData = levelData;

            typeDropDown.ClearOptions();
            mapDropDown.ClearOptions();

            ShowLevelData();
            ShowMap();
            dropdown.onValueChanged.AddListener(ShowTimeData);

            mapDropDown.onValueChanged.AddListener(index => levelData.mapType = (MapTypeEnum)index);
            mapDropDown.onValueChanged.AddListener(ShowMap);
            foreach (MapTypeEnum value in Enum.GetValues(typeof(MapTypeEnum)))
            {
                mapDropDown.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
            }

            foreach (EnemyTypeEnum value in Enum.GetValues(typeof(EnemyTypeEnum)))
            {
                typeDropDown.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
            }
            typeDropDown.onValueChanged.AddListener(value => CurrentData.enemyType = (EnemyTypeEnum)value);
            typeDropDown.onValueChanged.AddListener(_ => ShowEnemy());
            ShowTimeData(dropdown.value);

            amount.onValueChanged.AddListener(content => CurrentData.amount = int.Parse(content));
        }

        private void ShowMap(int _ = 0)
        {
            bg.sprite = AssetsLoadManager.LoadBg(levelData.mapType);
        }

        private void ShowTimeData(int index)
        {
            amount.SetTextWithoutNotify(CurrentData.amount.ToString());
            typeDropDown.value = (int)CurrentData.enemyType;
            ShowEnemy();
        }

        private void ShowEnemy()
        {
            enemyParent.ClearChild();
            AssetsLoadManager.LoadEnemy(CurrentData.enemyType, enemyParent);
        }

        private void ShowLevelData()
        {
            dropdown.ClearOptions();
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