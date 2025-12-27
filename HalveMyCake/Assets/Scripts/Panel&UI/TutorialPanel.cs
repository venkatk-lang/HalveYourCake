using System.Collections;
using DG.Tweening;
using IACGGames;
using TMPro;
using UnityEngine;
public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private CakeSlicer cakeSlicer;
    [SerializeField] private FlourSlicer flourSlicer;
    [SerializeField] private ButterSlicer butterSlicer;

    [SerializeField] private RectTransform cakeRect;
    [SerializeField] private RectTransform flourRect;
    [SerializeField] private RectTransform butterRect;

    [SerializeField] GameObject inbetweenPanel1, inbetweenPanel2;
    bool inbetweenPanel1Completed = false;
    bool inbetweenPanel2Completed = false;
    [SerializeField] GameObject finalPanelGO;
    bool finalPanelCompleted = false;

    Vector3 cakeInitPos, flourInitPos, butterInitPos;
    Coroutine tutorialRoutine;
    private void Awake()
    {
        cakeInitPos = cakeRect.transform.localPosition;
        flourInitPos = flourRect.transform.localPosition;
        butterInitPos = butterRect.transform.localPosition;
    }
    private void OnEnable()
    {
        Debug.Log("Tut enable");
        ReInitializePositions();
        Time.timeScale = 0f;
        inbetweenPanel1Completed = false;
        inbetweenPanel2Completed = false;
        finalPanelCompleted = false;
        slide1Completed = false;
        slide2Completed = false;
        slide3Completed = false;
        slide1Started = false;
        slide2Started = false;
        slide3Started = false;

        finalPanelGO.SetActive(false);
        if(tutorialRoutine != null) StopCoroutine(tutorialRoutine);
        tutorialRoutine = StartCoroutine(TutorialCR());
    }

    private void ReInitializePositions()
    {
        cakeRect.transform.localPosition = cakeInitPos;
        flourRect.transform.localPosition = flourInitPos;
        butterRect.transform.localPosition = butterInitPos;
    }

    IEnumerator TutorialCR()
    {
        while (true)
        {
            yield return null;
            if (!slide1Completed)
            {
                Slide1();
                //yield return null;
                continue;
            }
            if (!slide2Completed)
            {
                Slide2();
                //yield return null;
                continue;
            }
            if (!inbetweenPanel1Completed)
            {
                inbetweenPanel1.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    inbetweenPanel1Completed = true;
                    inbetweenPanel1.SetActive(false);
                }
                //yield return null;
                continue;
            }
            if (!inbetweenPanel2Completed)
            {
                inbetweenPanel2.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    inbetweenPanel2.SetActive(false);
                    inbetweenPanel2Completed = true;
                }
                //yield return null;
                continue;
            }
            if (!slide3Completed)
            {
                Slide3();
                //yield return null;
                continue;
            }
            if (!finalPanelCompleted)
            {
                finalPanelGO.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    finalPanelGO.SetActive(false);
                    finalPanelCompleted = true;
                }
                continue;
            }
            break;
        }
        yield return new WaitForSecondsRealtime(1.5f);
        gameObject.SetActive(false);
        GameSDKSystem.Instance.ResumeGame();
        yield return null;
    }
    public void OnClickedSkipTutorial()
    {
        slide1Completed = true;
        slide2Completed = true;
        slide3Completed = true;
        inbetweenPanel1Completed = true;
        inbetweenPanel2Completed = true;
        slide1.SetActive(false);
        slide2.SetActive(false);
        slide3.SetActive(false);
        inbetweenPanel1.SetActive(false);
        inbetweenPanel2.SetActive(false);

        finalPanelGO.SetActive(true);
        finalPanelGO.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        StartCoroutine(AwaitSkipTutorial());
       // StopCoroutine(TutorialCR());
        if (tutorialRoutine != null) StopCoroutine(tutorialRoutine);
    }
    public IEnumerator AwaitSkipTutorial()
    {
        DOTween.KillAll();
        ReInitializePositions();
        yield return new WaitForSecondsRealtime(1.5f);
        gameObject.SetActive(false);
        GameSDKSystem.Instance.ResumeGame();
        yield return null;
    }
    #region Slide 1
    [SerializeField] GameObject slide1;
    bool slide1Started = false;
    bool slide1Completed = false;
    [SerializeField] TextMeshProUGUI[] slide1Text;
    [SerializeField] TextMeshProUGUI slide1AnsText;
    bool paused = false;
    private void Slide1()
    {
        if (!slide1Started)
        {
            // start
            slide1.SetActive(true);
            slide1Started = true;
            butterSlicer.InitializeSlices(4);
            DOTween.To(() => butterRect.localPosition, x => butterRect.localPosition = x, butterRect.localPosition + Vector3.left * Screen.width, 0.5f).SetUpdate(true);
            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
            paused = true;
            foreach (var item in slide1Text)
            {
                sequence.Append(ShowText(item.rectTransform, item));
                sequence.Append(HideText(item.rectTransform, item));
            }
            sequence.onComplete += () => paused = false;
            sequence.Play();
        }
        if (paused) return;
        butterSlicer.UpdateSliceFill();
        slide1AnsText.text = "" + butterSlicer.GetAnswer();
        slide1AnsText.ForceMeshUpdate();
        if (Input.GetMouseButtonUp(0))
        {
            if (butterSlicer.GetAnswer() == 2)
            {
                slide1Completed = true;
                DOTween.To(() => butterRect.localPosition, x => butterRect.localPosition = x, butterRect.localPosition + Vector3.left * Screen.width, 0.5f).SetUpdate(true);
                slide1.SetActive(false);
            }
        }

    }
    #endregion
    #region Slide 2
    [SerializeField] GameObject slide2;
    bool slide2Started = false;
    bool slide2Completed = false;
    [SerializeField] TextMeshProUGUI[] slide2Text;
    [SerializeField] TextMeshProUGUI slide2AnsText;
    private void Slide2()
    {
        if (!slide2Started)
        {
            // start
            slide2.SetActive(true);
            slide2Started = true;
            cakeSlicer.InitializeSlices(5);
            DOTween.To(() => cakeRect.localPosition, x => cakeRect.localPosition = x, cakeRect.localPosition + Vector3.left * Screen.width, 0.5f).SetUpdate(true);
            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
            paused = true;
            foreach (var item in slide2Text)
            {
                sequence.Append(ShowText(item.rectTransform, item));
                sequence.Append(HideText(item.rectTransform, item));
                sequence.onStepComplete += () => paused = false;
            }
            sequence.Play();
        }
        if (paused) return;
        cakeSlicer.UpdateSliceFill();
        slide2AnsText.text = "" + cakeSlicer.GetAnswer();
        if (Input.GetMouseButtonUp(0))
        {
            if (cakeSlicer.GetAnswer() == 3)
            {
                slide2Completed = true;
                DOTween.To(() => cakeRect.localPosition, x => cakeRect.localPosition = x, cakeRect.localPosition + Vector3.left * Screen.width, 0.5f).SetUpdate(true);
                slide2.SetActive(false);
            }
        }

    }
    #endregion
    #region Slide 3
    [SerializeField] GameObject slide3;
    bool slide3Started = false;
    bool slide3Completed = false;
    [SerializeField] TextMeshProUGUI[] slide3Text;
    [SerializeField] TextMeshProUGUI slide3AnsText;
    private void Slide3()
    {
        if (!slide3Started)
        {
            // start
            slide3.SetActive(true);
            slide3Started = true;
            flourSlicer.InitializeSlices(4);
            DOTween.To(() => flourRect.localPosition, x => flourRect.localPosition = x, flourRect.localPosition + Vector3.left * Screen.width, 0.5f).SetUpdate(true);
            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
            paused = true;
            foreach (var item in slide3Text)
            {
                sequence.Append(ShowText(item.rectTransform, item));
                sequence.Append(HideText(item.rectTransform, item));
                sequence.onStepComplete += () => paused = false;
            }
            sequence.Play();
        }
        if (paused) return;
        flourSlicer.UpdateSliceFill();
        slide3AnsText.text = "" + flourSlicer.GetAnswer();
        if (Input.GetMouseButtonUp(0))
        {
            if (flourSlicer.GetAnswer() == 1)
            {
                slide3Completed = true;
                DOTween.To(() => flourRect.localPosition, x => flourRect.localPosition = x, flourRect.localPosition + Vector3.left * Screen.width, 0.5f).SetUpdate(true);
                slide3.SetActive(false);
            }
        }
    }
    #endregion
    private Tween ShowText(RectTransform item, TextMeshProUGUI text)
    {
        Tween tween = item.DOMoveY(item.position.y + item.rect.height, 0.5f);
        tween.SetUpdate(true);
        tween.SetDelay(1f);
        return tween;
    }
    private Tween HideText(RectTransform item, TextMeshProUGUI text)
    {
        Tween tween = item.DOMoveY(item.position.y - item.rect.height, 0.5f);
        tween.SetUpdate(true);
        tween.SetDelay(5f);
        return tween;
    }

}
