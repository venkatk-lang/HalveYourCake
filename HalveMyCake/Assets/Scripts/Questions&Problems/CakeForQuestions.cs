using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Nuker.Tools;
using TMPro;

public class CakeForQuestions : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Image pointerPrefab;

    [SerializeField] float pointerOffset;
    [SerializeField] private int slices = -1;

    List<Image> slicePointers = new();
    public int GetSlices() { return slices; }

    CircleCutter cutter = new CircleCutter();
    [SerializeField] TextMeshProUGUI denominator;
    public void SetSlices(int total, int filled)
    {
        denominator.text = total.ToString();
        slices = total;
        InitializeSlices(total);
        UpdateSliceFill(filled);
    }

    public virtual void InitializeSlices(int _slices = -1)
    {
        for(int i = 0; i < slicePointers.Count; i++)
        {
            Destroy(slicePointers[i].gameObject);  
        }
        slicePointers.Clear();
        slices = _slices;
        cutter.Cut(slices);
        for (int i = 0; i < slices; i++)
        {
            Image ins = Instantiate(pointerPrefab, itemImage.transform);
            ins.transform.position = itemImage.transform.position + (Vector3)cutter.GetCut(i) * pointerOffset;
            ins.transform.Rotate(0, 0, -360f / slices * i);
            slicePointers.Add(ins);
        }
    }
    public void UpdateRotation()
    {
        int i = 0;
        foreach(var pointer in slicePointers)
        {
            pointer.transform.position = itemImage.transform.position + (Vector3)cutter.GetCut(i) * pointerOffset;
            pointer.transform.eulerAngles = new Vector3(0, 0, -360f / slices * i);
            i++;
        }
    }
    int filledSlices = 0;
    public virtual void UpdateSliceFill(int filledSlices)
    {
        itemImage.fillAmount = Mathf.Clamp01((float)filledSlices / (float)slices);
    }
}
