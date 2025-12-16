using System.Collections.Generic;
using IACGGames;
using UnityEngine;
using UnityEngine.UI;

public class ButterSlicer : MonoBehaviour
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

        for(int i = 0; i < slices; i++)
        {
            float distance = rectTransform.rect.size.x * (i / slices);
            var pointerInstance = Instantiate(pointerPrefab, itemImage.transform);
            Vector3 originPosition = new Vector3(rectTransform.rect.xMin, rectTransform.rect.yMax, 0);
            pointerInstance.transform.position = originPosition + Vector3.right * distance;
        }
    }
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
            itemImage.fillAmount = 0;
        }
        else if (localMousePosition.x > xMax)
        {
            itemImage.fillAmount = 1;
        }
        else
        {
            int filledSlices = Mathf.FloorToInt((localMousePosition.x - xMin) / distancePerSlice) + 1;
            itemImage.fillAmount = Mathf.Clamp01((float)filledSlices / slices);
        }
    }

    //public void UpdateSliceFill()
    //{
    //    Vector2 position = Input.mousePosition;
    //    float distancePerSlice = rectTransform.rect.size.x / slices;
    //    if (position.x < rectTransform.rect.xMin)
    //    {
    //        itemImage.fillAmount = 0;
    //    } else if(position.x > rectTransform.rect.xMax)
    //    {
    //        itemImage.fillAmount = 1;
    //    } else
    //    {
    //        itemImage.fillAmount = (int)((position.x - rectTransform.rect.xMin) / distancePerSlice) / slices;
    //    }
    //}
}
