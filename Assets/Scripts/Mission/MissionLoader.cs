using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLoader : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
    if (!GameManager.hasBeenInit)
    {
      DataPersistenceManager.instance.SaveGame();

      print("LoadMission");

      for (int i = 0; i < 3; i++)
      {
        MissionManager.missions[i] = data.missionList[i];
        print("mission: " + data.missionList[i].text);
      }
      MissionManager.SubscribeToCompletion();
      GameManager.hasBeenInit = true;
    }
  }

  public void SaveData(GameData data)
  {
    for (int i = 0; i < 3; i++)
    {
      if (data.missionList[i].level == -1)
      {
        print("empty");
        Mission m = MissionData.GetRandomMission();
        m = CheckForDuplicates(m, ref data.missionListName);
        data.missionList[i].SetMission(m.isComplete, m.indexInMissionList, i, m.missionName, m.count, m.text, m.gamesPlayed);
      }
    }

  }

  private Mission CheckForDuplicates(Mission mission, ref List<MissionName> mln)
  {
    while (true)
    {
      if (mln.Contains(mission.missionName))
      {
        mission = MissionData.GetRandomMission();
      }
      else
      {
        mln.Add(mission.missionName);
        return mission;
      }
    }
  }


}
