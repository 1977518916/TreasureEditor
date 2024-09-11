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
                currentLevelData.EnemyMakerDatas.Add(new EnemyMakerData());
                currentIndex = currentLevelData.EnemyMakerDatas.Count - 1;
                RefreshTimesDropDown();
                RefreshUi();
            });

            ReduceTimesButton.onClick.AddListener(() =>
            {
                currentLevelData.EnemyMakerDatas.RemoveAt(currentIndex);
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
            EnemyTypeDrop.SetValueWithoutNotify((int)currentLevelData.EnemyMakerDatas[currentIndex].enemyType - 500);
            EnemyTypeDrop.onValueChanged.AddListener(value =>
            {
                currentLevelData.EnemyMakerDatas[currentIndex].enemyType = (EntityModelType)(value + 500);
                RefreshUi();
            });

            array = Enum.GetValues(typeof(EnemyType));
            ResetDropDown<EnemyType>(array, EnemyActionTypeDrop, index =>
                EnemyActionTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(index))));
            EnemyActionTypeDrop.onValueChanged.AddListener(index => currentLevelData.EnemyMakerDatas[currentIndex].enemyActionType = (EnemyType)index);
        }

        private void InitField()
        {
            TimesAmountField.onEndEdit.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].amount = ParseInt(value, 3));

            EnemyHpField.onEndEdit.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].enemyData.hp = ParseInt(value, 10));

            EnemyAtkField.onEndEdit.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].enemyData.atk = ParseInt(value, 1));

            EnemySpeedField.onEndEdit.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].enemyData.speed = ParseInt(value, 30));
        }

        private int ParseInt(string value, int defaultValue)
        {
            if(value.IsNullOrEmpty())
            {
                return defaultValue;
            }
            return int.Parse(value);
        }

        private void RefreshUi()
        {
            EnemyMakerData timesData = currentLevelData.EnemyMakerDatas[currentIndex];

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

        private void ShowEnemyModel(EnemyMakerData timesData)
        {
            EnemyParent.DestroyChildren();
            EnemyGameObjectFactory.Instance.Create(timesData.enemyType, EnemyParent);
        }

        private void RefreshTimesDropDown()
        {
            int index = 0;
            ResetDropDown<EnemyMakerData>(currentLevelData.EnemyMakerDatas.ToArray(), CurrentTimesDrop,
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