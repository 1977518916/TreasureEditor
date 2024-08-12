using Spine.Unity;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public SkeletonGraphic root;
    
    private CanvasRenderer canvasRenderer;

    private void Start()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
    }
    
    private void Update()
    {
        canvasRenderer.materialCount = 1;
        canvasRenderer.SetMaterial(root.material, 0);
        canvasRenderer.SetMesh(root.GetLastMesh());
        canvasRenderer.SetTexture(root.mainTexture);
    }
}
