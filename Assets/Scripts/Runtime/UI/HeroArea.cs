using System;
using Runtime.Data;
using Runtime.Extensions;
using Runtime.Manager;
using Runtime.Utils;
using Spine.Unity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class HeroArea : MonoBehaviour
    {
        private void Awake()
        {
            foreach (Transform child in transform)
            {
                if(child.name.Contains("hero"))
                {
                    child.AddComponent<Hero>();
                }
            }
        }
        private class Hero : MonoBehaviour
        {
            private HeroData heroData;
            private DataType.HeroPositionType positionType;
            private int index;
            private Action updateAction;
            private Transform heroParent, bulletParent;
            private void Awake()
            {
                index = int.Parse(name.Replace("hero", ""));
                positionType = (DataType.HeroPositionType)(index - 1);
                heroData = ReadWriteManager.Hero.GetHeroData(positionType);
                DataManager.HeroDatas.Add(positionType, heroData);
                transform.FindGet<TextMeshProUGUI>("info").SetText($"英雄{index}");
                heroParent = transform.Find("HeroNode");
                bulletParent = transform.Find("BulletNode");
                InitDropdown();
                InitNumberField();
                InitButton();
                updateAction.Invoke();
            }
            private void InitButton()
            {
                Button save = transform.FindGet<Button>("Save");
                save.onClick.AddListener(() => ReadWriteManager.Hero.SaveHeroData(positionType, heroData));
                Button clear = transform.FindGet<Button>("Clear");
                clear.onClick.AddListener(() =>
                {
                    heroData = new HeroData();
                    DataManager.HeroDatas[positionType] = heroData;
                    ReadWriteManager.Hero.SaveHeroData(positionType, null);
                    updateAction.Invoke();
                });
            }

            private void InitDropdown()
            {
                TMP_Dropdown heroType = transform.FindGet<TMP_Dropdown>("typeField");
                heroType.ClearOptions();
                foreach (HeroTypeEnum value in Enum.GetValues(typeof(HeroTypeEnum)))
                {
                    heroType.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
                }
                heroType.onValueChanged.AddListener(i =>
                {
                    heroData.heroTypeEnum = (HeroTypeEnum)i;
                    updateAction.Invoke();
                });
                updateAction += () =>
                {
                    heroType.value = (int)heroData.heroTypeEnum;
                    heroParent.ClearChild();
                    bulletParent.ClearChild();
                    if(heroData.heroTypeEnum == HeroTypeEnum.Null)
                    {
                        return;
                    }
                    AssetsLoadManager.LoadHero(heroData.heroTypeEnum, heroParent);
                    AssetsLoadManager.LoadBullet(heroData, bulletParent);
                };

                TMP_Dropdown bulletType = transform.FindGet<TMP_Dropdown>("bulletField");
                bulletType.options.Clear();
                foreach (BulletType value in Enum.GetValues(typeof(BulletType)))
                {
                    bulletType.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
                }
                bulletType.onValueChanged.AddListener(i => heroData.bulletType = (BulletType)i);
                updateAction += () => { bulletType.value = (int)heroData.bulletType; };
            }

            private void InitNumberField()
            {
                TMP_InputField hp = transform.FindGet<TMP_InputField>("hpField");
                hp.onValueChanged.AddListener(content => heroData.hp = int.Parse(content));
                updateAction += () => hp.SetTextWithoutNotify(heroData.hp.ToString());

                TMP_InputField atk = transform.FindGet<TMP_InputField>("atkField");
                atk.onValueChanged.AddListener(content => heroData.atk = int.Parse(content));
                updateAction += () => atk.SetTextWithoutNotify(heroData.atk.ToString());

                TMP_InputField bulletAmount = transform.FindGet<TMP_InputField>("bulletAmountField");
                bulletAmount.onValueChanged.AddListener(content => heroData.bulletAmount = int.Parse(content));
                updateAction += () => bulletAmount.SetTextWithoutNotify(heroData.bulletAmount.ToString());

                TMP_InputField atkInterval = transform.FindGet<TMP_InputField>("AtkIntervalField");
                atkInterval.onValueChanged.AddListener(content => heroData.atkInterval = float.Parse(content));
                updateAction += () => atkInterval.SetTextWithoutNotify(heroData.atkInterval.ToString());
                
                TMP_InputField amountField = transform.FindGet<TMP_InputField>("AmountField");
                amountField.onValueChanged.AddListener(content => heroData.shooterAmount = int.Parse(content));
                updateAction += () => amountField.SetTextWithoutNotify(heroData.shooterAmount.ToString());
            }
        }
    }
}