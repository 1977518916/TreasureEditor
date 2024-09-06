using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:854287ec-5a76-4b9e-aca0-9e1fc6aee2de
	public partial class MapEditorView
	{
		public const string Name = "MapEditorView";
		
		[SerializeField]
		public UnityEngine.UI.ScrollRect ScrollView;
		[SerializeField]
		public SelectNode SelectNode;
		[SerializeField]
		public UnityEngine.UI.Button CloseButton;
		
		private MapEditorViewData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ScrollView = null;
			SelectNode = null;
			CloseButton = null;
			
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
