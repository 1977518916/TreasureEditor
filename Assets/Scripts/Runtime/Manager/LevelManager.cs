using System.Collections.Generic;
using Runtime.Data;
using Tao_Framework.Core.Event;
using Tao_Framework.Core.Singleton;
using UnityEngine;

namespace Runtime.Manager
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private float currentTime, targetTime;
        private int index, amount;
        private Transform enemyParent;
        private List<TimesData> list;
        private bool isMaking;
        private void Awake()
        {
            list = DataManager.LevelData.timesDatas;
            targetTime = list[index].time;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            TryNextLayer();
            TryMakeEnemy();
        }

        private void TryNextLayer()
        {
            if(isMaking || index >= list.Count)
            {
                return;
            }
            if(currentTime >= targetTime)
            {
                isMaking = true;
                amount = list[index].amount;
            }
        }

        private void TryMakeEnemy()
        {
            if(!isMaking)
            {
                return;
            }
            if(!(currentTime >= targetTime)) return;
            MakeEnemy();
            if(--amount > 0)
            {
                targetTime += list[index].makeTime;
                return;
            }

            isMaking = false;
            if(++index < list.Count)
            {
                targetTime += list[index].time;
            }
        }

        private void MakeEnemy()
        {
            EventMgr.Instance.TriggerEvent(GameEvent.MakeEnemy, new EnemyBean()
            {
                EnemyType = list[index].enemyType,
                EnemyData = list[index].enemyData
            });
        }

        private void OnDestroy()
        {
            DestroyInstance();
        }

        public class EnemyBean
        {
            public EnemyTypeEnum EnemyType;
            public UnitData EnemyData;
        }
    }
}