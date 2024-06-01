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
  public int control;
  public Mission[] missionList;
  public List<MissionName> missionListName;
  public UpgradesData upgradesData;
  public bool playedTutorial;
  public int age;

  // the values defined in this constructor will be the default values
  // the game starts with when there's no data to load
  public GameData()
  {
    playedTutorial = false;
    totalCoins = 0;
    totalGems = 0;
    totalTokens = 0;
    control = 0;
    age = 0;
    missionList = new Mission[3];
    missionListName = new();
    upgradesData = new();
    for (int i = 0; i < 3; i++)
    {
      missionList[i] = new();
    }
  }
}