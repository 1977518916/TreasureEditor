/****************************************************************************
 * 2024.9 DESKTOP-SOMQ4IR
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Unity.VisualScripting;

namespace QFramework.Example
{
	public partial class SelectNode : UIComponent
	{
		private Action clickAction;
		public Action refreshAction;
		private void Awake()
		{
			transform.GetComponent<Button>().onClick.AddListener(Click);

		}

		private void Click()
		{
			clickAction?.Invoke();
			refreshAction?.Invoke();
		}

		public void SetTick(bool isTick)
		{
			tick.gameObject.SetActive(isTick);
		}

		public void SetNode(Sprite sprite,Action action)
		{
			bg.sprite = sprite;
			clickAction = action;
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}