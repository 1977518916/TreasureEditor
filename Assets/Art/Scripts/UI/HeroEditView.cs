using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Runtime.Manager;

namespace QFramework.Example
{
	public class HeroEditViewData : UIPanelData
	{
	}
	public partial class HeroEditView : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HeroEditViewData ?? new HeroEditViewData();
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			EditContainer.InitView(DataManager.GetHeroDataList());
			HeroAdd_Btn.onClick.AddListener(() => EditContainer.AddHeroData(new HeroData()));
			SaveData_Btn.onClick.AddListener(() => EditContainer.SaveAllHeroData());
			ResetData_Btn.onClick.AddListener(() => EditContainer.ResetView());
			CloseView_Btn.onClick.AddListener(() =>
			{
				DataManager.SetHeroListData(EditContainer.GetCurrentAllHeroData());
				UIKit.HidePanel<HeroEditView>();
			});
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
