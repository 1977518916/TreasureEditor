/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class HeroData_Item
	{
		[SerializeField] public UnityEngine.UI.Text HeroNumber_Text;
		[SerializeField] public HeroSelect HeroSelect;
		[SerializeField] public HeroAttributeData HeroAttributeData;
		[SerializeField] public BulletTypeDataEdit BulletTypeData;
		[SerializeField] public BulletDataEdit BulletData;
		[SerializeField] public HeroModelScaleDataEdit HeroModelScaleData;
		[SerializeField] public HeroSkillDataEdit HeroSkillData;
		[SerializeField] public BulletModelDataEdit BulletModelData;
		[SerializeField] public UnityEngine.UI.Button Delete_Btn;
		
		/// <summary>
		/// 英雄数据
		/// </summary>
		private HeroData data;
		
		public void Clear()
		{
			HeroNumber_Text = null;
			HeroSelect = null;
			HeroAttributeData = null;
			BulletTypeData = null;
			BulletData = null;
			HeroModelScaleData = null;
			HeroSkillData = null;
			BulletModelData = null;
			Delete_Btn = null;
		}

		public override string ComponentName
		{
			get { return "HeroData_Item";}
		}
	}
}
