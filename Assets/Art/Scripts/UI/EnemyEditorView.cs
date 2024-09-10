using System;
using Factories;
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

            Array array = Enum.GetValues(typeof(EntityModelType));
            ResetDropDown<EntityModelType>(array, EnemyTypeDrop, o =>
            {
                if((int)o >= 500)
                {
                    EnemyTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(o)));
                }
            });
            EnemyTypeDrop.SetValueWithoutNotify((int)currentLevelData.timesDatas[currentIndex].enemyType - 500);
            EnemyTypeDrop.onValueChanged.AddListener(value =>
            {
                currentLevelData.timesDatas[currentIndex].enemyType = (EntityModelType)(value + 500);
                RefreshUi();
            });

            array = Enum.GetValues(typeof(EnemyType));
            ResetDropDown<EnemyType>(array, EnemyActionTypeDrop, index =>
                EnemyActionTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(index))));
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

            EnemyTypeDrop.SetValueWithoutNotify((int)timesData.enemyType - 500);
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
            EnemyGameObjectFactory.Instance.Create(timesData.enemyType, EnemyParent);
        }

        private void RefreshTimesDropDown()
        {
            int index = 0;
            ResetDropDown<TimesData>(currentLevelData.timesDatas.ToArray(), CurrentTimesDrop,
                _ => { CurrentTimesDrop.options.Add(new TMP_Dropdown.OptionData($"第{++index}波")); });
        }

        private void ResetDropDown<T>(Array array, TMP_Dropdown dropdown, Action<T> action)
        {
            dropdown.ClearOptions();
            foreach (T o in array)
            {
                action.Invoke(o);
            }
        }
    }
}