﻿/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class HeroDataContainer
	{
		[SerializeField] public HeroData_Item HeroData_Item;

		/// <summary>
		/// 当前所有英雄数据
		/// </summary>
		private List<HeroData> currentAllHeroData = new List<HeroData>();
		
		/// <summary>
		/// 当前所有英雄数据视图容器
		/// </summary>
		private List<HeroData_Item> currentAllHeroDataViewList = new List<HeroData_Item>();
		
		public void Clear()
		{
			HeroData_Item = null;
		}

		public override string ComponentName
		{
			get { return "HeroDataContainer";}
		}
	}
}
