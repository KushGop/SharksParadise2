using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCurrency : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
  }

  public void SaveData(GameData data)
  {
    data.totalCoins = GameManager.totalCoins;
    data.totalGems = GameManager.totalGems;
  }
}
