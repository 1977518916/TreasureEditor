using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:790b8bb1-8ee7-47ca-bfbf-a66c8223cfaf
	public partial class HeroSelectPopUI
	{
		public const string Name = "HeroSelectPopUI";
		
		[SerializeField]
		public UnityEngine.UI.ScrollRect HeroSelectView;
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public HeroSelectItem HeroSelectItem;
		
		private HeroSelectPopUIData mPrivateData = null;
		
		/// <summary>
		/// 英雄选择元素的容器
		/// </summary>
		private List<HeroSelectItem> heroSelectItemList = new List<HeroSelectItem>();
		
		protected override void ClearUIComponents()
		{
			HeroSelectView = null;
			Content = null;
			HeroSelectItem = null;
			
			mData = null;
		}
		
		public HeroSelectPopUIData Data
		{
			get
			{
				return mData;
			}
		}
		
		HeroSelectPopUIData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new HeroSelectPopUIData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
