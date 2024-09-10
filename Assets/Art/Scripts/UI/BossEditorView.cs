using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Manager;

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
			var bossData = DataManager.GetLevelData().BossData;
			AddBossBtn.gameObject.SetActive(bossData == null);
			RemoveBossBtn.gameObject.SetActive(bossData != null);
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
		
		/// <summary>
		/// 初始化按钮
		/// </summary>
		private void InitBtn()
		{
			AddBossBtn.onClick.AddListener(() =>
			{
				
			});
			RemoveBossBtn.onClick.AddListener(() =>
			{

			});
		}
	}
}
