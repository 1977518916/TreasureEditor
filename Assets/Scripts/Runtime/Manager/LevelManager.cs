using System;
using Runtime.Component.Position;
using Runtime.Data;
using Tao_Framework.Core.Singleton;
using UnityEngine;

namespace Runtime.Manager
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private LevelData levelData;
        private float currentTime = 0f, targetTime = 0f;
        private int index = 0;
        private void Awake()
        {
            levelData = DataManager.LevelData;
            targetTime = levelData.timesDatas[index++].time;
        }

        private void Update()
        {
            if(index >= levelData.timesDatas.Count)
            {
                return;
            }
            currentTime += Time.deltaTime;
            if(currentTime > targetTime)
            {
                MakeEnemy();
                targetTime = currentTime + levelData.timesDatas[index++].time;
            }
        }

        private void MakeEnemy()
        {
            
        }

        private void OnDestroy()
        {
            DestroyInstance();
        }
    }
}