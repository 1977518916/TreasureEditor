using System;
using System.Collections.Generic;
using Runtime.Data;
using Runtime.Extensions;
using Runtime.Manager;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class SkillShowUi : MonoBehaviour
    {
        public HeroData heroData;
        public int skillId;
        private Dictionary<string, SkeletonDataAsset> assets;
        private RectTransform sklls;
        private GameObject nodePrefab;
        private void Start()
        {
            assets = DataManager.AllEntitySkillSpineDic;
            sklls = transform.FindGet<RectTransform>("Scroll/View/Skills");
            nodePrefab = transform.Find("Node").gameObject;
            ShowSkills();
        }


        private void ShowSkills()
        {
            foreach ((string key, SkeletonDataAsset value) in assets)
            {
                GameObject go = Instantiate(nodePrefab, sklls, false);
                SkeletonGraphic.NewSkeletonGraphicGameObject(value, go.transform, Graphic.defaultGraphicMaterial);
                go.AddComponent<SkillBtn>();

                go.name = value.name;
            }
        }

        private void SelectSkill(string key)
        {

        }

        private class SkillBtn : MonoBehaviour
        {
            public string key;
            public Image tick;
            private SkeletonGraphic skeletonGraphic;
            private RectTransform rectTransform;
            private void Awake()
            {
                tick = transform.FindGet<Image>("tick");
                rectTransform = GetComponent<RectTransform>();
                skeletonGraphic = transform.GetComponentInChildren<SkeletonGraphic>();
                skeletonGraphic.AnimationState.SetAnimation(0, skeletonGraphic.SkeletonData.Animations.Items[0].Name, true);
            }

            private void Start()
            {

                Invoke(nameof(TryMatch), .1f);
            }

            private void TryMatch()
            {
                skeletonGraphic.MatchRectTransformWithBounds();
                SkillData data = DataManager.SkillStruct.GetSkillDataOfKey(key);
                Vector2 skeSize = skeletonGraphic.rectTransform.sizeDelta;
                Vector2 size = rectTransform.sizeDelta;
                float scale = .5f;
                skeletonGraphic.transform.localScale = new Vector3(scale,scale,scale);
                skeletonGraphic.transform.localPosition = Vector3.zero;

            }
        }
    }
}