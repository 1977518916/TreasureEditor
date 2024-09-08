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
	public partial class HeroData_Item : UIElement
	{
		private void Awake()
		{

		}

		protected override void OnBeforeDestroy()
		{
		}

		/// <summary>
		/// 初始化视图数据
		/// </summary>
		public void InitView(int index, HeroData heroData)
		{
			data = heroData;
			gameObject.SetActive(true);
			InitDeleteBtn(index);
			InitNumber(index);
			InitHeroSelect(heroData);
			InitHeroAttribute(heroData);
			InitHeroBullet(heroData);
			InitHeroBulletData(heroData);
			InitHeroModelScale(heroData);
			InitHeroSkillDataEdit(heroData);
			InitHeroBulletModelData(heroData);
		}

		/// <summary>
		/// 初始化编号
		/// </summary>
		/// <param name="index"></param>
		private void InitNumber(int index)
		{
			HeroNumber_Text.text = $"{index + 1}";
		}

		/// <summary>
		/// 初始化英雄选择
		/// </summary>
		/// <param name="heroData"></param>
		private void InitHeroSelect(HeroData heroData)
		{
			HeroSelect.InitView(heroData);
		}

		/// <summary>
		/// 初始化英雄属性
		/// </summary>
		/// <param name="heroData"></param>
		private void InitHeroAttribute(HeroData heroData)
		{
			HeroAttributeData.InitView(heroData);
		}
		
		/// <summary>
		/// 初始化英雄子弹
		/// </summary>
		/// <param name="heroData"></param>
		private void InitHeroBullet(HeroData heroData)
		{
			BulletTypeData.InitView(heroData);
		}

		/// <summary>
		/// 初始化英雄子弹数据
		/// </summary>
		/// <param name="heroData"></param>
		private void InitHeroBulletData(HeroData heroData)
		{
			BulletData.InitView(heroData);
		}
		
		/// <summary>
		/// 初始化英雄模型大小
		/// </summary>
		/// <param name="heroData"></param>
		private void InitHeroModelScale(HeroData heroData)
		{
			HeroModelScaleData.InitView(heroData);
		}
		
		/// <summary>
		/// 初始化英雄技能数据编辑界面
		/// </summary>
		/// <param name="heroData"></param>
		private void InitHeroSkillDataEdit(HeroData heroData)
		{
			HeroSkillData.InitView(heroData);
		}
		
		/// <summary>
		/// 初始化英雄子弹模型数据
		/// </summary>
		/// <param name="heroData"></param>
		private void InitHeroBulletModelData(HeroData heroData)
		{
			BulletModelData.InitView(heroData);
		}

		/// <summary>
		/// 初始化删除按钮
		/// </summary>
		/// <param name="index"></param>
		private void InitDeleteBtn(int index)
		{
			// 首个英雄无法被删除
			Delete_Btn.gameObject.SetActive(index != 0);
			// 删除按钮绑定的事件
			Delete_Btn.onClick.AddListener(() =>
			{
				DataManager.HeroDataList.RemoveAt(DataManager.HeroDataList.Count - 1);
				Destroy(gameObject);
			});
		}
	}
}