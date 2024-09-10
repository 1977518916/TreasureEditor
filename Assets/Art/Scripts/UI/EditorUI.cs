using UnityEngine;

namespace QFramework.Example
{
    public class EditorUIData : UIPanelData
    {
    }
    public partial class EditorUI : UIPanel
    {

        private void Awake()
        {
            HeroEdit_Btn.onClick.AddListener(ShowView<HeroEditView>);
            BossEdit_Btn.onClick.AddListener(ShowView<BossEditorView>);
            EnemyEdit_Btn.onClick.AddListener(ShowView<EnemyEditorView>);
            MapEdit_Btn.onClick.AddListener(ShowView<MapEditorView>);
        }
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as EditorUIData ?? new EditorUIData();
            // please add init code here
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

        private void ShowView<T>() where T : UIPanel
        {
            if(ContainView<T>())
            {
                UIKit.ShowPanel<T>();
                return;
            }
            UIKit.OpenPanel<T>(UILevel.PopUI);
        }

        private bool ContainView<T>()
        {
            foreach (Transform t in UIKit.Root.PopUI)
            {
                if(t.GetComponent<T>() != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}