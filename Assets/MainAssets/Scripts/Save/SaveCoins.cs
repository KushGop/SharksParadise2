using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCoins : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
  }

  public void SaveData(GameData data)
  {
    data.totalCoins = GameManager.totalCoins;
  }
}
