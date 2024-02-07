using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionStats", menuName = "Sharks Paradise/MissionStats", order = 0)]
public class MissionStats : ScriptableObject
{
  public int timesInked, birdsEaten, timesStung, coinsCollected, coinFishesEaten, bigSharksEaten, starfishesCollected, fishnetsBroken, multiplyerMax, bigSharkDodges;
  private int numMissions;

  public GameObject missionComplete;
  public Transform missionParent;

  public event EventHandler<string> OnCompletion;

  public Dictionary<MissionName, int> missionDictionary = new()
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
  private readonly List<MissionDescription> missionList = new()
  {
    { new(MissionName.timesInked, 1, 2, "Get inked ", " times") }, //0
    { new(MissionName.timesInked, 2, 4, "Get inked ", " times") },
    { new(MissionName.birdsEaten, 1, 5, "Eat ", " birds") },
    { new(MissionName.birdsEaten, 2, 10, "Eat ", " birds") },
    { new(MissionName.timesStung, 1, 2, "Get stung ", " times") },
    { new(MissionName.timesStung, 2, 4, "Get stung ", " times") }, //5
    { new(MissionName.coinsCollected, 1, 25, "Collect ", " coins") },
    { new(MissionName.coinsCollected, 2, 50, "Collect ", " coins") },
    { new(MissionName.coinsCollected, 3, 100, "Collect ", " coins") },
    { new(MissionName.bigSharksEaten, 1, 3, "Eat ", " big sharks") },
    { new(MissionName.bigSharksEaten, 2, 6, "Eat ", " big sharks") }, //10
    { new(MissionName.starfishesCollected, 1, 1, "Collect ", " starfish") },
    { new(MissionName.starfishesCollected, 2, 2, "Collect ", " starfish") },
    //{ new(MissionName.fishnetsBroken, 1, 1, "Break open ", " fish net") },
    //{ new(MissionName.fishnetsBroken, 2, 2, "Break open ", " fish nets") },
    { new(MissionName.multiplyerMax, 1, 10, "Get a", " combo") }, //15
    { new(MissionName.multiplyerMax, 2, 15, "Get a", " combo") },
    { new(MissionName.multiplyerMax, 3, 20, "Get a", " combo") },
    //{ new(MissionName.bigSharkDodges, 1, 3, "Jump over ", " bigger sharks") },
    //{ new(MissionName.bigSharkDodges, 2, 5, "Jump over ", " bigger sharks") },
    //{ new(MissionName.bigSharkDodges, 3, 10, "Jump over ", " bigger sharks") }, //20
  };

  public Dictionary<MissionName, MissionBehavior> missions = new();

  public MissionStats()
  {
    missions.Add(missionList.ElementAt(0).missionName, new(missionList.ElementAt(0)));
    missions.Add(missionList.ElementAt(2).missionName, new(missionList.ElementAt(2)));
    missions.Add(missionList.ElementAt(6).missionName, new(missionList.ElementAt(6)));
  }

  private MissionBehavior newMission;

  private void Start()
  {
    numMissions = missionList.Count;
  }

  public void IncrementMission(MissionName name)
  {
    missionDictionary[name]++;
    if (missions.ContainsKey(name))
    {
      UpdateMissions(name);
    }
  }
  public void UpdateMissions(MissionName name)
  {
    string mString = missions[name].CheckCount(missionDictionary[name]);
    if (mString != null)
    {
      OnCompletion(this, mString);
    }
  }

  private MissionBehavior NewMission()
  {
    int randMission = UnityEngine.Random.Range(0, numMissions);
    newMission = new MissionBehavior(missionList.ElementAt(randMission));
    return newMission;
  }
  public void ResetAllMissions()
  {
    foreach (MissionName name in missionDictionary.Keys)
    {
      missionDictionary[name] = 0;
    }
  }
  public void ResetMission(MissionName missionName)
  {
    missionDictionary[missionName] = 0;
  }

}
