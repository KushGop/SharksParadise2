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
  SENSOR,
  BUBBLE
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
  TREASURE,
  BUBBLE
}
public enum Rarity
{
  COMMON,
  UNCOMMON,
  RARE,
  LEGENDARY,
  SPECIAL,
  OTHER
}

[System.Serializable]
public struct FishCard_S
{
  public int count;
  public int level;
  public Rarity rarity;

  public FishCard_S(Rarity r, int c, int l)
  {
    rarity = r;
    count = c;
    level = l;
  }
}
[System.Serializable]
public class AquariumData
{
  public SerializableDictionary<Fishes, FishCard_S> fishCards;

  public readonly Dictionary<Fishes, Rarity> fishRarity = new()
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
    //other
    { Fishes.BUBBLE, Rarity.OTHER },
  };

  public AquariumData()
  {
    fishCards = new();

    foreach (Fishes f in Enum.GetValues(typeof(Fishes)))
    {
      switch (fishRarity[f])
      {
        case Rarity.COMMON:
          fishCards.Add(f, new(Rarity.COMMON, 0, 0));
          break;
        case Rarity.UNCOMMON:
          fishCards.Add(f, new(Rarity.UNCOMMON, 0, 0));
          break;
        case Rarity.RARE:
          fishCards.Add(f, new(Rarity.RARE, 0, 0));
          break;
        case Rarity.SPECIAL:
          fishCards.Add(f, new(Rarity.SPECIAL, 0, 0));
          break;
      }
    }
  }
}
