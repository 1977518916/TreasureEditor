/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class HeroDataContainer : UIElement
	{
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}
		
		public void InitView(List<HeroData> heroDataList)
		{
			currentAllHeroData = heroDataList;
			for (var i = 0; i < heroDataList.Count; i++)
			{
				var heroDataItem = Instantiate(HeroData_Item, transform);
				heroDataItem.InitView(i, heroDataList[i]);
			}
		}
	}
}