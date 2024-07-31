using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUpgrades : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
    UpgradesManager.upgradesData = data.upgradesData;
  }

  public void SaveData(GameData data)
  {
  }
}
