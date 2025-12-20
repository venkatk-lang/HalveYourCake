using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(ScrollRect))]
public class ScrollParallax : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] [Range(0,1)] float scrollBias = 0f;

    public void OnValueChanged(Vector2 value)
    {
        print(value);
        scrollRect.normalizedPosition = value * scrollBias;
    }
}
