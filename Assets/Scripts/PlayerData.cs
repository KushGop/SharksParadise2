using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
  public int coins;
  public int score;

  public PlayerData(int coins, int score)
  {
    this.coins = coins;
    this.score = score;
  }
}
