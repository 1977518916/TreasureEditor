using System;
using System.Collections.Generic;
using Factories;
using Runtime.Data;
using Runtime.Extensions;
using Runtime.Manager;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class SkillShowUi : MonoBehaviour
    {
        private HeroData heroData;
        private Dictionary<string, SkeletonDataAsset> assets;
        private RectTransform sklls;
        private GameObject nodePrefab, scroll;
        private static SkillShowUi instance;
        private List<SkillBtn> skillBtns;
        private int clickIndex;

        public static void Show(HeroData data)
        {
            if(!instance)
            {
                instance = FindFirstObjectByType<SkillShowUi>();
                instance.ShowSkills();
            }
            instance.heroData = data;
            instance.UpdateSelection();
        }

        private void Awake()
        {
            assets = DataManager.AllEntitySkillSpineDic;
            scroll = transform.Find("Scroll").gameObject;
            sklls = scroll.transform.FindGet<RectTransform>("View/Skills");
            nodePrefab = transform.Find("Node").gameObject;
            scroll.transform.FindAdd<Button>("Close").onClick.AddListener(Hide);
        }

        private void Hide()
        {
            scroll.SetActive(false);
        }

        private void ShowSkills()
        {
            skillBtns = new List<SkillBtn>();
            foreach (SkeletonDataAsset value in assets.Values)
            {
                GameObject go = Instantiate(nodePrefab, sklls, false);
                SkeletonGraphicFactory.Instance.Create(value, go.transform);

                SkillBtn skill = go.AddComponent<SkillBtn>();
                skill.gameObject.SetActive(true);
                skill.key = value.name;
                skill.ClickAction = SelectSkill;
                skillBtns.Add(skill);
                go.name = value.name;
            }
        }

        private void SelectSkill(string key)
        {
            int index = clickIndex++ % 2;
            switch(index)
            {
                case 0:
                    heroData.skillData1 = DataManager.GetSkillStruct().GetSkillDataOfKey(key);
                    break;
                case 1:
                    heroData.skillData2 = DataManager.GetSkillStruct().GetSkillDataOfKey(key);
                    break;
            }
            UpdateSelection();
        }

        private void UpdateSelection()
        {
            scroll.SetActive(true);

            foreach (SkillBtn skillBtn in skillBtns)
            {
                skillBtn.RefreshTick(heroData);
            }
        }

        private class SkillBtn : MonoBehaviour
        {
            public string key;
            public Action<string> ClickAction { private get; set; }
            private Tick tick;
            private SkeletonGraphic skeletonGraphic;

            public void Awake()
            {
                tick = transform.FindAdd<Tick>("tick");
                skeletonGraphic = transform.GetComponentInChildren<SkeletonGraphic>();
                skeletonGraphic.AnimationState.SetAnimation(0, skeletonGraphic.SkeletonData.Animations.Items[0].Name, true);
                gameObject.GetComponent<Button>().onClick.AddListener(Click);
            }

            private void Start()
            {
                Invoke(nameof(TryMatch), .5f);
            }

            private void TryMatch()
            {
                skeletonGraphic.MatchRectTransformWithBounds();
                SkillData data = DataManager.GetSkillStruct().GetSkillDataOfKey(key);
                float scale = .5f;
                Vector3 pos = Vector3.zero;
                if(data != null)
                {
                    scale = data.showScale;
                    pos = data.showPosition;
                }
                skeletonGraphic.transform.localScale = new Vector3(scale, scale, scale);
                skeletonGraphic.transform.localPosition = pos;
            }

            private void Click()
            {
                ClickAction?.Invoke(key);
            }

            public void RefreshTick(HeroData heroData)
            {
                if(key.Equals(heroData.skillData1?.key))
                {
                    tick.Set1(key);
                    return;
                }
                if(key.Equals(heroData.skillData2?.key))
                {
                    tick.Set2(key);
                    return;
                }
                tick.SetNone();
            }

            private class Tick : MonoBehaviour
            {
                TextMeshProUGUI text;
                private void Awake()
                {
                    text = transform.GetComponentInChildren<TextMeshProUGUI>();
                }

                public void Set1(string s)
                {
                    ExchangeActive(true);
                    text.SetText(1.ToString());
                    Debug.Log($"{s}: 作为第一个技能");
                }

                public void Set2(string s)
                {
                    ExchangeActive(true);
                    text.SetText(2.ToString());
                    Debug.Log($"{s}: 作为第二个技能");
                }

                public void SetNone()
                {
                    ExchangeActive(false);
                }

                private void ExchangeActive(bool active)
                {
                    if(active == gameObject.activeSelf)
                    {
                        return;
                    }
                    gameObject.SetActive(active);
                }
            }
        }
    }
}