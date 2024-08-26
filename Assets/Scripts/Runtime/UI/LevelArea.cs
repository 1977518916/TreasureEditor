using System;
using System.Collections;
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
            bossRun,
            bossAttack;

        private Button addBossBtn, removeBossBtn;
        private LevelData levelData;
        private Transform enemyParent, bossModelParent, bossBulletModelParent;
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
            BossUiInit();
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
            BossUIDataUpdate();
            ReadWriteManager.Level.SaveLevelData(levelData);
        }
        
        private void Delete()
        {
            ReadWriteManager.Level.SaveLevelData(null);
            levelData.timesDatas.Clear();
            levelData.timesDatas.Add(new TimesData());
            ShowLevelData();
        }

        #region BossUI
        
        /// <summary>
        /// Boss UI 查找
        /// </summary>
        private void BossUIFind()
        {
            addBossBtn = transform.FindGet<Button>("BossInfo/AddBossBtn");
            removeBossBtn = transform.FindGet<Button>("BossInfo/RemoveBossBtn");
            selectWhichBossDrop = transform.FindGet<TMP_Dropdown>("BossInfo/BossSelectDrop");
            bossModelParent = transform.Find("BossInfo/BossPanel/BossModel");
            bossBulletModelParent = transform.Find("BossInfo/BossPanel/BossBulletModel");
            selectBossModel = transform.FindGet<TMP_Dropdown>("BossInfo/BossPanel/BossModelDrop");
            selectBossBullet = transform.FindGet<TMP_Dropdown>("BossInfo/BossPanel/BossData/BossBulletTable/BossBulletDrop");
            bossGenerateTime = transform.FindGet<TMP_InputField>("BossInfo/BossPanel/BossData/GenerateTimeTable/GenerateTimeInputField");
            bossAttack = transform.FindGet<TMP_InputField>("BossInfo/BossPanel/BossData/BossAttackTable/BossAttackInputField");
            bossHp = transform.FindGet<TMP_InputField>("BossInfo/BossPanel/BossData/BossHpTable/BossHpInputField");
            bossRun = transform.FindGet<TMP_InputField>("BossInfo/BossPanel/BossRunSpeedTable/BossRunSpeedInputField");
            selectBossModel.ClearOptions();
            selectBossBullet.ClearOptions();
            selectWhichBossDrop.ClearOptions();
            addBossBtn.gameObject.SetActive(levelData.BossData.EntityModelType == EntityModelType.Null);
            removeBossBtn.gameObject.SetActive(levelData.BossData.EntityModelType != EntityModelType.Null);
            HideBossModel();
            HideBossBulletModel();
            UpdateBossHp($"{levelData.BossData.Hp}");
            UpdateBossAttack($"{levelData.BossData.Atk}");
            UpdateBossGenerateTime($"{levelData.BossData.Time}");
            UpdateBossRunSpeed($"{levelData.BossData.RunSpeed}");
        }
        
        /// <summary>
        /// Boss UI初始化
        /// </summary>
        private void BossUiInit()
        {
            levelData.BossData ??= new BossData();
            BossUIFind();
            BossUiBindAction();
            
        }
        
        private void BossUiBindAction()
        {
            addBossBtn.onClick.AddListener(AddBossAction);
            removeBossBtn.onClick.AddListener(ClearBossUIData);
            UpdateBulletDrop();
            UpdateBossModelTypeDropdown(levelData.BossData.EntityModelType != EntityModelType.Null);
            bossGenerateTime.onValueChanged.AddListener(UpdateBossGenerateTime);
            bossAttack.onValueChanged.AddListener(UpdateBossAttack);
            bossHp.onValueChanged.AddListener(UpdateBossHp);
            bossRun.onValueChanged.AddListener(UpdateBossRunSpeed);
        }
        
        private void AddBossAction()
        {
            addBossBtn.gameObject.SetActive(false);
            removeBossBtn.gameObject.SetActive(true);
            selectBossModel.options.Clear();
            selectBossBullet.options.Clear();
            selectWhichBossDrop.options.Clear();
            UpdateBossHp($"{levelData.BossData.Hp}");
            UpdateBossAttack($"{levelData.BossData.Atk}");
            UpdateBossGenerateTime($"{levelData.BossData.Time}");
            UpdateBossRunSpeed($"{levelData.BossData.RunSpeed}");
            UpdateBulletDrop();
            UpdateBossModelTypeDropdown(true);
        }

        /// <summary>
        /// Boss数据更新  用于保存按键中的保存
        /// </summary>
        private void BossUIDataUpdate()
        {
            levelData.BossData.Atk = Convert.ToInt32(bossAttack.text);
            levelData.BossData.Hp = Convert.ToInt32(bossHp.text);
            levelData.BossData.Time = Convert.ToSingle(bossGenerateTime.text);
            levelData.BossData.RunSpeed = Convert.ToSingle(bossRun.text);
            levelData.BossData.EntityModelType = (EntityModelType)selectBossModel.value;
            levelData.BossData.BulletType = IsHoldTheBullet((EntityModelType)selectBossModel.value)
                ? (BulletType)selectBossModel.value
                : BulletType.NoEntity;
        }
        
        /// <summary>
        /// 重置BossUI
        /// </summary>
        private void ClearBossUIData()
        {
            levelData.BossData = new BossData();
            selectBossModel.options.Clear();
            selectBossBullet.options.Clear();
            selectWhichBossDrop.options.Clear();
            selectBossModel.onValueChanged.RemoveAllListeners();
            var option = new TMP_Dropdown.OptionData
            {
                text = TranslateUtil.TranslateUi(EntityModelType.Null)
            };
            selectBossModel.options.Add(option);
            selectBossModel.value = 0;
            selectBossBullet.onValueChanged.RemoveAllListeners();
            selectBossBullet.options.Add(option);
            addBossBtn.gameObject.SetActive(true);
            removeBossBtn.gameObject.SetActive(false);
            bossGenerateTime.text = $"{levelData.BossData.Time}";
            bossAttack.text = $"{levelData.BossData.Atk}";
            bossHp.text = $"{levelData.BossData.Hp}";
            bossRun.text = $"{levelData.BossData.RunSpeed}";
            HideBossModel();
            HideBossBulletModel();
        }
        
        /// <summary>
        /// 更新Boss生成时间
        /// </summary>
        /// <param name="generateTime"></param>
        private void UpdateBossGenerateTime(string generateTime)
        {
            bossGenerateTime.text = generateTime;
            levelData.BossData.Time = Convert.ToSingle(bossGenerateTime.text);
        }
        
        /// <summary>
        /// 更新Boss攻击力
        /// </summary>
        /// <param name="bossAtk"></param>
        private void UpdateBossAttack(string bossAtk)
        {
            bossAttack.text = bossAtk;
            levelData.BossData.Atk = Convert.ToInt32(bossAttack.text);
        }
        
        /// <summary>
        /// 更新Boss血量
        /// </summary>
        /// <param name="bossHpValue"></param>
        private void UpdateBossHp(string bossHpValue)
        {
            bossHp.text = bossHpValue;
            levelData.BossData.Hp = Convert.ToInt32(bossHp.text);
        }   
        
        /// <summary>
        /// 更新Boss移动速度
        /// </summary>
        /// <param name="bossRunSpeed"></param>
        private void UpdateBossRunSpeed(string bossRunSpeed)
        {
            bossRun.text = bossRunSpeed;
            levelData.BossData.RunSpeed = Convert.ToSingle(bossRun.text);
        }

        /// <summary>
        /// 更新子弹下拉框
        /// </summary>
        private void UpdateBulletDrop()
        {
            selectBossBullet.value = -1;
            if (selectBossBullet.options.Count == 0) 
            {
                var self = new TMP_Dropdown.OptionData
                {
                    text = "自身子弹"
                };
                var anima = new TMP_Dropdown.OptionData
                {
                    text = "攻击动画"
                };
                selectBossBullet.options.Add(self);
                selectBossBullet.options.Add(anima);
            }
            selectBossBullet.onValueChanged.RemoveAllListeners();
            selectBossBullet.onValueChanged.AddListener(value =>
            {
                levelData.BossData.BulletType = value != -1 ? (BulletType)value : BulletType.NoEntity;
                if (IsHoldTheBullet(levelData.BossData.EntityModelType))   
                {
                    switch (value)
                    {
                        case 0:
                            ShowBossBulletModel(levelData.BossData.EntityModelType);
                            return;
                        case 1:
                            HideBossBulletModel();
                            return;
                    }
                }
                else
                {
                    HideBossBulletModel();
                }
            });
        }

        private bool IsHoldTheBullet(EntityModelType modelType)
        {
            return modelType is not (EntityModelType.DongZhuo or EntityModelType.Null);
        }
        
        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.5f);
            selectBossBullet.value = (int)levelData.BossData.BulletType;
        }

        /// <summary>
        /// 更新Boss模型选项下拉框
        /// </summary>
        private void UpdateBossModelTypeDropdown(bool isInit = false)
        {
            selectBossModel.options.Clear();
            selectBossModel.ClearOptions();
            selectBossModel.onValueChanged.RemoveAllListeners();
            if (!isInit) return;
            foreach (EntityModelType entityModelName in Enum.GetValues(typeof(EntityModelType)))
            {
                var option = new TMP_Dropdown.OptionData
                {
                    text = TranslateUtil.TranslateUi(entityModelName)
                };
                selectBossModel.options.Add(option);
            }

            selectBossModel.onValueChanged.AddListener(value =>
            {
                levelData.BossData.EntityModelType = (EntityModelType)value;
                UpdateBulletDrop();
                if (value == 0)
                {
                    HideBossModel();
                    return;
                }

                ShowBoss((EntityModelType)value);
            });
            
            selectBossModel.value = levelData.BossData.EntityModelType == EntityModelType.Null
                ? 1
                : (int)levelData.BossData.EntityModelType;
        }

        /// <summary>
        /// 显示Boss模型
        /// </summary>
        private void ShowBoss(EntityModelType entityModelType)
        {
            HideBossModel();
            var boss = AssetsLoadManager.LoadBoss(entityModelType, bossModelParent);
            if (entityModelType is EntityModelType.DongZhuo )
            {
                boss.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
            }
        }
        
        /// <summary>
        /// 隐藏Boss模型,也是删除
        /// </summary>
        private void HideBossModel()
        {
            bossModelParent.ClearChild();
        }
            
        private void ShowBossBulletModel(EntityModelType entityModelType)
        {
            HideBossBulletModel();
            if (entityModelType is EntityModelType.DongZhuo or EntityModelType.Null)
            {
                return;
            }
            AssetsLoadManager.LoadBulletSkeletonOfEnum(entityModelType, bossBulletModelParent);
        }

        private void HideBossBulletModel()
        {
            bossBulletModelParent.ClearChild();
        }

        #endregion
    }
}