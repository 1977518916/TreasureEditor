using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class BossEditorViewData : UIPanelData
	{
	}
	public partial class BossEditorView : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as BossEditorViewData ?? new BossEditorViewData();
			// please add init code here
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
