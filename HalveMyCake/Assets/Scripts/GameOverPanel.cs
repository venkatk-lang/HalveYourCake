using IACGGames;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoretext;
    public void ShowGameOver(int score)
    {
        UpdateScore(GameSDKSystem.Instance.correctAnswers);
        gameObject.SetActive(true);
        //Time.timeScale = 0;
        scoretext.text = "Score: " + score.ToString();
    }
    public void UpdateScore(int points)
    {
        if (SaveDataHandler.Instance.levelData.Count > SaveDataHandler.Instance.SaveData.unlockedLevels)
        {
            var levelData = SaveDataHandler.Instance.levelData[GameSDKSystem.Instance.currentLevel];
            if (GameSDKSystem.Instance.currentLevel == SaveDataHandler.Instance.SaveData.unlockedLevels)
            {
                if (levelData.starThresholds[0] <= points) SaveDataHandler.Instance.SaveData.unlockedLevels += 1;
                int i = 0;
                while (levelData.starThresholds[i] <= points)
                {
                    if (SaveDataHandler.Instance.SaveData.starsEarned == null)
                    {
                        SaveDataHandler.Instance.SaveData.starsEarned = new System.Collections.Generic.List<int>();
                    }
                    if(SaveDataHandler.Instance.SaveData.starsEarned.Count <= GameSDKSystem.Instance.currentLevel)
                    {
                        SaveDataHandler.Instance.SaveData.starsEarned.Add(new int());
                    }
                    SaveDataHandler.Instance.SaveData.starsEarned[GameSDKSystem.Instance.currentLevel] =
                        SaveDataHandler.Instance.SaveData.starsEarned[GameSDKSystem.Instance.currentLevel] >= 3 ? 3 :
                        SaveDataHandler.Instance.SaveData.starsEarned[GameSDKSystem.Instance.currentLevel] + 1;
                    i++;
                    if (i >= 3) break;
                }
                SaveDataHandler.Instance.WriteDataToSaveFile(SaveDataFiles.SaveData);
            }


        }
    }

}
