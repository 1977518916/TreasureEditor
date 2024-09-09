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

            foreach (MapTypeEnum type in Enum.GetValues(typeof(MapTypeEnum)))
            {
                MapSelectNode mapSelectNode = Instantiate(this.mapSelectNode, ScrollView.content);
                mapSelectNode.name = type.ToString();
                mapSelectNode.SetNode(AssetsLoadManager.LoadBg(type),
                    () => DataManager.GetLevelData().mapType = type);
                refreshAction += () => mapSelectNode.SetTick(type == DataManager.GetLevelData().mapType);
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