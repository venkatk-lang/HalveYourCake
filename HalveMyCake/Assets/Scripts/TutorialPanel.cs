using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private CakeSlicer cakeSlicer;
    [SerializeField] private FlourSlicer flourSlicer;
    [SerializeField] private ButterSlicer butterSlicer;

    [SerializeField] private RectTransform cakeRect;
    [SerializeField] private RectTransform flourRect;
    [SerializeField] private RectTransform butterRect;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        StartCoroutine(TutorialCR());
    }
    IEnumerator TutorialCR()
    {
        while (true)
        {
            if (!slide1Completed)
            {
                Slide1();
                yield return null;
                continue;
            }
            if (!slide2Completed)
            {
                Slide2();
                yield return null;
                continue;
            }
            break;
        }
        yield return null;
    }
    bool slide1Started = false;
    bool slide1Completed = false;
    [SerializeField] TextMeshProUGUI[] slide1Text;
    private void Slide1()
    {
        if (!slide1Started)
        {
            // start
            slide1Started = true;
            butterSlicer.InitializeSlices(4);
            DOTween.To(() => butterRect.localPosition, x => butterRect.localPosition = x, butterRect.localPosition + Vector3.left * 1920, 0.5f).SetUpdate(true);
        }

        //// Update
        //Sequence sequence;
        //foreach(var item in slide1Text)
        //{

        //}
        butterSlicer.UpdateSliceFill();
        if (Input.GetMouseButtonUp(0))
        {
            if (butterSlicer.GetAnswer() == 2) slide1Completed = true;
        }

    }
    float moveDistance = 100;
    private Tweener ShowText(RectTransform item, TextMeshProUGUI text)
    {
        var currentPosition = item.position;
        var tween = item.DOMove(currentPosition, 0.8f);
        tween.startValue = currentPosition + Vector3.down * item.rect.height;
        tween.SetEase(Ease.Linear);
        tween.SetUpdate(true);
        var fadeTween = text.DOFade(0, .5f);
        fadeTween.Pause();
        fadeTween.SetDelay(7f);
        tween.SetUpdate(true);
        tween.onComplete = () => { fadeTween.Play(); };
        return fadeTween;
    }
    bool slide2Started = false;
    bool slide2Completed = false;
    private void Slide2()
    {
        if (!slide2Started)
        {
            // start
            slide2Started = true;
            butterSlicer.InitializeSlices(4);
            DOTween.To(() => cakeRect.localPosition, x => cakeRect.localPosition = x, cakeRect.localPosition + Vector3.left * 1920, 0.5f).SetUpdate(true);
            //butterRect.DOMove(Vector3.zero, 0.5f).SetEase(Ease.Linear).SetUpdate(true);
        }

        // Update
        butterSlicer.UpdateSliceFill();
        if (Input.GetMouseButtonUp(0))
        {
            if (butterSlicer.GetAnswer() == 2) slide2Completed = true;
        }

    }

}
