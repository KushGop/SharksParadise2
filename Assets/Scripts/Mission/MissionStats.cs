using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionStats", menuName = "Sharks Paradise/MissionStats", order = 0)]
public class MissionStats : ScriptableObject
{
  public int timesInked, birdsEaten, timesStung, coinsCollected, coinFishesEaten, bigSharksEaten, starfishesCollected, fishnetsBroken, multiplyerMax, bigSharkDodges;
  private int numMissions;

  public Dictionary<MissionName, int> missionDictionary = new(){
      {MissionName.timesInked, 0},
      {MissionName.birdsEaten,0},
      {MissionName.timesStung,0},
      {MissionName.coinsCollected,0},
      {MissionName.coinFishesEaten,0},
      {MissionName.bigSharksEaten,0},
      {MissionName.starfishesCollected,0},
      {MissionName.fishnetsBroken,0},
      {MissionName.multiplyerMax,0},
      {MissionName.bigSharkDodges,0}
  };
  private readonly List<MissionDescription> missionList = new(){
    {new(MissionName.timesInked,0,1,100,"Get inked "," time")}, //0
    {new(MissionName.timesInked,1,3,500,"Get inked "," times")},
    {new(MissionName.birdsEaten,0,5,100,"Eat "," birds")},
    {new(MissionName.birdsEaten,1,10,500,"Eat "," birds")},
    {new(MissionName.timesStung,0,1,500,"Get stung "," time")},
    {new(MissionName.timesStung,1,3,500,"Get stung "," times")}, //5
    {new(MissionName.coinsCollected,0,25,500,"Collect "," coins")},
    {new(MissionName.coinsCollected,1,50,500,"Collect "," coins")},
    {new(MissionName.coinsCollected,2,100,500,"Collect "," coins")},
    {new(MissionName.bigSharksEaten,0,3,500,"Eat "," big sharks")},
    {new(MissionName.bigSharksEaten,1,6,500,"Eat "," big sharks")}, //10
    {new(MissionName.starfishesCollected,0,1,500,"Collect "," starfish")},
    {new(MissionName.starfishesCollected,1,2,500,"Collect "," starfish")},
    {new(MissionName.fishnetsBroken,0,1,500,"Break open "," fish net")},
    {new(MissionName.fishnetsBroken,1,2,500,"Break open "," fish nets")},
    {new(MissionName.multiplyerMax,0,10,500,"Get "," of the same fish in a row")}, //15
    {new(MissionName.multiplyerMax,1,15,500,"Get "," of the same fish in a row")},
    {new(MissionName.multiplyerMax,2,20,500,"Get "," of the same fish in a row")},
    {new(MissionName.bigSharkDodges,0,3,500,"Jump over "," bigger sharks")},
    {new(MissionName.bigSharkDodges,1,5,500,"Jump over "," bigger sharks")},
    {new(MissionName.bigSharkDodges,2,10,500,"Jump over "," bigger sharks")}, //20
  };

  // public Dictionary<KeyValuePair<MissionName, int[,]>, int> missions = new();
  public Dictionary<MissionName, MissionBehavior> missions = new();

  /*
  I want a game object in the score scene to hold 3 mission prefabs
  I want MissionStats to hold the three missions
  */

  public MissionStats()
  {
    missions.Add(MissionName.timesInked, new(missionList.ElementAt(0)));
    missions.Add(MissionName.birdsEaten, new(missionList.ElementAt(2)));
    missions.Add(MissionName.coinsCollected, new(missionList.ElementAt(6)));

    // missions.Add(new(missionDictionary.ElementAt(0), 0));
    // missions.Add(new(missionDictionary.ElementAt(1), 0));
    // missions.Add(new(missionDictionary.ElementAt(3), 0));

    // missions.Add(missionDictionary.ElementAt(0), 0);
    // missions.Add(missionDictionary.ElementAt(1), 0);
    // missions.Add(missionDictionary.ElementAt(3), 0);
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
    missions[name].CheckCount(missionDictionary[name]);
  }

  private MissionBehavior NewMission()
  {
    int randMission = Random.Range(0, numMissions);
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


  public int getVariableToInt(string name)
  {
    return (int)GetType().GetField(name).GetValue(this);
  }
  public void setVariable(string name, int value)
  {
    GetType().GetField(name).SetValue(this, value);
  }


}
