using IACGGames;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FlourSlicer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image pointerPrefab;

    [SerializeField] float pointerOffset;
    [SerializeField] int slices;

    [SerializeField] RectTransform rectTransform;
    List<Image> slicePointers = new();
    public int GetSlices() { return slices; }
    [SerializeField] private GameObject bg;
    private void OnEnable()
    {
        bg.SetActive(true);
    }
    private void OnDisable()
    {
        bg.SetActive(false);
    }
    public void InitializeSlices(int _slices = -1)
    {
        Helpers.DestroyChildren(transform);
        slicePointers.Clear();
        slices = _slices;

        float unitLength = itemImage.rectTransform.rect.height / slices;
        Vector3 initialPosition = itemImage.rectTransform.position - Vector3.up * itemImage.rectTransform.rect.height / 2 + Vector3.left * pointerOffset;
        for (int i = 0; i < slices + 1; i++)
        {
            var instance = Instantiate(pointerPrefab, itemImage.transform);
            instance.rectTransform.position = initialPosition + Vector3.up * unitLength * i;
        }

    }
    int filledSlices;
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
            // itemImage.fillAmount = 0;
            filledSlices = 0;
            DOTween.To(
                (x) => itemImage.fillAmount = x,
                itemImage.fillAmount,
                0,
                0.2f
            );
        }
        else if (localMousePosition.y > yMax)
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
            filledSlices = Mathf.FloorToInt(((localMousePosition.y - yMin) + (distancePerSlice / 2)) / distancePerSlice);
            // itemImage.fillAmount = Mathf.Clamp01((float)filledSlices / slices);
            DOTween.To(
                (x) => itemImage.fillAmount = x,
                itemImage.fillAmount,
                Mathf.Clamp01((float)filledSlices / slices),
                0.2f
            );
            //filledSlices++;
        }
    }
    public int GetAnswer()
    {
        return filledSlices;
    }
}
