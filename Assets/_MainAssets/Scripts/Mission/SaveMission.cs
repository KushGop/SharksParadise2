using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveMission : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
  }

  public void SaveData(GameData data)
  {
    for (int i = 0; i < 3; i++)
    {
      if (MissionManager.missions[i].isComplete)
      {
        data.missionList[i].isComplete = true;
      }
    }
  }
}
