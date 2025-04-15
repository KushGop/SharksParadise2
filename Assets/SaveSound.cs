using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSound : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
  }

  public void SaveData(GameData data)
  {
    data.isMusicOn = GameManager.isMusicOn;
  }
}
