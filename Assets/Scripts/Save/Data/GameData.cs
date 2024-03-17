using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
  public long lastUpdated;
  public int totalCoins;
  public int totalGems;
  public int totalTokens;
  public int highscore;
  public Mission[] missionList;
  public List<MissionName> missionListName;
  public UpgradesData upgradesData;

  // the values defined in this constructor will be the default values
  // the game starts with when there's no data to load
  public GameData()
  {
    totalCoins = 0;
    totalGems = 0;
    totalTokens = 0;
    missionList = new Mission[3];
    missionListName = new();
    upgradesData = new();
    for (int i = 0; i < 3; i++)
    {
      missionList[i] = new();
    }
  }
}