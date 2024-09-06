using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Runtime.Manager;
using Runtime.Utils;
using TMPro;

namespace QFramework.Example
{
	// Generate Id:180d5a17-e711-4f07-a5e9-3b2c1d920d7e
	public partial class EnemyEditorView
	{
		public const string Name = "EnemyPanel";
		
		[SerializeField]
		public TMPro.TMP_Dropdown CurrentTimesDrop;
		[SerializeField]
		public UnityEngine.UI.Button AddTimesButton;
		[SerializeField]
		public UnityEngine.UI.Button ReduceTimesButton;
		[SerializeField]
		public UnityEngine.UI.Button SaveButton;
		[SerializeField]
		public UnityEngine.UI.Button DeleteButton;
		[SerializeField]
		public TMPro.TMP_InputField TimesAmountField;
		[SerializeField]
		public TMPro.TMP_InputField EnemyHpField;
		[SerializeField]
		public TMPro.TMP_InputField EnemyAtkField;
		[SerializeField]
		public TMPro.TMP_InputField EnemySpeedField;
		[SerializeField]
		public TMPro.TMP_Dropdown EnemyTypeDrop;
		[SerializeField]
		public TMPro.TMP_Dropdown EnemyActionTypeDrop;
		[SerializeField]
		public RectTransform EnemyParent;
		
		private EnemyEditorViewData mPrivateData = null;
		private int currentIndex = 0;
		protected override void ClearUIComponents()
		{
			CurrentTimesDrop = null;
			AddTimesButton = null;
			ReduceTimesButton = null;
			SaveButton = null;
			DeleteButton = null;
			TimesAmountField = null;
			EnemyHpField = null;
			EnemyAtkField = null;
			EnemySpeedField = null;
			EnemyTypeDrop = null;
			EnemyActionTypeDrop = null;
			EnemyParent = null;
			
			mData = null;
		}
		
		public EnemyEditorViewData Data
		{
			get
			{
				return mData;
			}
		}
		
		EnemyEditorViewData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new EnemyEditorViewData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
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
			
			SaveButton.onClick.AddListener((() =>
			{
				ReadWriteManager.Level.SaveLevelData(mPrivateData.LevelData);
			}));
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
			ResetDropDown(array.Length,EnemyTypeDrop, index =>
			{
				EnemyTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi((EnemyTypeEnum)index)));
			});
			EnemyTypeDrop.SetValueWithoutNotify((int)mPrivateData.LevelData.timesDatas[currentIndex].enemyType);
			EnemyTypeDrop.onValueChanged.AddListener(value=> mPrivateData.LevelData.timesDatas[currentIndex].enemyType = (EnemyTypeEnum)value);

			array = Enum.GetValues(typeof(EnemyType));
			ResetDropDown(array.Length, EnemyActionTypeDrop, index => 
				EnemyActionTypeDrop.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi((EnemyType)index))));
			EnemyActionTypeDrop.onValueChanged.AddListener(index=>mPrivateData.LevelData.timesDatas[currentIndex].enemyActionType = (EnemyType)index);
		}

		private void InitField()
		{
			TimesAmountField.onValueChanged.AddListener(value=> mPrivateData.LevelData.timesDatas[currentIndex].amount = int.Parse(value));
			
			EnemyHpField.onValueChanged.AddListener(value=>mPrivateData.LevelData.timesDatas[currentIndex].enemyData.hp = int.Parse(value));
			
			EnemyAtkField.onValueChanged.AddListener(value=>mPrivateData.LevelData.timesDatas[currentIndex].enemyData.atk = int.Parse(value));
			
			EnemySpeedField.onValueChanged.AddListener(value=>mPrivateData.LevelData.timesDatas[currentIndex].enemyData.speed = int.Parse(value));
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
			ResetDropDown(mPrivateData.LevelData.timesDatas.Count,CurrentTimesDrop,
				index=>CurrentTimesDrop.options.Add(new TMP_Dropdown.OptionData($"第{index}波")));
		}

		private void ResetDropDown(int amount,TMP_Dropdown dropdown,Action<int> action)
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
