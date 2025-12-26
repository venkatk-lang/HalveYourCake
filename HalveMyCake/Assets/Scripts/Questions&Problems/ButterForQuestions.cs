using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterForQuestions : MonoBehaviour
{
    [SerializeField] public Image itemImage;
    [SerializeField] public Image pointerPrefab;

    [SerializeField] public float pointerOffset;
    int slices;

    List<Image> slicePointers = new();
    public void SetSlices(int total, int filled)
    {
        slices = total;
        InitializeSlices(total, filled);
    }
    public void InitializeSlices(int _slices, int filled)
    {
        for (int i = 0; i < slicePointers.Count; i++)
        {
            Destroy(slicePointers[i].gameObject);
        }
        
        for (int i = 0; i < slices; i++)
        {
            Image ins = Instantiate(pointerPrefab, itemImage.transform);
            slicePointers.Add(ins);
            if (i < filled)
            {
                ins.color = Color.yellow;
            }
        }
    }

}
