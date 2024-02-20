using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLoader : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
    DataPersistenceManager.instance.SaveGame();

    print("LoadMission");

    for (int i = 0; i < 3; i++)
    {
      MissionManager.missions[i] = data.missionList[i];
      print("data mission: " + data.missionList[i].text);
      print("manager mission: " + MissionManager.missions[i].text);
    }
    MissionManager.SubscribeToCompletion();
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
        data.missionList[i].SetMission(m.isComplete, m.indexInMissionList, i, m.missionName, m.count, m.text);
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
