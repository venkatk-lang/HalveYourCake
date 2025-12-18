using System.Collections.Generic;
using IACGGames;
using UnityEngine;
using UnityEngine.UI;

public class CakeSlicer : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Image pointerPrefab;

    [SerializeField] float pointerOffset;
    [SerializeField] private int slices = -1;

    List<Image> slicePointers = new();
    public int GetSlices() { return slices; }
    public virtual void InitializeSlices(int _slices = -1)
    {
        Helpers.DestroyChildren(this.transform);
        slicePointers.Clear();
        slices = _slices;
        //pointerOffset = _pointerOffset;
        for (int i = 0; i < slices; i++)
        {
            float angle = 360 * i / slices;
            var pointerInstance = Instantiate(pointerPrefab, itemImage.transform);
            Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle), 0);
            pointerInstance.rectTransform.position = itemImage.rectTransform.position + (pointerOffset * direction);
            slicePointers.Add(pointerInstance);
        }

    }
    int filledSlices = 0;
    public virtual void UpdateSliceFill()
    {
        Vector3 vector = Input.mousePosition - itemImage.rectTransform.position;
        vector.Normalize();
        float angle = Vector3.SignedAngle(itemImage.rectTransform.up, vector, -itemImage.rectTransform.forward);
        //if (angle < 0) angle = 360 + angle;
        float anglePerSlice = 360 / slices;
        angle += (anglePerSlice / 2);

        angle += angle < 0 ? 360 : 0;
        angle %= 360;

        filledSlices = Mathf.FloorToInt((angle) / anglePerSlice);
        itemImage.fillAmount = Mathf.Clamp01((float)filledSlices / slices);
        //angle -= anglePerSlice / 2;
        //itemImage.fillAmount = ((int)((angle / anglePerSlice) + 1) * anglePerSlice) / 360;
    }
    public int GetAnswer()
    {
        return Mathf.RoundToInt(filledSlices);
    }
}
