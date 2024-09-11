using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:58c787eb-f657-45cb-bb40-522361f1e9e1
	public partial class EnemyEditorView
	{
		public const string Name = "EnemyEditorView";
		
		[SerializeField]
		public TMPro.TMP_Dropdown CurrentMakerDrop;
		[SerializeField]
		public UnityEngine.UI.Button AddMakerButton;
		[SerializeField]
		public UnityEngine.UI.Button ReduceMakerButton;
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
		[SerializeField]
		public UnityEngine.UI.Button CloseButton;
		[SerializeField]
		public TMPro.TMP_InputField StartTimeField;
		[SerializeField]
		public TMPro.TMP_InputField IntervalTimeField;
		
		private EnemyEditorViewData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			CurrentMakerDrop = null;
			AddMakerButton = null;
			ReduceMakerButton = null;
			SaveButton = null;
			DeleteButton = null;
			TimesAmountField = null;
			EnemyHpField = null;
			EnemyAtkField = null;
			EnemySpeedField = null;
			EnemyTypeDrop = null;
			EnemyActionTypeDrop = null;
			EnemyParent = null;
			CloseButton = null;
			StartTimeField = null;
			IntervalTimeField = null;
			
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
	}
}
