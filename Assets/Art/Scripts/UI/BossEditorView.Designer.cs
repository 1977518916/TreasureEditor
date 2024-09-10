using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:3ab59eb6-ad0f-434b-8150-fde5b89e7c7b
	public partial class BossEditorView
	{
		public const string Name = "BossEditorView";
		
		[SerializeField]
		public UnityEngine.UI.Button AddBossBtn;
		[SerializeField]
		public UnityEngine.UI.Button RemoveBossBtn;
		[SerializeField]
		public BossDataPanel BossDataPanel;
		
		private BossEditorViewData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			AddBossBtn = null;
			RemoveBossBtn = null;
			BossDataPanel = null;
			
			mData = null;
		}
		
		public BossEditorViewData Data
		{
			get
			{
				return mData;
			}
		}
		
		BossEditorViewData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new BossEditorViewData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
