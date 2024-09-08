using System;
using Runtime.Data;
using Runtime.Manager;
using Runtime.Utils;
using TMPro;

namespace QFramework.Example
{
    public class EnemyEditorViewData : UIPanelData
    {
        public LevelData LevelData => DataManager.GetLevelData();
    }
    public partial class EnemyEditorView : UIPanel
    {
        
        private int currentIndex = 0;
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as EnemyEditorViewData ?? new EnemyEditorViewData();
            InitShow();
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
                mPrivateData.LevelData.timesDatas.Add(new TimesData());
                currentIndex = mPrivateData.LevelData.timesDatas.Count - 1;
                RefreshUi();
            });

            DeleteButton.onClick.AddListener((() =>
            {
                mPrivateData.LevelData.timesDatas.RemoveAt(currentIndex);
                currentIndex = 0;
                RefreshUi();
            }));

            DeleteButton.onClick.AddListener((() =>
            {
                ReadWriteManager.Level.SaveLevelData(null);
                mPrivateData.LevelData.timesDatas.Clear();
                mPrivateData.LevelData.timesDatas.Add(new TimesData());
                RefreshUi();
            }));

            SaveButton.onClick.AddListener((() => { ReadWriteManager.Level.SaveLevelData(mPrivateData.LevelData); }));

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
            EnemyTypeDrop.SetValueWithoutNotify((int)mPrivateData.LevelData.timesDatas[currentIndex].enemyType);
            EnemyTypeDrop.onValueChanged.AddListener(value => mPrivateData.LevelData.timesDatas[currentIndex].enemyType = (EnemyTypeEnum)value);

            array = Enum.GetValues(typeof(EnemyType));
            ResetDropDown(array.Length, EnemyActionTypeDrop, index =>
                EnemyActionTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi((EnemyType)index))));
            EnemyActionTypeDrop.onValueChanged.AddListener(index => mPrivateData.LevelData.timesDatas[currentIndex].enemyActionType = (EnemyType)index);
        }

        private void InitField()
        {
            TimesAmountField.onValueChanged.AddListener(value => mPrivateData.LevelData.timesDatas[currentIndex].amount = int.Parse(value));

            EnemyHpField.onValueChanged.AddListener(value => mPrivateData.LevelData.timesDatas[currentIndex].enemyData.hp = int.Parse(value));

            EnemyAtkField.onValueChanged.AddListener(value => mPrivateData.LevelData.timesDatas[currentIndex].enemyData.atk = int.Parse(value));

            EnemySpeedField.onValueChanged.AddListener(value => mPrivateData.LevelData.timesDatas[currentIndex].enemyData.speed = int.Parse(value));
        }

        private void RefreshUi()
        {
            RefreshTimesDropDown();

            TimesData timesData = mPrivateData.LevelData.timesDatas[currentIndex];
            TimesAmountField.SetTextWithoutNotify(timesData.amount.ToString());
            EnemyHpField.SetTextWithoutNotify(timesData.enemyData.hp.ToString());
            EnemyAtkField.SetTextWithoutNotify(timesData.enemyData.atk.ToString());
            EnemySpeedField.SetTextWithoutNotify(timesData.enemyData.speed.ToString());

        }

        private void RefreshTimesDropDown()
        {
            ResetDropDown(mPrivateData.LevelData.timesDatas.Count, CurrentTimesDrop,
                index => CurrentTimesDrop.options.Add(new TMP_Dropdown.OptionData($"第{index}波")));
        }

        private void ResetDropDown(int amount, TMP_Dropdown dropdown, Action<int> action)
        {
            int index = 1;
            dropdown.ClearOptions();
            for(int i = 0; i < amount; i++)
            {
                action.Invoke(index++);
            }
        }
    }
}