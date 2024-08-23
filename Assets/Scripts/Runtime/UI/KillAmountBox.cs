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
            EventMgr.Instance.RegisterEvent<EntityType>(GetHashCode(),GameEvent.EntityDead,UpdateAmount);
        }

        private void OnDisable()
        {
            EventMgr.Instance.RemoveEvent(GetHashCode(), GameEvent.EntityDead);
        }
        
        private void UpdateAmount(EntityType entityType)
        {
            if (entityType != EntityType.EnemyEntity) return;
            currentAmount++;
            amount.SetText(currentAmount.ToString());
        }
    }
}