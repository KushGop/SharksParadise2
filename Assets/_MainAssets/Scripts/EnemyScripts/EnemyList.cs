using UnityEngine;
using System.Collections.Generic;

public static class EnemyList
{

  readonly static public List<Fishes> triggerAreaList = new()
  {
    Fishes.SHARK_L,
    Fishes.SHARK_S,
    Fishes.CLOWN,
    Fishes.ANGEL,
    Fishes.FLUB,
    Fishes.EEL,
    Fishes.MANTA,
    Fishes.SQUID,
    Fishes.COIN,
    Fishes.CRAWLER,
    Fishes.CUCUMBER
  };

  readonly static public List<FishType> playAreaList = new()
  {
    FishType.PREY,
    FishType.PREDATOR,
    FishType.STARFISH
  };

  readonly static public List<FishType> spawnList = new()
  {
    FishType.TREASURE
  };

  readonly static public Dictionary<Fishes, int> multiplyerCap = new()
  {
    { Fishes.SHARK_S, 5 },
    { Fishes.CLOWN, 20 },
    { Fishes.ANGEL, 15 },
    { Fishes.EEL, 15 },
    { Fishes.MANTA, 15 },
    { Fishes.FLUB, 15 },
    { Fishes.JELLY, 8 },
    { Fishes.SEAGULL, 8 },
    { Fishes.STARFISH, 1 },
    //{ "SharkNet", 50 },
    { Fishes.SQUID, 5 },
    { Fishes.COIN, 0 },
    { Fishes.SHARK_L, 3 },
    { Fishes.CRAWLER, 3 },
    { Fishes.CUCUMBER, 5 },
  };

  //Fishname : scaleModifier  USED FOR SKELETON
  readonly static public Dictionary<Fishes, float> scaleModifier = new()
  {
    { Fishes.SHARK_S, -1.5f },
    { Fishes.SHARK_L, -1.5f },
    { Fishes.CLOWN, 1f },
    { Fishes.ANGEL, 2f },
    { Fishes.COIN, 1f },
    { Fishes.CRAWLER, -3f },
    { Fishes.CUCUMBER, -4f }
  };


  public static readonly Dictionary<Fishes, int[]> specialFish = new()
  {
    //Coin vale
    { Fishes.COIN, new int[] { 1, 5, 10, 25, 50 } },
    //Multiplier
    { Fishes.TREASURE, new int[] { 1, 2, 5, 10, 15 } },
    //Duration
    { Fishes.STARFISH, new int[] { 5, 10, 15, 20, 25 } },
  };
  public static readonly Dictionary<Rarity, int[]> rarityPoint = new()
  {
    { Rarity.COMMON, new int[] { 100, 150, 250, 400, 600 } },
    { Rarity.UNCOMMON, new int[] { 250, 350, 500, 700, 950 } },
    { Rarity.RARE, new int[] { 500, 900, 1500, 2200, 3000 } },
    { Rarity.SPECIAL, new int[] { 0, 0, 0, 0, 0 } },
  };
  public static readonly Dictionary<Rarity, int[]> rarityGemCost = new()
  {
    { Rarity.COMMON, new int[] { 5, 10, 15, 20, 25 } },
    { Rarity.UNCOMMON, new int[] { 25, 75, 150, 200, 250 } },
    { Rarity.RARE, new int[] { 50, 100, 150, 250, 350 } },
    { Rarity.SPECIAL, new int[] { 100, 150, 250, 350, 500 } },
  };
  public static readonly Dictionary<Rarity, int> rarityCoinCost = new()
  {
    { Rarity.COMMON, 100 },
    { Rarity.UNCOMMON, 250 },
    { Rarity.RARE, 1000 },
    { Rarity.SPECIAL, 2500 },
  };
  public static readonly Dictionary<Rarity, int[]> rarityCount = new()
  {
    { Rarity.COMMON, new int[] { 50, 250, 500, 1000, 2500, 5000 } },
    { Rarity.UNCOMMON, new int[] { 100, 250, 500, 1000, 2500 } },
    { Rarity.RARE, new int[] { 50, 150, 250, 500, 1000 } },
    { Rarity.SPECIAL, new int[] { 25, 100, 250, 500, 1000 } },
  };
}

