using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:8be3c711-5214-4c10-bc0e-1304e7f64ae0
	public partial class HeroEditView
	{
		public const string Name = "HeroEditView";
		
		[SerializeField]
		public UnityEngine.UI.Button HeroAdd_Btn;
		[SerializeField]
		public UnityEngine.UI.Button SaveData_Btn;
		[SerializeField]
		public UnityEngine.UI.Button ResetData_Btn;
		[SerializeField]
		public UnityEngine.UI.Button CloseView_Btn;
		[SerializeField]
		public HeroDataContainer EditContainer;
		
		private HeroEditViewData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			HeroAdd_Btn = null;
			SaveData_Btn = null;
			ResetData_Btn = null;
			CloseView_Btn = null;
			EditContainer = null;
			
			mData = null;
		}
		
		public HeroEditViewData Data
		{
			get
			{
				return mData;
			}
		}
		
		HeroEditViewData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new HeroEditViewData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
