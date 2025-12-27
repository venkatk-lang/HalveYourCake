using DG.Tweening;
using IACGGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoretext;
    [SerializeField] Slider slider;
    [SerializeField] RectTransform target1, target2, target3;
    public void ShowGameOver(int score)
    {
        UpdateScore(GameSDKSystem.Instance.correctAnswers);
        gameObject.SetActive(true);
        //Time.timeScale = 0;
        scoretext.text = "Score: " + score.ToString();
    }
    LevelData levelData;
    public void UpdateScore(int points)
    {
        if (SaveDataHandler.Instance.levelData.Count > SaveDataHandler.Instance.SaveData.unlockedLevels)
        {
            levelData = SaveDataHandler.Instance.levelData[GameSDKSystem.Instance.currentLevel];
            if (GameSDKSystem.Instance.currentLevel == SaveDataHandler.Instance.SaveData.unlockedLevels)
            {
                if (levelData.starThresholds[0] <= points) SaveDataHandler.Instance.SaveData.unlockedLevels += 1;
            }
            int i = 0;

            while (levelData.starThresholds[i] <= points)
            {
                i++;
                //localStarsEarned++;
                if (i >= 3) break;
            }
            if (SaveDataHandler.Instance.SaveData.starsEarned == null)
            {
                SaveDataHandler.Instance.SaveData.starsEarned = new List<int>();
            }
            if (SaveDataHandler.Instance.SaveData.starsEarned.Count <= GameSDKSystem.Instance.currentLevel)
            {
                SaveDataHandler.Instance.SaveData.starsEarned.Add(new int());
            }
            if (SaveDataHandler.Instance.SaveData.starsEarned[GameSDKSystem.Instance.currentLevel] < i)
            {
                SaveDataHandler.Instance.SaveData.starsEarned[GameSDKSystem.Instance.currentLevel] = i;
            }
            SaveDataHandler.Instance.WriteDataToSaveFile(SaveDataFiles.SaveData);
            ScoreSliderFill(levelData, points);
        }
    }
    public void ScoreSliderFill(LevelData levelData, int points)
    {
        //slider.value = points / levelData.starThresholds[2];
        Tweener t = DOTween.To(() => slider.value, x => slider.value = x, Mathf.Clamp01(points / (float)levelData.starThresholds[2]), 1f).SetEase(Ease.OutCubic);
        var srect = slider.GetComponent<RectTransform>();
        target1.localPosition += Vector3.right * srect.rect.width * (levelData.starThresholds[0] / (float)levelData.starThresholds[2]);
        target2.localPosition += Vector3.right * srect.rect.width * (levelData.starThresholds[1] / (float)levelData.starThresholds[2]);
        target3.localPosition += Vector3.right * srect.rect.width;
    }

}
