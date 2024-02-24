using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUpgrades : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
    print("Load upgrades");
    UpgradesManager.upgradesData = data.upgradesData;
  }

  public void SaveData(GameData data)
  {
    data.upgradesData = UpgradesManager.upgradesData;
  }
}
