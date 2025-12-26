using DG.Tweening;
using UnityEngine;

public class CorrectOrWrong : MonoBehaviour
{
    [SerializeField] RectTransform wrong;
    [SerializeField] RectTransform correct;
    [SerializeField] RectTransform target;
    public void CorrectAnswer()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(correct.DOLocalMove(target.position - transform.position, 0.5f));
        sequence.AppendInterval(.5f);
        sequence.Append(correct.DOLocalMove(Vector3.zero, 0.5f));
        sequence.Play();
    }

    public void WrongAnswer()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(wrong.DOLocalMove(target.position - transform.position, 0.5f));
        sequence.AppendInterval(.5f);
        sequence.Append(wrong.DOLocalMove(Vector3.zero, 0.5f));
        sequence.Play();
    }
}
