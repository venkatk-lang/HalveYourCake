using System;

[Serializable]
public class PlayerData
{
    public string playerId;
    public string playerName;
    public int highScore;
    public int unlockedLevels = 1;
    public PlayerData(string id, string name, int score = 0)
    {
        this.playerId = id;
        this.playerName = name;
        this.highScore = score;
    }
}