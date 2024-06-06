using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
  public static bool playedTutorial;
  public static bool hasBeenInit = false;
  public static bool missionFirstPass;
  public static int score, highscore, xp, coins, gems, totalCoins, totalXP, totalGems, totalTokens;
  public static string lastFish;
  public static int multiplyer, multiplyerCap;
  public static int control; // 0 = right, 1 = left
  public static Transform position;
  public static Mission[] missionList = new Mission[3];
  public static List<int> dodgeHelper = new();

  public static readonly float dayCycleTime = 40f;
  public static int day = 1;

  public delegate void Power(string text, float waitTime);
  public static Power eventText;
  public static Dictionary<int, string> powers = new()
  {
    { 0, "Invincible" },
    { 1, "Double Points" },
    { 2, "Unlimited Boost" }
  };

  public delegate void NormalEvent();
  public static NormalEvent switchToNight;
  public static NormalEvent switchToDay;

  public static NormalEvent disappear;
}
