using UnityEngine;
using UnityEngine.UI;
[ExecuteAlways]
public class LayoutRebuilderImp : MonoBehaviour
{
    private void OnRectTransformDimensionsChange()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponentInParent<RectTransform>());
    }
}
