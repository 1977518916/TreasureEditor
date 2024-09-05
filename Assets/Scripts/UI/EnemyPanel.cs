using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public class EnemyPanelData : UIPanelData
	{
	}
	public partial class EnemyPanel : UIPanel
	{
		private EnemiesDataModel enemiesDataModel;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as EnemyPanelData ?? new EnemyPanelData();
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
