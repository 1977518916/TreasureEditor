using System.Collections.Generic;
using Runtime.Data;
using Tao_Framework.Core.Event;
using Tao_Framework.Core.Singleton;
using UnityEngine;

namespace Runtime.Manager
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private List<EnemyMaker> list = new List<EnemyMaker>();
        private void Awake()
        {
            foreach (EnemyMakerData data in DataManager.GetLevelData().EnemyMakerDatas)
            {
                list.Add(new EnemyMaker(data));
            }
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            foreach (EnemyMaker enemyMaker in list)
            {
                enemyMaker.Update(deltaTime);
            }
        }

        private void OnDestroy()
        {
            DestroyInstance();
        }

        /// <summary>
        /// 停止出怪
        /// </summary>
        public void StopMakeEnemy()
        {
            foreach (EnemyMaker enemyMaker in list)
            {
                enemyMaker.Stop();
            }
        }

        public class EnemyBean
        {
            public EntityModelType EnemyType;
            public EnemyData EnemyData;
        }

        private class EnemyMaker
        {
            /// <summary>
            /// 开始时间
            /// </summary>
            float startTime;
            /// <summary>
            /// 当前生成总数
            /// </summary>
            private int amount;

            private float targetTime, currentTime;
            private readonly EnemyMakerData data;
            private bool canMaking = true;

            public EnemyMaker(EnemyMakerData data)
            {
                this.data = data;
                targetTime = data.time;
            }

            public void Update(float deltaTime)
            {
                if(!canMaking || amount >= data.amount)
                {
                    return;
                }
                if(currentTime >= targetTime)
                {
                    MakeEnemy();
                    targetTime += data.makeTime;
                }
                currentTime += deltaTime;
            }

            private void MakeEnemy()
            {
                amount++;
                EventMgr.Instance.TriggerEvent(GameEvent.MakeEnemy, new EnemyBean()
                {
                    EnemyType = data.enemyType,
                    EnemyData = data.enemyData
                });
            }

            public void Stop()
            {
                canMaking = false;
            }

            public void Resume()
            {
                canMaking = true;
            }
        }
    }
}