using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.EasyIAP;

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
  public bool isMusicOn;
  public int age;
  public ShopProductNames hat;
  public AquariumData aquarium;
  public bool hasSeenTutorial;

  // the values defined in this constructor will be the default values
  // the game starts with when there's no data to load
  public GameData()
  {
    hat = ShopProductNames.NoHat;
    isMusicOn = true;
    playedTutorial = false;
    hasSeenTutorial = false;
    totalCoins = 0;
    totalGems = 0;
    totalTokens = 0;
    control = 0;
    age = 0;
    missionList = new Mission[3];
    missionListName = new();
    upgradesData = new();
    aquarium = new();
    for (int i = 0; i < 3; i++)
    {
      missionList[i] = new();
    }
  }
}