using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class SkillSelectPopUIData : UIPanelData
	{
	}
	public partial class SkillSelectPopUI : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as SkillSelectPopUIData ?? new SkillSelectPopUIData();
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
