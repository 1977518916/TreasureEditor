using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:fbf8a93c-c81b-4ad7-a892-8a2216f663f8
	public partial class SkillSelectPopUI
	{
		public const string Name = "SkillSelectPopUI";
		
		[SerializeField]
		public UnityEngine.UI.ScrollRect SkillScrollView;
		[SerializeField]
		public UnityEngine.UI.GridLayoutGroup Content;
		[SerializeField]
		public SkillItem SkillItem;
		
		private SkillSelectPopUIData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			SkillScrollView = null;
			Content = null;
			SkillItem = null;
			
			mData = null;
		}
		
		public SkillSelectPopUIData Data
		{
			get
			{
				return mData;
			}
		}
		
		SkillSelectPopUIData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new SkillSelectPopUIData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
