/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class HeroSelect
	{
		[SerializeField] public UnityEngine.UI.Button HeroSelect_Btn;
		[SerializeField] public Spine.Unity.SkeletonGraphic HeroView;

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
