using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStats : MonoBehaviour
{
  public GameSessionStats stats;

  // Start is called before the first frame update
  void Awake()
  {
    stats.totalCoins += stats.coins;
    stats.xp = stats.score/100;
    stats.totalXP += stats.xp;
  }
}
