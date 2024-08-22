using System;
using Tao_Framework.Core.Event;
using TMPro;
using UnityEngine;

namespace Runtime.UI
{
    public class KillAmountBox : MonoBehaviour
    {
        private TextMeshProUGUI amount;
        private int currentAmount;
        private EntitySystem entitySystem;
        private void Awake()
        {
            amount = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Start()
        {
            entitySystem = EntitySystem.Instance;
            amount.SetText(currentAmount.ToString());
            EventMgr.Instance.RegisterEvent<int>(GameEvent.EntityDead,UpdateAmount);
        }

        private void UpdateAmount(int entityId)
        {
            
            currentAmount++;
            amount.SetText(currentAmount.ToString());
        }
    }
}