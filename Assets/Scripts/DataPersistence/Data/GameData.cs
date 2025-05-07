using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int dayCount;
    public Vector3 playerPosition;
    public Dictionary<string, bool> flowersCollected;

    public GameData()
    {
        this.dayCount = 1;
        this.playerPosition = new Vector3(-5, 1, -8);
        flowersCollected = new Dictionary<string, bool>();
    }
}
