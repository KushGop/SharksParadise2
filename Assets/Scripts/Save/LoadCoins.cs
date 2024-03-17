using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCoins : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
    GameManager.totalCoins = data.totalCoins;
    GameManager.totalTokens = data.totalTokens;
    GameManager.totalGems = data.totalGems;
  }

  public void SaveData(GameData data)
  {
  }
}
