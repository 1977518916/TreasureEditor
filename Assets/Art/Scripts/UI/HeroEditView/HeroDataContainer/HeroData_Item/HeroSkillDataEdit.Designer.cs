/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Runtime.Data;
using Spine.Unity;

namespace QFramework.Example
{
	public partial class HeroSkillDataEdit
	{
		[SerializeField] public UnityEngine.UI.Button SkillOne_Select_Btn;
		[SerializeField] public Spine.Unity.SkeletonGraphic SkillOneView;
		[SerializeField] public UnityEngine.UI.Button SkillTwo_Select_Btn;
		[SerializeField] public Spine.Unity.SkeletonGraphic SkillTwoView;

		private HeroData data;
		private Dictionary<string, SkeletonDataAsset> allSkill;
		
		public void Clear()
		{
			SkillOne_Select_Btn = null;
			SkillOneView = null;
			SkillTwo_Select_Btn = null;
			SkillTwoView = null;
		}

		public override string ComponentName
		{
			get { return "HeroSkillDataEdit";}
		}
	}
}
