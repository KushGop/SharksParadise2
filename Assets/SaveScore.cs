using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScore : MonoBehaviour, IDataPersistence
{

  private void Awake()
  {
  }


  public void LoadData(GameData data)
  {
    print("Save Score");
    GameManager.totalCoins = GameManager.coins + data.totalCoins;
    data.totalCoins = GameManager.totalCoins;
    if (data.highscore < GameManager.score)
    {
      print("HIGHSCORE");
      data.highscore = GameManager.score;
    }
  }

  public void SaveData(GameData data)
  {
    //print("Score Save");
    //data.totalCoins = GameManager.totalCoins;
    //print(GameManager.totalCoins + " " + GameManager.coins);
  }


}
