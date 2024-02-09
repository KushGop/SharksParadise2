using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Holds all missions
//Missions are of type Mission
public static class MissionManager
{

  public static Mission[] missions = new Mission[3];
  public delegate void UpdateCoins(int coins);
  public static UpdateCoins AddCoins;


  public static void AddMission(Mission mission)
  {
    for (int i = 0; i < 3; i++)
    {
      if (missions[i] == null)
      {
        missions[i] = mission;
        mission.indexInManager = i;
        return;
      }
    }
    Debug.Log("mission list full");
  }

  public static void UpdateMissionStatus()
  {
    for (int i = 0; i < 3; i++)
    {
      if (missions[i].GetIsComplete())
      {
        AddCoins(missions[i].coins);
        missions[i] = null;
      }
    }
  }

  public static void IncrementMission(MissionName name)
  {
    MissionData.missionDictionary[name]++;
    if (MissionData.missionDictionary.ContainsKey(name))
    {
      MissionData.MissionDelegate(name, MissionData.missionDictionary[name]);
    }
  }

  public static void ResetMission(MissionName missionName)
  {
    MissionData.missionDictionary[missionName] = 0;
  }


  public static Mission GetMission(int index)
  {
    return missions[index];
  }

  public static Mission[] GetMissions()
  {
    return missions;
  }

  public static bool HasEmptyMission()
  {
    foreach (Mission m in missions)
    {
      if (m == null)
      {
        return true;
      }
    }
    return false;
  }
}
