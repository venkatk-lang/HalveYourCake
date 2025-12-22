using UnityEngine;
using UnityEngine.UI;

public class BuildingItem : MonoBehaviour
{
    [SerializeField] public Image[] stars = new Image[3];
    [SerializeField] public Sprite starActive;
    public void SetStars(int starCount = 0)
    {
        for(int i = 0; i < starCount && i < 3; i++)
        {
            stars[i].color = Color.green;
        }
    }
}
