/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class HeroSelect
	{
		[SerializeField] public UnityEngine.UI.Button HeroSelect_Btn;
		[SerializeField] public Spine.Unity.SkeletonGraphic HeroView;

		private HeroData_Item heroDataItem;
		private HeroData data;
		
		public void Clear()
		{
			HeroSelect_Btn = null;
			HeroView = null;
		}

		public override string ComponentName
		{
			get { return "HeroSelect";}
		}
	}
}
