using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
  public long lastUpdated;
  public int totalCoins, highscore;
  public Mission[] missionList;

  // the values defined in this constructor will be the default values
  // the game starts with when there's no data to load
  public GameData()
  {
    this.totalCoins = 0;
    missionList = new Mission[3];
    for (int i = 0; i < 3; i++)
    {
      missionList[i] = new();
    }
  }
}