using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Runtime.Manager;

namespace QFramework.Example
{
	public class EnemyEditorViewData : UIPanelData
	{
		public LevelData LevelData => DataManager.LevelData;
	}
	public partial class EnemyEditorView : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as EnemyEditorViewData ?? new EnemyEditorViewData();
			// please add init code here
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
	}
}
