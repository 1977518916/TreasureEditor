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
            AddMakerButton.onClick.AddListener(() =>
            {
                currentLevelData.EnemyMakerDatas.Add(new EnemyMakerData());
                currentIndex = currentLevelData.EnemyMakerDatas.Count - 1;
                RefreshTimesDropDown();
                RefreshUi();
            });

            ReduceMakerButton.onClick.AddListener(() =>
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
            CurrentMakerDrop.onValueChanged.AddListener(index =>
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
            TimesAmountField.onValueChanged.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].amount = int.Parse(value));

            EnemyHpField.onValueChanged.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].enemyData.hp = int.Parse(value));

            EnemyAtkField.onValueChanged.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].enemyData.atk = int.Parse(value));

            EnemySpeedField.onValueChanged.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].enemyData.speed = int.Parse(value));

            StartTimeField.onValueChanged.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].time = float.Parse(value));

            IntervalTimeField.onValueChanged.AddListener(value => currentLevelData.EnemyMakerDatas[currentIndex].makeTime = float.Parse(value));
        }

        private void RefreshUi()
        {
            EnemyMakerData timesData = currentLevelData.EnemyMakerDatas[currentIndex];

            CurrentMakerDrop.SetValueWithoutNotify(currentIndex);
            CurrentMakerDrop.RefreshShownValue();

            EnemyTypeDrop.SetValueWithoutNotify((int)timesData.enemyType - 500);
            EnemyTypeDrop.RefreshShownValue();

            EnemyActionTypeDrop.SetValueWithoutNotify((int)timesData.enemyActionType);
            EnemyActionTypeDrop.RefreshShownValue();

            TimesAmountField.SetTextWithoutNotify(timesData.amount.ToString());
            EnemyHpField.SetTextWithoutNotify(timesData.enemyData.hp.ToString());
            EnemyAtkField.SetTextWithoutNotify(timesData.enemyData.atk.ToString());
            EnemySpeedField.SetTextWithoutNotify(timesData.enemyData.speed + "");
            StartTimeField.SetTextWithoutNotify(timesData.time + "");
            IntervalTimeField.SetTextWithoutNotify(timesData.makeTime + "");
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
            ResetDropDown<EnemyMakerData>(currentLevelData.EnemyMakerDatas.ToArray(), CurrentMakerDrop,
                _ => { CurrentMakerDrop.options.Add(new TMP_Dropdown.OptionData($"生成器{++index}")); });
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