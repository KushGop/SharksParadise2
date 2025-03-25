using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
  public static bool playedTutorial;
  public static bool hasBeenInit = false;
  public static bool missionFirstPass;
  public static int score, highscore, xp, coins, gems, totalCoins, totalXP, totalGems, totalTokens, missionCoins;
  public static Fishes lastFish;
  public static int multiplyer, multiplyerCap, starfishMulti;
  public static int control; // 0 = right, 1 = left
  public static Transform position;
  public static Mission[] missionList = new Mission[3];
  public static List<int> dodgeHelper = new();
  public static int adCount;
  public static bool isAlive = true;
  public static bool isFlying = false;
  public static readonly float dayCycleTime = 40f;
  public static int day = 1;
  public static bool isNight = false;
  public static bool isGoldRush = false;

  public delegate void Power(string text, float waitTime);
  public static Power eventText;
  public static Dictionary<int, string> powers = new()
  {
    { -1, "Second Chance" },
    { 0, "Shield" },
    { 1, "Double Points" },
    { 2, "Speed Boost" }
  };

  public delegate void NormalEvent();
  public static NormalEvent fishEaten;
  public static NormalEvent switchToNight;
  public static NormalEvent switchToDay;
  public static NormalEvent TurnCoinsOn;
  public static NormalEvent TurnCoinsOff;
  public static NormalEvent disappear;
  public static NormalEvent pause;
  public static NormalEvent unpause;

  public static NormalEvent IncrementBirdMeter;
  public static NormalEvent IncrementGoldMeter;
  public static NormalEvent FullBirdMeter;
  public static NormalEvent StopGoldRush;

  public static NormalEvent UpdateScore;

  public static GameObject coinPrefab;
  public static Transform coinParent;


  //main background music loop
  public static NormalEvent pauseBackground;
  public static NormalEvent unpauseBackground;

  //Powers
  public static float starfishPowerTime;
  public static int coinfishMulti;
  public static int treasureMulti;

  public static bool isMultiActive = false;
}
