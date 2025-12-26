using System.Collections.Generic;
using IACGGames;
using UnityEngine;
using UnityEngine.UI;
using Nuker.Tools;
using DG.Tweening;

public class CakeSlicer : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Image pointerPrefab;
    [SerializeField] Image specialPointerPrefab;

    [SerializeField] float pointerOffset;
    [SerializeField] private int slices = -1;

    List<Image> slicePointers = new();
    public int GetSlices() { return slices; }

    CircleCutter cutter = new CircleCutter();

    [SerializeField] GameObject bgGameObject;
    
    private void OnDisable()
    {
        bgGameObject.SetActive(false);
    }
    private void OnEnable()
    {
        bgGameObject.SetActive(true);
    }
    public virtual void InitializeSlices(int _slices = -1)
    {
        Helpers.DestroyChildren(this.transform);
        slicePointers.Clear();
        slices = _slices;
        cutter.Cut(slices);
        int specialPointerCondition = slices % 5 == 0 ? 5
           : slices % 4 == 0 ? 4
           : slices % 3 == 0 ? 3
           : slices % 2 == 0 ? 2 : 1;
        for (int i = 0; i < slices; i++) {
            if(i % specialPointerCondition == 0)
            {
                Image inst = Instantiate(specialPointerPrefab, itemImage.transform);
                inst.transform.position = itemImage.transform.position + (Vector3)cutter.GetCut(i) * pointerOffset;
                slicePointers.Add(inst);
                continue;
            }
            Image ins = Instantiate(pointerPrefab, itemImage.transform);
            ins.transform.position = itemImage.transform.position + (Vector3)cutter.GetCut(i) * pointerOffset;
            slicePointers.Add(ins);
        }
    }
    int filledSlices = 0;
    public virtual void UpdateSliceFill()
    {
        Vector3 vector = Input.mousePosition - itemImage.rectTransform.position;
        vector.Normalize();

        filledSlices = cutter.PieceSelection(vector);
        // itemImage.fillAmount = Mathf.Clamp01((float)filledSlices/(float)slices);
        Tween(Mathf.Clamp01((float)filledSlices / (float)slices));
        //DOTween.To(
        //        (x) => itemImage.fillAmount = x,
        //        itemImage.fillAmount,
        //        Mathf.Clamp01((float)filledSlices/(float)slices),
        //        0.2f
        //    );
    }
    public Vector2Int GetFraction()
    {
        return new Vector2Int(filledSlices, slices);
    }
    Tweener tweener;
    private void Tween(float value)
    {
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
        return Mathf.RoundToInt(filledSlices);
    }
}
