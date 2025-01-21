using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveControls : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
  }

  public void SaveData(GameData data)
  {
    data.control = GameManager.control;
  }
}
