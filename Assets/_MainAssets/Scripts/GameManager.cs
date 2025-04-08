using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
  //Player States
  public static bool isAlive = true;
  public static bool isMultiActive = false;
  public static bool isInvincible = false;

  //Game States
  public static bool playedTutorial;
  public static bool hasBeenInit = false;
  public static bool isNight = false;
  public static bool isGoldRush = false;

  //Init
  public static bool missionFirstPass;
  public static int score, highscore, xp, coins, gems, totalCoins, totalXP, totalGems, totalTokens, missionCoins;
  public static int multiplyer, multiplyerCap, starfishMulti;
  public static Transform position;
  public static Mission[] missionList = new Mission[3];
  public static readonly float dayCycleTime = 40f;
  public static int day = 1;
  public static int adCount;
  public static Fishes lastFish;

  //Currently usesless
  public static int control; // 0 = right, 1 = left
  public static List<int> dodgeHelper = new();

  //Power
  public delegate void Power(string text, float waitTime);
  public static Power eventText;
  public static Dictionary<int, string> powers = new()
  {
    { -1, "Second Chance" },
    { 0, "Shield" },
    { 1, "Double Points" },
    { 2, "Speed Boost" }
  };

  //Events
  public delegate void NormalEvent();
  public delegate void BoolEvent(bool b);
  public static NormalEvent fishEaten;
  public static NormalEvent switchToNight;
  public static NormalEvent switchToDay;
  public static BoolEvent ToogleCoins;
  public static NormalEvent disappear;
  public static NormalEvent pause;
  public static NormalEvent unpause;
  public static NormalEvent UpdateScore;

  //Power Events
  public static NormalEvent IncrementBirdMeter;
  public static NormalEvent IncrementGoldMeter;
  public static NormalEvent FullBirdMeter;
  public static NormalEvent StopGoldRush;
  public static BoolEvent Invincible;


  public static GameObject coinPrefab;
  public static Transform coinParent;


  //Main background music loop
  public static NormalEvent pauseBackground;
  public static NormalEvent unpauseBackground;

  //Powers
  public static float starfishPowerTime;
  public static int coinfishMulti;
  public static int treasureMulti;

}
