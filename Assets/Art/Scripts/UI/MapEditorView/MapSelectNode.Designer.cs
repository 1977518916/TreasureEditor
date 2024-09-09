/****************************************************************************
 * 2024.9 DESKTOP-SOMQ4IR
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class MapSelectNode
	{
		[SerializeField] public UnityEngine.UI.Image bg;
		[SerializeField] public UnityEngine.UI.Image tick;

		public void Clear()
		{
			bg = null;
			tick = null;
		}

		public override string ComponentName
		{
			get { return "SelectNode";}
		}
	}
}
