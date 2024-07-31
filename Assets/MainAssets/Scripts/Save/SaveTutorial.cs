using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTutorial : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {

  }

  public void SaveData(GameData data)
  {
    data.playedTutorial = GameManager.playedTutorial;
  }
}
