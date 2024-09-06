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
            SaveButton.onClick.AddListener(() => ReadWriteManager.Level.SaveLevelData(DataManager.LevelData));
            InitMaps();
            refreshAction.Invoke();
        }

        private void InitMaps()
        {

            foreach (MapTypeEnum type in Enum.GetValues(typeof(MapTypeEnum)))
            {
                SelectNode selectNode = Instantiate(SelectNode, ScrollView.content);
                selectNode.name = type.ToString();
                selectNode.SetNode(AssetsLoadManager.LoadBg(type),
                    () => DataManager.LevelData.mapType = type);
                refreshAction += () => selectNode.SetTick(type == DataManager.LevelData.mapType);
            }
            foreach (Transform o in ScrollView.content)
            {
                o.GetComponent<SelectNode>().refreshAction = refreshAction;
            }
            GridLayoutGroup gridLayoutGroup = ScrollView.content.GetComponent<GridLayoutGroup>();
            gridLayoutGroup.CalculateLayoutInputHorizontal();
            gridLayoutGroup.CalculateLayoutInputVertical();
            ScrollView.content.sizeDelta = new Vector2(ScrollView.content.sizeDelta.x, gridLayoutGroup.preferredHeight);
        }
    }
}