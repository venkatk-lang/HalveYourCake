using System.Collections.Generic;
using IACGGames;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButterSlicer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image pointerPrefab;
    [SerializeField] private Image specialPointerPrefab;
    [SerializeField] float pointerOffset;
    [SerializeField] int slices;

    [SerializeField] RectTransform rectTransform;
    List<Image> slicePointers = new();
    public int GetSlices() { return slices; }
    public void InitializeSlices(int _slices = -1)
    {
        Helpers.DestroyChildren(transform);
        slicePointers.Clear();
        slices = _slices;

        float unitLength = itemImage.rectTransform.rect.width / slices;
        Vector3 initialPosition = itemImage.rectTransform.position - Vector3.right * itemImage.rectTransform.rect.width / 2 + Vector3.up * pointerOffset;
        int specialPointerCondition = slices % 5 == 0 ? 5
           : 4 % slices == 0 ? 4
           : 3 % slices == 0 ? 3
           : 2 % slices == 0 ? 2 : 1;



        for (int i = 0; i < slices + 1; i++)
        {
            if (i % specialPointerCondition == 0)
            {
                Image inst = Instantiate(specialPointerPrefab, itemImage.transform);
                inst.rectTransform.position = initialPosition + Vector3.right * unitLength * i;
                slicePointers.Add(inst);
                continue;
            }
            var instance = Instantiate(pointerPrefab, itemImage.transform);
            instance.rectTransform.position = initialPosition + Vector3.right * unitLength * i;
            slicePointers.Add(instance);
        }

    }
    int filledSlices;
    Tweener tweener;
    public void UpdateSliceFill()
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, Input.mousePosition, null, out localMousePosition);

        float xMin = rectTransform.rect.xMin;
        float xMax = rectTransform.rect.xMax;
        float distancePerSlice = rectTransform.rect.width / slices;

        if (localMousePosition.x < xMin)
        {
            filledSlices = 0;
            Tween(0);
            //DOTween.To(
            //    (x) => itemImage.fillAmount = x,
            //    itemImage.fillAmount,
            //    0,
            //    0.2f
            //);
        }
        else if (localMousePosition.x > xMax)
        {
            filledSlices = slices;
            Tween(1);
            //DOTween.To(
            //    (x) => itemImage.fillAmount = x,
            //    itemImage.fillAmount,
            //    1,
            //    0.2f
            //);
        }
        else
        {
            filledSlices = Mathf.FloorToInt(((localMousePosition.x - xMin) + (distancePerSlice / 2)) / distancePerSlice);
            Tween(Mathf.Clamp01((float)filledSlices / slices));
            //DOTween.To(
            //    (x) => itemImage.fillAmount = x,
            //    itemImage.fillAmount,
            //    Mathf.Clamp01((float)filledSlices / slices),
            //    0.2f
            //);
        }
    }
    public Vector2Int GetFraction()
    {
        return new Vector2Int(filledSlices, slices);
    }
    private void Tween(float value)
    {
        //itemImage.fillAmount = value;
        //itemImage.SetAllDirty();
        //itemImage.SetVerticesDirty();
        //itemImage.SetMaterialDirty();
        //Canvas.ForceUpdateCanvases();
        //LayoutRebuilder.ForceRebuildLayoutImmediate(itemImage.rectTransform);
        tweener?.Kill();
        tweener = DOTween.To(
                (x) => itemImage.fillAmount = x,
                itemImage.fillAmount,
                value,
                0.2f
            );
        tweener.SetUpdate(true);
    }
    public int GetAnswer()
    {
        return filledSlices;
    }
}
