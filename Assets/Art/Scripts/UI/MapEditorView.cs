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
            CloseButton.onClick.AddListener(() => UIKit.HidePanel<MapEditorView>());
            SaveButton.onClick.AddListener(() => ReadWriteManager.Level.SaveLevelData(DataManager.GetLevelData()));
            InitMaps();
            refreshAction.Invoke();
        }

        private void InitMaps()
        {
            for(int index = 0; index < 6; index++)
            {
                MapSelectNode mapSelectNode = Instantiate(this.mapSelectNode, ScrollView.content);
                mapSelectNode.name = index.ToString();
                if(ResLoaderTools.TryGetMapSprite(index, out Sprite sprite))
                {
                    mapSelectNode.SetNode(sprite,
                        () => DataManager.GetLevelData().mapSprite = sprite);
                    refreshAction += () => mapSelectNode.SetTick(sprite == DataManager.GetLevelData().mapSprite);
                }

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