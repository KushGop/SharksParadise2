using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public int coins;
  public int score;

  public Player(int coins, int score)
  {
    this.coins = coins;
    this.score = score;
  }
}
