using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCoins : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
    print("load loadcoins");
    GameManager.totalCoins = data.totalCoins;
    GameManager.totalTokens = data.totalTokens;
    GameManager.totalGems = data.totalGems;
    GameManager.highscore = data.highscore;
    GameManager.control = data.control;
  }

  public void SaveData(GameData data)
  {
    print("save loadcoins");
    data.control = GameManager.control;
  }
}
