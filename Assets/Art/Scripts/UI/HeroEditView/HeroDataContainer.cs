/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Runtime.Manager;

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

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="heroDataList"></param>
		public void InitView(List<HeroData> heroDataList)
		{
			currentAllHeroData = heroDataList;
			HeroData_Item.gameObject.SetActive(false);
			if (DetectAndInitData()) return;
			for (var i = 0; i < heroDataList.Count; i++)
			{
				var heroDataItem = Instantiate(HeroData_Item, transform);
				heroDataItem.InitView(i, heroDataList[i]);
				heroDataItem.gameObject.SetActive(true);
			}
		}

		/// <summary>
		/// 检测并初始化数据
		/// </summary>
		/// <returns></returns>
		private bool DetectAndInitData()
		{
			if (currentAllHeroData.Count != 0) return false;
			AddHeroData(new HeroData());
			return true;
		}

		/// <summary>
		/// 添加一个英雄数据
		/// </summary>
		/// <param name="heroData"></param>
		public void AddHeroData(HeroData heroData)
		{
			currentAllHeroData.Add(heroData);
			var heroDataItem = Instantiate(HeroData_Item, transform);
			heroDataItem.InitView(currentAllHeroData.Count - 1, heroData);
			heroDataItem.gameObject.SetActive(true);
		}
		
		/// <summary>
		/// 保存当前编辑的英雄数据
		/// </summary>
		public void SaveAllHeroData()
		{
			DataManager.HeroDataList.Clear();
			DataManager.HeroDataList.AddRange(currentAllHeroData.ToArray());
		}
		
		/// <summary>
		/// 重置视图
		/// </summary>
		public void ResetView()
		{
			DataManager.HeroDataList.Clear();
			DataManager.HeroDataList.Add(new HeroData());
			ClearAllHeroDataView();
			InitView(DataManager.HeroDataList);
		}
		
		/// <summary>
		/// 清除所有英雄数据视图
		/// </summary>
		private void ClearAllHeroDataView()
		{
			foreach (var heroDataItem in currentAllHeroDataViewList)
			{
				Destroy(heroDataItem.gameObject);
			}

			currentAllHeroDataViewList.Clear();
		}
	}
}