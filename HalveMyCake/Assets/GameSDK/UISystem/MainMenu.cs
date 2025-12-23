using UnityEngine;
using UnityEngine.UI;

namespace IACGGames.UISystem
{

    public class MainMenu : UIPanelBase
    {
        
        [SerializeField] Button playButton;
        [SerializeField] Button howToPlayButton;

        [SerializeField] BuildingItem[] buildingItems; 
      
        protected override void OnEnable()
        {
            base.OnEnable();
            playButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(SFXAudioID.Click);
                OnPlayButtonPressed();
            });
            howToPlayButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(SFXAudioID.Click);
                OnHowToPlayButtonPressed();
            });
            InitializeLevelSelection();
        }

        private void InitializeLevelSelection()
        {
            int unlockedLevels = SaveDataHandler.Instance.SaveData.unlockedLevels;
            print(unlockedLevels);
            for (int i = 0; i < buildingItems.Length; i++)
            {
                if (i <= unlockedLevels)
                {
                    buildingItems[i].GetComponent<Button>().enabled = true;
                    if (SaveDataHandler.Instance.SaveData.starsEarned.Count > i)
                    {
                        buildingItems[i].SetStars(SaveDataHandler.Instance.SaveData.starsEarned[i]);
                    }
                }
                else
                {
                    buildingItems[i].GetComponent<Button>().enabled = false;
                }
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            playButton.onClick.RemoveAllListeners();
            howToPlayButton.onClick.RemoveAllListeners();
        }
        public void PlayButtonPressedWithInt(int difficulty)
        {
            GameSDKSystem.Instance.currentLevel = difficulty;
            OnPlayButtonPressed();
        }
        public void OnPlayButtonPressed()
        {
            GameSDKSystem.Instance.StartGame();
        }

        public void OnHowToPlayButtonPressed()
        {
            GameSDKSystem.Instance.StartTutorail();
       
        }
    }
}