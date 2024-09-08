/****************************************************************************
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
		/// 英雄数据列表
		/// </summary>
		private List<HeroData> heroDataList = new List<HeroData>();

		/// <summary>
		/// 英雄数据视图列表
		/// </summary>
		private List<HeroData_Item> heroDataViewList = new List<HeroData_Item>();
		
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
