using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:0fff3b8d-d995-46e0-8c88-a09ceb2af8c7
	public partial class EnemyEditorView
	{
		public const string Name = "EnemyEditorView";
		
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
		[SerializeField]
		public UnityEngine.UI.Button CloseButton;
		
		private EnemyEditorViewData mPrivateData = null;
		
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
			CloseButton = null;
			
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
