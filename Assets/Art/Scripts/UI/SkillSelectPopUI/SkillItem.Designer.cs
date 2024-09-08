/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Spine.Unity;

namespace QFramework.Example
{
	public partial class SkillItem
	{
		[SerializeField] public UnityEngine.UI.Button SkillSelectBtn;
		[SerializeField] public SkeletonGraphic SkillSpine;
		private string skillKey;
		
		public void Clear()
		{
			SkillSelectBtn = null;
		}

		public override string ComponentName
		{
			get { return "SkillItem";}
		}
	}
}
