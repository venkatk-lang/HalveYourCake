using System.Collections.Generic;
using IACGGames;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButterSlicer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image pointerPrefab;

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
        for (int i = 0; i < slices + 1; i++)
        {
            var instance = Instantiate(pointerPrefab, itemImage.transform);
            instance.rectTransform.position = initialPosition + Vector3.right * unitLength * i;
        }

    }
    int filledSlices;
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
            // itemImage.fillAmount = 0;
            filledSlices = 0;
            DOTween.To(
                (x) => itemImage.fillAmount = x,
                itemImage.fillAmount,
                0,
                0.2f
            );
        }
        else if (localMousePosition.x > xMax)
        {
            // itemImage.fillAmount = 1;
            filledSlices = slices;
            DOTween.To(
                (x) => itemImage.fillAmount = x,
                itemImage.fillAmount,
                1,
                0.2f
            );
        }
        else
        {
            filledSlices = Mathf.FloorToInt((localMousePosition.x - xMin) / distancePerSlice);
            // itemImage.fillAmount = Mathf.Clamp01((float)filledSlices / slices);
            DOTween.To(
                (x) => itemImage.fillAmount = x,
                itemImage.fillAmount,
                Mathf.Clamp01((float)filledSlices / slices),
                0.2f
            );
            filledSlices++;
        }
    }
    public int GetAnswer()
    {
        return filledSlices;
    }
}
