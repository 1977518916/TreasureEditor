/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;

namespace QFramework.Example
{
	public partial class HeroSelectItem
	{
		[SerializeField] public UnityEngine.UI.Button SelectBtn;
		[SerializeField] public Spine.Unity.SkeletonGraphic HeroView;

		private EntityModelType entityModelType;
		
		public void Clear()
		{
			SelectBtn = null;
			HeroView = null;
		}

		public override string ComponentName
		{
			get { return "HeroSelectItem";}
		}
	}
}
