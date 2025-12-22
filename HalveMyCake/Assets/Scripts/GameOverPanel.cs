using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoretext;
    public void ShowGameOver(int score)
    {
        gameObject.SetActive(true);
        //Time.timeScale = 0;
        scoretext.text = "Score: " + score.ToString();
    }

}
