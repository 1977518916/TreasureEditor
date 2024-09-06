using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:af281b6e-6aef-43ea-9e92-19c95f3c35e7
	public partial class MapEditorView
	{
		public const string Name = "MapEditorView";
		
		[SerializeField]
		public UnityEngine.UI.ScrollRect ScrollView;
		[SerializeField]
		public SelectNode SelectNode;
		[SerializeField]
		public UnityEngine.UI.Button CloseButton;
		[SerializeField]
		public UnityEngine.UI.Button SaveButton;
		
		private MapEditorViewData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ScrollView = null;
			SelectNode = null;
			CloseButton = null;
			SaveButton = null;
			
			mData = null;
		}
		
		public MapEditorViewData Data
		{
			get
			{
				return mData;
			}
		}
		
		MapEditorViewData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new MapEditorViewData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
