using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
  public static int score, highscore, xp, coins, gems, totalCoins, totalXP, totalGems, totalTokens;
  public static string lastFish;
  public static int multiplyer, multiplyerCap;
  public static Transform position;
  public static Mission[] missionList = new Mission[3];
  public static List<int> dodgeHelper = new();

  public delegate void Power(int i);
  public static Power powerEvent;
  public static Dictionary<int, string> powers = new()
  {
    { 0, "Invincible" },
    { 1, "Double Points" },
    { 2, "Unlimited Boost" }
  };
}
