using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyList", menuName = "Sharks Paradise/EnemyList", order = 0)]
public class EnemyList : ScriptableObject
{
  readonly List<string> fullList = new()
  {
    "SmallShark",
    "BigShark",
    "GoldFish",
    "AngelFish",
    "Jellyfish",
    "Starfish",
    "Food",
    "Squid",
    "CoinFish",
    "LightFish",
    "BadLight"
  };

  readonly Dictionary<string, int> pointDictionary = new()
  {
    { "SmallShark", 100 },
    { "BigShark", 1000 },
    { "GoldFish", 10 },
    { "AngelFish", 15 },
    { "Jellyfish", 50 },
    { "Starfish", 0 },
    { "Food", 5 },
    { "Squid", 150 },
    { "CoinFish", 0 }
  };

  readonly public List<string> triggerAreaList = new()
  {
    "SmallShark",
    "BigShark",
    "GoldFish",
    "AngelFish",
    "Flub",
    "Eel",
    "MantaRay",
    "Squid",
    "CoinFish",
    "LightFish",
    "BadLight",
    "TutorialFish",
    "TutorialBird",
    "TutorialPredator",
  };

  readonly public List<string> playAreaList = new()
  {
    "Prey",
    "Predator",
    "Starfish"
  };

  readonly public List<string> spawnList = new()
  {
    "Treasure"
  };

  readonly public Dictionary<string, int> multiplyerCap = new()
  {
    { "SmallShark", 5 },
    { "GoldFish", 20 },
    { "AngelFish", 15 },
    { "Eel", 15 },
    { "MantaRay", 15 },
    { "Flub", 15 },
    { "Jelly", 8 },
    { "Bird", 8 },
    { "Starfish", 1 },
    { "SharkNet", 50 },
    { "Squid", 5 },
    { "CoinFish", 0 },
    { "BigShark", 3 },
    { "LightFish", 3 },
    { "BadLight", 5 },
    { "TutorialFish", 5 },
    { "TutorialBird", 5 },
    { "TutorialPredator", 1 },
  };

  //Fishname : scaleModifier  USED FOR SKELETON
  readonly public Dictionary<string, float> scaleModifier = new()
  {
    { "SmallShark", -1.5f },
    { "BigShark", -1.5f },
    { "GoldFish", 1f },
    { "AngelFish", 2f },
    { "CoinFish", 1f },
    { "LightFish", -3f },
    { "BadLight", -4f }
  };
}
