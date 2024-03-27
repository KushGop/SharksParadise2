using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUpgrades : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
    UpgradesManager.upgradesData = data.upgradesData;
        Debug.Log("After purchase PackCoins " + GameManager.totalCoins);
    }

  public void SaveData(GameData data)
  {
  }
}
