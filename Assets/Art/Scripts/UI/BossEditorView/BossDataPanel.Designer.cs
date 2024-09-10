/****************************************************************************
 * 2024.9 DESKTOP-426MFOR
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class BossDataPanel
	{
		[SerializeField] public TMPro.TMP_Dropdown BossModelDrop;
		[SerializeField] public BossDataView BossDataView;
		[SerializeField] public Spine.Unity.SkeletonGraphic BossModel;
		[SerializeField] public RectTransform BossBulletModel;

		public void Clear()
		{
			BossModelDrop = null;
			BossDataView = null;
			BossModel = null;
			BossBulletModel = null;
		}

		public override string ComponentName
		{
			get { return "BossDataPanel";}
		}
	}
}
