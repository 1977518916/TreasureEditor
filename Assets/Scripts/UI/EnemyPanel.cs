using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public class EnemyPanelData : UIPanelData
	{
	}
	public partial class EnemyPanel : UIPanel,IController
	{
		private EnemiesDataModel enemiesDataModel;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as EnemyPanelData ?? new EnemyPanelData();
			// please add init code here
			enemiesDataModel = this.GetModel<EnemiesDataModel>();
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
		public IArchitecture GetArchitecture()
		{
			return ProtectArchitecture.Interface;
		}
	}
}
