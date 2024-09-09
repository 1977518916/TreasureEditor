using System;
using Runtime.Data;
using Runtime.Manager;
using Runtime.Utils;
using TMPro;
using UnityEngine;

namespace QFramework.Example
{
    public class EnemyEditorViewData : UIPanelData
    {
    }
    public partial class EnemyEditorView : UIPanel
    {

        private int currentIndex;
        private LevelData currentLevelData;
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as EnemyEditorViewData ?? new EnemyEditorViewData();
            currentLevelData = DataManager.GetLevelData();
            InitShow();
            RefreshUi();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }

        private void InitShow()
        {
            InitBtn();
            InitDropDown();
            InitField();
        }

        private void InitBtn()
        {
            AddTimesButton.onClick.AddListener(() =>
            {
                currentLevelData.timesDatas.Add(new TimesData());
                currentIndex = currentLevelData.timesDatas.Count - 1;
                RefreshTimesDropDown();
                RefreshUi();
            });

            ReduceTimesButton.onClick.AddListener(() =>
            {
                currentLevelData.timesDatas.RemoveAt(currentIndex);
                currentIndex = Mathf.Max(0, currentIndex - 1);
                RefreshTimesDropDown();
                RefreshUi();
            });

            DeleteButton.onClick.AddListener(() =>
            {
                DataManager.SetLevelData(new LevelData());
                DataManager.SaveLevelData();
                currentLevelData = DataManager.GetLevelData();
                currentIndex = 0;
                RefreshTimesDropDown();
                RefreshUi();
            });

            SaveButton.onClick.AddListener(() =>
            {
                DataManager.SetLevelData(currentLevelData);
                DataManager.SaveLevelData();
            });

            CloseButton.onClick.AddListener(() => UIKit.HidePanel<EnemyEditorView>());
        }

        private void InitDropDown()
        {
            RefreshTimesDropDown();
            CurrentTimesDrop.onValueChanged.AddListener(index =>
            {
                currentIndex = index;
                RefreshUi();
            });

            Array array = Enum.GetValues(typeof(EnemyTypeEnum));
            ResetDropDown(array.Length, EnemyTypeDrop, index => { EnemyTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi((EnemyTypeEnum)index))); });
            EnemyTypeDrop.SetValueWithoutNotify((int)currentLevelData.timesDatas[currentIndex].enemyType);
            EnemyTypeDrop.onValueChanged.AddListener(value =>
            {
                currentLevelData.timesDatas[currentIndex].enemyType = (EnemyTypeEnum)value;
                RefreshUi();
            });

            array = Enum.GetValues(typeof(EnemyType));
            ResetDropDown(array.Length, EnemyActionTypeDrop, index =>
                EnemyActionTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi((EnemyType)index))));
            EnemyActionTypeDrop.onValueChanged.AddListener(index => currentLevelData.timesDatas[currentIndex].enemyActionType = (EnemyType)index);
        }

        private void InitField()
        {
            TimesAmountField.onValueChanged.AddListener(value => currentLevelData.timesDatas[currentIndex].amount = int.Parse(value));

            EnemyHpField.onValueChanged.AddListener(value => currentLevelData.timesDatas[currentIndex].enemyData.hp = int.Parse(value));

            EnemyAtkField.onValueChanged.AddListener(value => currentLevelData.timesDatas[currentIndex].enemyData.atk = int.Parse(value));

            EnemySpeedField.onValueChanged.AddListener(value => currentLevelData.timesDatas[currentIndex].enemyData.speed = int.Parse(value));
        }

        private void RefreshUi()
        {
            TimesData timesData = currentLevelData.timesDatas[currentIndex];

            CurrentTimesDrop.SetValueWithoutNotify(currentIndex);
            CurrentTimesDrop.RefreshShownValue();

            EnemyTypeDrop.SetValueWithoutNotify((int)timesData.enemyType);
            EnemyTypeDrop.RefreshShownValue();

            EnemyActionTypeDrop.SetValueWithoutNotify((int)timesData.enemyActionType);
            EnemyActionTypeDrop.RefreshShownValue();

            TimesAmountField.SetTextWithoutNotify(timesData.amount.ToString());
            EnemyHpField.SetTextWithoutNotify(timesData.enemyData.hp.ToString());
            EnemyAtkField.SetTextWithoutNotify(timesData.enemyData.atk.ToString());
            EnemySpeedField.SetTextWithoutNotify(timesData.enemyData.speed + "");

            ShowEnemyModel(timesData);
        }

        private void ShowEnemyModel(TimesData timesData)
        {
            EnemyParent.DestroyChildren();
            AssetsLoadManager.LoadEnemy(timesData.enemyType, EnemyParent);
        }

        private void RefreshTimesDropDown()
        {
            ResetDropDown(currentLevelData.timesDatas.Count, CurrentTimesDrop,
                index => CurrentTimesDrop.options.Add(new TMP_Dropdown.OptionData($"第{index + 1}波")));
        }

        private void ResetDropDown(int amount, TMP_Dropdown dropdown, Action<int> action)
        {
            dropdown.ClearOptions();
            for(int i = 0; i < amount; i++)
            {
                action.Invoke(i);
            }
        }
    }
}