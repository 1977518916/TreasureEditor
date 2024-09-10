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
		/// <param name="data"></param>
		public void InitView(List<HeroData> data)
		{
			heroDataList.AddRange(data);
			HeroData_Item.gameObject.SetActive(false);
			if (DetectAndInitData()) return;
			for (var i = 0; i < heroDataList.Count; i++)
			{
				var viewItem = Instantiate(HeroData_Item, transform);
				viewItem.InitView(i, heroDataList[i]);
				viewItem.gameObject.SetActive(true);
				heroDataViewList.Add(viewItem);
			}
		}
		
		/// <summary>
		/// 检测并初始化数据
		/// </summary>
		/// <returns></returns>
		private bool DetectAndInitData()
		{
			if (heroDataList.Count != 0) return false;
			AddHeroData(new HeroData());
			return true;
		}

		/// <summary>
		/// 添加一个英雄数据
		/// </summary>
		/// <param name="heroData"></param>
		public void AddHeroData(HeroData heroData)
		{
			if (heroDataList.Count >= 5) return;
			var heroDataItem = Instantiate(HeroData_Item, transform);
			heroDataItem.InitView(0, heroData);
			heroDataItem.gameObject.SetActive(true);
			heroDataList.Add(heroData);
			heroDataViewList.Add(heroDataItem);
		}
		
		/// <summary>
		/// 保存当前编辑的英雄数据
		/// </summary>
		public void SaveAllHeroData()
		{
			DataManager.GetHeroDataList().Clear();
			DataManager.SetHeroListData(heroDataList);
			DataManager.SaveHeroData();
		}
		
		/// <summary>
		/// 重置视图
		/// </summary>
		public void ResetView()
		{
			ClearAllHeroDataView();
			DataManager.SetHeroListData(heroDataList);
			InitView(DataManager.GetHeroDataList());
		}

		/// <summary>
		/// 删除指定视图和数据
		/// </summary>
		public void RemoveAtViewAndData(int index)
		{
			heroDataList.RemoveAt(index);
			heroDataViewList.RemoveAt(index);
		}

		/// <summary>
		/// 清除所有英雄数据视图
		/// </summary>
		private void ClearAllHeroDataView()
		{
			foreach (var item in heroDataViewList)
			{
				Destroy(item.gameObject);
			}

			heroDataViewList.Clear();
			heroDataList.Clear();
		}
	}
}