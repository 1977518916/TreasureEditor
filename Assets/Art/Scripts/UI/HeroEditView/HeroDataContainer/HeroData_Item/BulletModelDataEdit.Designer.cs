/****************************************************************************
 * 2024.9 HEALER
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class BulletModelDataEdit
	{
		[SerializeField] public UnityEngine.UI.Button BulletModelSelect_Btn;
		[SerializeField] public Spine.Unity.SkeletonGraphic BulletSpineView;
		[SerializeField] public UnityEngine.UI.Image BulletSpiteView;

		public void Clear()
		{
			BulletModelSelect_Btn = null;
			BulletSpineView = null;
			BulletSpiteView = null;
		}

		public override string ComponentName
		{
			get { return "BulletModelDataEdit";}
		}
	}
}
