using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:ce64c688-b814-43e6-a4b7-725b70357135
	public partial class EnemyPanel
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
		public RectTransform EnemyParent;
		
		private EnemyPanelData mPrivateData = null;
		
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
			EnemyParent = null;
			
			mData = null;
		}
		
		public EnemyPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		EnemyPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new EnemyPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
