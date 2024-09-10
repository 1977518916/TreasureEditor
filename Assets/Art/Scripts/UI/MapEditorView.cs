using System;
using Runtime.Data;
using Runtime.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Example
{
    public class MapEditorViewData : UIPanelData
    {
    }
    public partial class MapEditorView : UIPanel
    {
        Action refreshAction;
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as MapEditorViewData ?? new MapEditorViewData();
            // please add init code here
            Init();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }

        private void Init()
        {
            CloseButton.onClick.AddListener(UIKit.HidePanel<MapEditorView>);
            SaveButton.onClick.AddListener(DataManager.SaveLevelData);
            InitMaps();
            refreshAction.Invoke();
        }

        private void InitMaps()
        {
            int index = 0;
            for(; index < 6; index++)
            {
                MapSelectNode mapSelectNode = Instantiate(this.mapSelectNode, ScrollView.content);
                mapSelectNode.name = index.ToString();
                if(ResLoaderTools.TryGetMapSprite(index, out Sprite sprite))
                {
                    int index1 = index;
                    mapSelectNode.SetNode(sprite,
                        () => DataManager.GetLevelData().mapIndex = index1);
                    refreshAction += () => mapSelectNode.SetTick(index1 == DataManager.GetLevelData().mapIndex);
                }
            }
            
            foreach (Sprite sprite in ResLoaderTools.GetAllExternalMap())
            {
                MapSelectNode mapSelectNode = Instantiate(this.mapSelectNode, ScrollView.content);
                mapSelectNode.name = index++.ToString();
                int index1 = index;
                mapSelectNode.SetNode(sprite,
                    () => DataManager.GetLevelData().mapIndex = index1);
                refreshAction += () => mapSelectNode.SetTick(index1 == DataManager.GetLevelData().mapIndex);
            }

            foreach (Transform o in ScrollView.content)
            {
                o.GetComponent<MapSelectNode>().refreshAction = refreshAction;
            }
            GridLayoutGroup gridLayoutGroup = ScrollView.content.GetComponent<GridLayoutGroup>();
            gridLayoutGroup.CalculateLayoutInputHorizontal();
            gridLayoutGroup.CalculateLayoutInputVertical();
            ScrollView.content.sizeDelta = new Vector2(ScrollView.content.sizeDelta.x, gridLayoutGroup.preferredHeight);
        }
    }
}