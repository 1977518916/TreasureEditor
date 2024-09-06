using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:0245591a-a04e-46ee-9fd5-6ed9b1585d0d
	public partial class EditorUI
	{
		public const string Name = "EditorUI";
		
		[SerializeField]
		public UnityEngine.UI.Image MapView;
		[SerializeField]
		public UnityEngine.UI.Button HeroEdit_Btn;
		[SerializeField]
		public UnityEngine.UI.Button EnemyEdit_Btn;
		[SerializeField]
		public UnityEngine.UI.Button BossEdit_Btn;
		[SerializeField]
		public UnityEngine.UI.Button MapEdit_Btn;
		
		private EditorUIData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			MapView = null;
			HeroEdit_Btn = null;
			EnemyEdit_Btn = null;
			BossEdit_Btn = null;
			MapEdit_Btn = null;
			
			mData = null;
		}
		
		public EditorUIData Data
		{
			get
			{
				return mData;
			}
		}
		
		EditorUIData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new EditorUIData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
