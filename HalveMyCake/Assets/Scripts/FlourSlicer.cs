using IACGGames;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlourSlicer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image pointerPrefab;

    [SerializeField] float pointerOffset;
    [SerializeField] int slices;

    [SerializeField] RectTransform rectTransform;
    List<Image> slicePointers = new();
    public int GetSlices() { return slices; }
    public void InitializeSlices(int _slices = -1, float _pointerOffset = 200)
    {
        Helpers.DestroyChildren(transform);
        slicePointers.Clear();
        slices = _slices;
        pointerOffset = _pointerOffset;

        for (int i = 0; i < slices; i++)
        {
            float distance = rectTransform.rect.y * (i / slices);
            var pointerInstance = Instantiate(pointerPrefab, itemImage.transform);
            Vector3 originPosition = new Vector3(rectTransform.rect.yMin, rectTransform.rect.yMax, 0);
            pointerInstance.transform.position = originPosition + Vector3.right * distance;
        }
    }
    public void UpdateSliceFill()
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, Input.mousePosition, null, out localMousePosition);

        float yMin = rectTransform.rect.yMin;
        float yMax = rectTransform.rect.yMax;
        float distancePerSlice = rectTransform.rect.height / slices;

        if (localMousePosition.y < yMin)
        {
            itemImage.fillAmount = 0;
        }
        else if (localMousePosition.y > yMax)
        {
            itemImage.fillAmount = 1;
        }
        else
        {
            int filledSlices = Mathf.FloorToInt((localMousePosition.y - yMin) / distancePerSlice) + 1;
            itemImage.fillAmount = Mathf.Clamp01((float)filledSlices / slices);
        }
    }
}
