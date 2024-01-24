using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStats : MonoBehaviour
{
  public GameSessionStats stats;

  void Awake()
  {
    Debug.Log("hele");
    stats.totalCoins = SaveSystem.LoadPlayer().coins + stats.coins;
    //stats.totalCoins = stats.coins + stats.coins;
    stats.xp = stats.score / 100;
    stats.totalXP += stats.xp;

    SaveSystem.SaveData(new(stats.totalCoins, stats.score));
  }
}
