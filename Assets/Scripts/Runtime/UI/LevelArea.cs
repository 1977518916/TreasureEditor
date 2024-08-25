using System;
using System.Collections.Generic;
using Runtime.Data;
using Runtime.Extensions;
using Runtime.Manager;
using Runtime.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class LevelArea : MonoBehaviour
    {
        private TMP_Dropdown dropdown,
            typeDropDown,
            mapDropDown,
            selectWhichBossDrop,
            selectBossModel,
            selectBossBullet;

        private TMP_InputField amount,
            time,
            timeInterval,
            hp,
            atk,
            speed,
            bossGenerateTime,
            bossHp,
            bossAttack;

        private Button addBossBtn, removeBossBtn;
        private LevelData levelData;
        private Transform enemyParent, bossModelParent;
        private Image bg;
        private TimesData CurrentData => levelData.timesDatas[dropdown.value];
        private void Awake()
        {
            dropdown = transform.FindGet<TMP_Dropdown>("LevelDrop");
            typeDropDown = transform.FindGet<TMP_Dropdown>("TimesInfo/Type");
            mapDropDown = transform.FindGet<TMP_Dropdown>("MapDrop");
            amount = transform.FindGet<TMP_InputField>("TimesInfo/Amount");
            time = transform.FindGet<TMP_InputField>("TimesInfo/TimeField");
            timeInterval = transform.FindGet<TMP_InputField>("TimesInfo/DelayTimeField");
            hp = transform.FindGet<TMP_InputField>("TimesInfo/Hp");
            atk = transform.FindGet<TMP_InputField>("TimesInfo/Atk");
            speed = transform.FindGet<TMP_InputField>("TimesInfo/Speed");
            enemyParent = transform.Find("TimesInfo/EnemyParent");
            bg = transform.parent.FindGet<Image>("bg");
            transform.FindGet<Button>("DeleteButton").onClick.AddListener(DeleteTimes);
            transform.FindGet<Button>("AddButton").onClick.AddListener(AddTimes);
            transform.FindGet<Button>("Delete").onClick.AddListener(Delete);
            transform.FindGet<Button>("Save").onClick.AddListener(Save);
            Init();
        }

        private void Init()
        {
            levelData = ReadWriteManager.Level.GetLevelData();
            DataManager.LevelData = levelData;

            typeDropDown.ClearOptions();
            mapDropDown.ClearOptions();

            ShowLevelData();
            ShowMap();
            dropdown.onValueChanged.AddListener(ShowTimeData);

            mapDropDown.onValueChanged.AddListener(index => levelData.mapType = (MapTypeEnum)index);
            mapDropDown.onValueChanged.AddListener(ShowMap);
            foreach (MapTypeEnum value in Enum.GetValues(typeof(MapTypeEnum)))
            {
                mapDropDown.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
            }

            foreach (EnemyTypeEnum value in Enum.GetValues(typeof(EnemyTypeEnum)))
            {
                typeDropDown.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
            }
            typeDropDown.onValueChanged.AddListener(value => CurrentData.enemyType = (EnemyTypeEnum)value);
            typeDropDown.onValueChanged.AddListener(_ => ShowEnemy());
            ShowTimeData(dropdown.value);

            amount.onValueChanged.AddListener(content => CurrentData.amount = int.Parse(content));
            time.onValueChanged.AddListener(t => CurrentData.time = float.Parse(t));
            timeInterval.onValueChanged.AddListener(t => CurrentData.makeTime = float.Parse(t));
            hp.onValueChanged.AddListener(v => CurrentData.enemyData.hp = int.Parse(v));
            atk.onValueChanged.AddListener(v => CurrentData.enemyData.atk = int.Parse(v));
            speed.onValueChanged.AddListener(v => CurrentData.enemyData.speed = float.Parse(v));
        }

        /// <summary>
        /// Boss UI初始化
        /// </summary>
        private void BossUiInit()
        {
            addBossBtn = transform.FindGet<Button>("BossInfo/AddBossBtn");
            removeBossBtn = transform.FindGet<Button>("BossInfo/RemoveBossBtn");
            selectWhichBossDrop = transform.FindGet<TMP_Dropdown>("BossInfo/BossSelectDrop");
            selectBossModel = transform.FindGet<TMP_Dropdown>("BossInfo/BossPanel/BossModelDrop");
            selectBossBullet = transform.FindGet<TMP_Dropdown>("BossInfo/BossPanel/BossData/BossBulletTable/BossBulletDrop");
            bossGenerateTime = transform.FindGet<TMP_InputField>("BossInfo/BossPanel/BossData/GenerateTimeTable/BossAttackInputField");
            bossAttack = transform.FindGet<TMP_InputField>("BossInfo/BossPanel/BossData/BossAttackTable/BossAttackInputField");
            bossHp = transform.FindGet<TMP_InputField>("BossInfo/BossPanel/BossData/BossHpTable/BossHpInputField");

            addBossBtn.onClick.AddListener(() =>
            {
                // 保存一次Boss数据
            });
        }

        private void ShowMap(int _ = 0)
        {
            bg.sprite = AssetsLoadManager.LoadBg(levelData.mapType);
        }

        private void ShowTimeData(int index)
        {
            amount.SetTextWithoutNotify(CurrentData.amount.ToString());
            timeInterval.SetTextWithoutNotify(CurrentData.makeTime.ToString());
            time.SetTextWithoutNotify(CurrentData.time.ToString());
            hp.SetTextWithoutNotify(CurrentData.enemyData.hp.ToString());
            atk.SetTextWithoutNotify(CurrentData.enemyData.atk.ToString());
            speed.SetTextWithoutNotify(CurrentData.enemyData.speed.ToString());
            typeDropDown.value = (int)CurrentData.enemyType;
            ShowEnemy();
        }

        private void ShowEnemy()
        {
            enemyParent.ClearChild();
            AssetsLoadManager.LoadEnemy(CurrentData.enemyType, enemyParent);
        }

        private void ShowBoss()
        {
            bossModelParent.ClearChild();
            //AssetsLoadManager.LoadEnemy()
        }

        private void ShowLevelData()
        {
            dropdown.ClearOptions();
            int i = 1;
            foreach (TimesData unused in levelData.timesDatas)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData($"第{i++}波"));
            }

        }

        private void DeleteTimes()
        {
            levelData.timesDatas.RemoveAt(dropdown.value);
            ShowLevelData();
            dropdown.SetValueWithoutNotify(0);
            ShowTimeData(dropdown.value);
        }

        private void AddTimes()
        {
            levelData.timesDatas.Add(new TimesData());
            ShowLevelData();
            dropdown.SetValueWithoutNotify(dropdown.options.Count - 1);
            ShowTimeData(dropdown.value);
        }

        private void Save()
        {
            ReadWriteManager.Level.SaveLevelData(levelData);
        }

        private void Delete()
        {
            ReadWriteManager.Level.SaveLevelData(null);
            levelData.timesDatas.Clear();
            levelData.timesDatas.Add(new TimesData());
            ShowLevelData();
        }
    }
}