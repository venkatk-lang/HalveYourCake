using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public string playerId;
    public string playerName;
    //public int unlockedLevels = 1;
    //public List<int> starsEarned = new();
    public PlayerData(string id, string name)
    {
        this.playerId = id;
        this.playerName = name;
    }
}