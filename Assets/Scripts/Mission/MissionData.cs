using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MissionData
{
  public static readonly int missionCount = 7;

  public static Dictionary<MissionName, int> missionDictionary = new()
  {
    { MissionName.timesInked, 0 },
    { MissionName.birdsEaten, 0 },
    { MissionName.timesStung, 0 },
    { MissionName.coinsCollected, 0 },
    { MissionName.coinFishesEaten, 0 },
    { MissionName.bigSharksEaten, 0 },
    { MissionName.starfishesCollected, 0 },
    { MissionName.fishnetsBroken, 0 },
    { MissionName.multiplyerMax, 0 },
    { MissionName.bigSharkDodges, 0 }
  };

  public static readonly List<Mission> allMissions = new()
  {
    { new(0, MissionName.timesInked, 2, "Get inked {0} times") },
    { new(1, MissionName.birdsEaten, 5, "Eat {0} birds") },
    { new(2, MissionName.timesStung, 2, "Get stung {0} times") },
    { new(3, MissionName.coinsCollected, 25, "Collect {0} coins") },
    { new(4, MissionName.bigSharksEaten, 3, "Eat {0} big sharks") },
    { new(5, MissionName.starfishesCollected, 1, "Collect {0} starfish") },
    { new(6, MissionName.multiplyerMax, 5, "Get a {0} combo") },
  };

  public static Mission GetRandomMission()
  {
    return allMissions[Random.Range(0, allMissions.Count)];
  }




}
