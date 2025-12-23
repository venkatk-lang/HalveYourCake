using UnityEngine;
using System.Collections.Generic;

namespace IACGGames {
    [System.Serializable]
    public class SaveData
    {
        [Header("Settings Save Data")]
        public bool vibrationOn;
        public float bgSoundValue;
        public float inGameSoundFXValue;
        public int unlockedLevels;
        public List<int> starsEarned = new List<int>();

        [Header("Level Save Data")]

        public bool tutorialCompleted;
        public SaveData(GameConfig gameConfig)
        {
            tutorialCompleted = false;
            vibrationOn = true;
            bgSoundValue = 1f;
            inGameSoundFXValue = 1f;
            starsEarned = new List<int>();
        }

    }

}
