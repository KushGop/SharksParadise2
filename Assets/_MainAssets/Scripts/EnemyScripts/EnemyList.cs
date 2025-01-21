using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyList", menuName = "Sharks Paradise/EnemyList", order = 0)]
public class EnemyList : ScriptableObject
{

  readonly public List<Fishes> triggerAreaList = new()
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

  readonly public List<FishType> playAreaList = new()
  {
    FishType.PREY,
    FishType.PREDATOR,
    FishType.STARFISH
  };

  readonly public List<FishType> spawnList = new()
  {
    FishType.TREASURE
  };

  readonly public Dictionary<Fishes, int> multiplyerCap = new()
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
  readonly public Dictionary<Fishes, float> scaleModifier = new()
  {
    { Fishes.SHARK_S, -1.5f },
    { Fishes.SHARK_L, -1.5f },
    { Fishes.CLOWN, 1f },
    { Fishes.ANGEL, 2f },
    { Fishes.COIN, 1f },
    { Fishes.CRAWLER, -3f },
    { Fishes.CUCUMBER, -4f }
  };
}

