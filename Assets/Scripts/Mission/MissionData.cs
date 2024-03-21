using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MissionData
{
  public static readonly int missionCount = 7;
  public delegate void MissionEvent(MissionName name, int count);
  public static MissionEvent MissionDelegate;

  public static Dictionary<MissionName, int> missionDictionary = new()
  {
    { MissionName.timesInked, 0 },
    { MissionName.birdsEaten, 0 },
    { MissionName.timesStung, 0 },
    { MissionName.coinsCollected, 0 },
    { MissionName.treasureCollected, 0 },
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
    { new(3, MissionName.coinsCollected, 15, "Collect {0} coins") },
    { new(4, MissionName.bigSharksEaten, 2, "Eat {0} big sharks") },
    { new(5, MissionName.starfishesCollected, 1, "Collect {0} starfish") },
    { new(6, MissionName.multiplyerMax, 4, "Get a {0}* combo") },
    { new(7, MissionName.treasureCollected, 1, "Collect {0} treasure chests") },
  };

  public static Mission GetRandomMission()
  {
    return allMissions[Random.Range(0, allMissions.Count)];
  }

  public static void IncrementMission(MissionName name)
  {
    missionDictionary[name]++;
    //Debug.Log("Mission Name:" + name + "  Count: " + missionDictionary[name]);
    if (missionDictionary.ContainsKey(name))
    {
      MissionDelegate(name, missionDictionary[name]);
    }
  }


}
