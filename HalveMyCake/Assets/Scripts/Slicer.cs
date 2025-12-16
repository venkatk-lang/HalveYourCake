using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slicer : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Image pointerPrefab;

    [SerializeField] ItemType type;
    [SerializeField] float pointerOffset;
    [SerializeField] private int slices;

    List<Image> slicePointers = new();
    public int GetSlices => slices;
    public virtual void InitializeSlices(int _slices, float _pointerOffset)
    {
        slices = _slices;
        pointerOffset = _pointerOffset;
        for (int i = 0; i < slices; i++)
        {
            float angle = 360 * i / slices;
            var pointerInstance = Instantiate(pointerPrefab);
            Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle), 0);
            pointerInstance.rectTransform.position = itemImage.rectTransform.position + (pointerOffset * direction);
            slicePointers.Add(pointerInstance);
        }

    }

    public virtual void UpdateSliceFill(float angle)
    {
        float anglePerSlice = 360 / slices;
        angle += anglePerSlice;
        angle %= 360;
        itemImage.fillAmount = (int)(angle / anglePerSlice) * anglePerSlice;
    }
}
public enum ItemType
{
    circular,
    horizontal,
    vertical
}