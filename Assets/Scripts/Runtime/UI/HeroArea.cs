using System;
using Factories;
using Runtime.Data;
using Runtime.Extensions;
using Runtime.Manager;
using Runtime.Utils;
using Spine.Unity;
using Tao_Framework.Core.Event;
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
            EventMgr.Instance.RegisterEvent(GetHashCode(), GameEvent.DataInitEnd, Init);
        }

        private void OnDestroy()
        {
            EventMgr.Instance.RemoveEvent(GetHashCode(), GameEvent.DataInitEnd);
        }

        private void Init()
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
            //private DataType.HeroPositionType positionType;
            private int index;
            private Action updateAction;
            private Transform heroParent, bulletParent;
            private void Awake()
            {
                index = int.Parse(name.Replace("hero", ""));
                //positionType = (DataType.HeroPositionType)(index - 1);
                //heroData = ReadWriteManager.Hero.GetHeroData(positionType);
                //DataManager.HeroDatas.Add(positionType, heroData);
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
                // transform.FindGet<Button>("Save")
                //     .onClick.AddListener(() => ReadWriteManager.Hero.SaveHeroData(positionType, heroData));
                
                transform.FindGet<Button>("Clear").onClick.AddListener(() =>
                {
                    heroData = new HeroData();
                    //DataManager.HeroDatas[positionType] = heroData;
                    //ReadWriteManager.Hero.SaveHeroData(positionType, null);
                    updateAction.Invoke();
                });
                
                transform.FindGet<Button>("Skill").onClick.AddListener(() =>
                {
                    SkillShowUi.Show(heroData);
                });
            }

            private void InitDropdown()
            {
                TMP_Dropdown heroType = transform.FindGet<TMP_Dropdown>("typeField");
                heroType.ClearOptions();
                foreach (EntityModelType value in Enum.GetValues(typeof(EntityModelType)))
                {
                    heroType.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
                }
                heroType.onValueChanged.AddListener(i =>
                {
                    heroData.modelType = (EntityModelType)i;
                    updateAction.Invoke();
                });
                
                updateAction += () =>
                {
                    heroType.SetValueWithoutNotify((int)heroData.modelType);
                    heroParent.ClearChild();
                    bulletParent.ClearChild();
                    heroType.RefreshShownValue();
                    if(heroData.modelType == EntityModelType.Null)
                    {
                        return;
                    }
                    HeroGameObjectFactory.Instance.Create(heroData.modelType, heroParent);
                    BulletGameObjectFactory.Instance.Create(heroData.modelType, LayerMask.NameToLayer("UI"), bulletParent);
                };

                TMP_Dropdown bulletType = transform.FindGet<TMP_Dropdown>("bulletField");
                bulletType.options.Clear();
                foreach (BulletType value in Enum.GetValues(typeof(BulletType)))
                {
                    bulletType.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
                }
                bulletType.onValueChanged.AddListener(i => heroData.bulletType = (BulletType)i);
                updateAction += () =>
                {
                    bulletType.value = (int)heroData.bulletType;
                    bulletType.RefreshShownValue();
                };
                var bulletAttribute = transform.FindGet<TMP_Dropdown>("bulletAttributeField");
                bulletAttribute.options.Clear();
                foreach (BulletAttributeType value in Enum.GetValues(typeof(BulletAttributeType)))
                {
                    bulletAttribute.options.Add(new TMP_Dropdown.OptionData(TranslateUtil.TranslateUi(value)));
                }
                bulletAttribute.onValueChanged.AddListener(i => heroData.bulletAttributeType = (BulletAttributeType)i);
                updateAction += () =>
                {
                    bulletAttribute.value = (int)heroData.bulletAttributeType;
                    bulletAttribute.RefreshShownValue();
                };
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

                var modelScaleField = transform.FindGet<TMP_InputField>("ScaleField");
                modelScaleField.onValueChanged.AddListener(value =>
                {
                    if (string.IsNullOrEmpty(value)) 
                    {
                        return;
                    }

                    var validValue = Convert.ToSingle(value) > 0
                        ? Convert.ToSingle(value) <= 2 ? Convert.ToSingle(value) : 2f
                        : 0.1f;
                    heroData.modelScale = validValue;
                    if (heroParent.GetChild(0) != null)
                    {
                        heroParent.GetChild(0).transform.localScale = new Vector3(validValue, validValue, 1f);
                    }
                });
                modelScaleField.onEndEdit.AddListener(value =>
                {
                    modelScaleField.SetTextWithoutNotify($"{heroData.modelScale}");
                });
                updateAction += () => modelScaleField.SetTextWithoutNotify($"{heroData.modelScale}");
            }
        }
    }
}