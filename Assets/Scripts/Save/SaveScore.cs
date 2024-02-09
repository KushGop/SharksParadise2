using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScore : MonoBehaviour, IDataPersistence
{

  private bool saveScore;

  public void LoadData(GameData data)
  {
    print("Load Score");
    GameManager.totalCoins = GameManager.coins + data.totalCoins;
    DataPersistenceManager.instance.SaveGame();
  }

  public void SaveData(GameData data)
  {
    print("Score Save");

    data.totalCoins = GameManager.totalCoins;
    if (data.highscore < GameManager.score)
    {
      print("HIGHSCORE");
      data.highscore = GameManager.score;
    }
  }


}
