using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameManager : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
    print("load game manager");
    GameManager.totalCoins = data.totalCoins;
    GameManager.totalTokens = data.totalTokens;
    GameManager.totalGems = data.totalGems;
    GameManager.highscore = data.highscore;
    GameManager.control = data.control;
    GameManager.playedTutorial = data.playedTutorial;
    GameManager.isMusicOn = data.isMusicOn;
    GameManager.hasSeenTutorial = data.hasSeenTutorial;
  }

  public void SaveData(GameData data)
  {
  }
}
