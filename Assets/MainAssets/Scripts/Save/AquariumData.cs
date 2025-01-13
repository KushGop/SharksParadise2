using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

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
  STARFISH
}
public enum Rarity
{
  COMMON,
  UNCOMMON,
  RARE,
  LEGENDARY,
  SPECIAL,
}

public struct FishCard_S
{
  public Fishes fish;
  public int count;
  public int baseCount;
  public int multiplyer;
  public int value;
  public Rarity rarity;

  public FishCard_S(Fishes f, Rarity r, int c, int b, int m, int v)
  {
    rarity = r;
    fish = f;
    count = c;
    baseCount = b;
    multiplyer = m;
    value = v;
  }
}

public class AquariumData
{
  public List<FishCard_S> fishCards;

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
  };

  public AquariumData()
  {
    fishCards = new();

    foreach (Fishes f in Enum.GetValues(typeof(Fishes)))
    {
      switch (fishRarity[f])
      {
        case Rarity.COMMON:
          fishCards.Add(new(f, Rarity.COMMON, 0, 250, 1, 100));
          break;
        case Rarity.UNCOMMON:
          fishCards.Add(new(f, Rarity.UNCOMMON, 0, 50, 1, 250));
          break;
        case Rarity.RARE:
          fishCards.Add(new(f, Rarity.RARE, 0, 10, 1, 500));
          break;
        case Rarity.SPECIAL:
          fishCards.Add(new(f, Rarity.SPECIAL, 0, 5, 1, 1000));
          break;
      }
    }
  }
}
