using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum FishType
{
  PREY,
  PREDATOR,
  STARFISH,
  FOOD,
  OBJECT,
  STUN,
  INK,
  COIN,
  TREASURE,
  SENSOR
}

public enum Fishes
{
  CLOWN,
  JELLY,
  MANTA,
  ANGEL,
  FLUB,
  SQUID,
  CRAWLER,
  SHARK_S,
  EEL,
  COIN,
  SEAGULL,
  CUCUMBER,
  SHARK_L,
  STARFISH,
  TREASURE
}
public enum Rarity
{
  COMMON,
  UNCOMMON,
  RARE,
  LEGENDARY,
  SPECIAL,
}

[System.Serializable]
public struct FishCard_S
{
  public int count;
  public int baseCount;
  public int multiplyer;
  public int value;
  public int increaseCost;
  public int upgradeCost;
  public Rarity rarity;

  public FishCard_S(Rarity r, int c, int b, int m, int v, int i, int u)
  {
    rarity = r;
    count = c;
    baseCount = b;
    multiplyer = m;
    value = v;
    increaseCost = i;
    upgradeCost = u;
  }
}
[System.Serializable]
public class AquariumData
{
  public SerializableDictionary<Fishes, FishCard_S> fishCards;

  private readonly Dictionary<Fishes, Rarity> fishRarity = new()
  {
    //common
    { Fishes.CLOWN, Rarity.COMMON },
    { Fishes.ANGEL, Rarity.COMMON },
    { Fishes.SEAGULL, Rarity.COMMON },
    { Fishes.SHARK_S, Rarity.COMMON },
    { Fishes.FLUB, Rarity.COMMON },
    { Fishes.EEL, Rarity.COMMON },
    { Fishes.MANTA, Rarity.COMMON },
    //uncommon
    { Fishes.JELLY, Rarity.UNCOMMON },
    { Fishes.CRAWLER, Rarity.UNCOMMON },
    //rare
    { Fishes.SHARK_L, Rarity.RARE },
    { Fishes.SQUID, Rarity.RARE },
    { Fishes.CUCUMBER, Rarity.RARE },
    //special
    { Fishes.COIN, Rarity.SPECIAL },
    { Fishes.STARFISH, Rarity.SPECIAL },
    { Fishes.TREASURE, Rarity.SPECIAL },
  };

  public AquariumData()
  {
    fishCards = new();

    foreach (Fishes f in Enum.GetValues(typeof(Fishes)))
    {
      switch (fishRarity[f])
      {
        case Rarity.COMMON:
          fishCards.Add(f, new(Rarity.COMMON, 0, 250, 1, 100, 100, 5));
          break;
        case Rarity.UNCOMMON:
          fishCards.Add(f, new(Rarity.UNCOMMON, 0, 50, 1, 250, 500, 10));
          break;
        case Rarity.RARE:
          fishCards.Add(f, new(Rarity.RARE, 0, 10, 1, 500, 1500, 15));
          break;
        case Rarity.SPECIAL:
          fishCards.Add(f, new(Rarity.SPECIAL, 0, 5, 1, 1000, 2500, 20));
          break;
      }
    }
  }
}
