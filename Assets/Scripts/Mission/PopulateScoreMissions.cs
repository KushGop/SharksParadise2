using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateScoreMissions : MonoBehaviour, IDataPersistence
{
  [SerializeField] private GameObject missionPrefab;
  [SerializeField] public GameObject coinTarget;
  private GameObject missionItem;
  private Mission newMission;
  private List<MissionName> names;

  public void LoadData(GameData data)
  {
    names = data.missionListName;
    GameManager.totalCoins = GameManager.coins + data.totalCoins;
    for (int i = 0; i < 3; i++)
    {
      missionItem = Instantiate(missionPrefab, transform);
      missionItem.GetComponent<ScoreMission>().SetMission(MissionManager.GetMission(i));
      if (MissionManager.GetMission(i).isComplete)
      {
        MissionManager.missions[i].isClaimed = true;
        while (true)
        {
          newMission = MissionData.GetRandomMission();
          if (names.Contains(newMission.missionName))
          {
            newMission = MissionData.GetRandomMission();
          }
          else
          {
            names.Remove(MissionManager.GetMission(i).missionName);
            MissionManager.missions[i].SetMission(newMission.isComplete, newMission.indexInMissionList, i, newMission.missionName, newMission.count, newMission.text);
            names.Add(newMission.missionName);
            break;
          }
        }

      }
    }
    DataPersistenceManager.instance.SaveGame();
  }

  public void SaveData(GameData data)
  {
    for (int i = 0; i < 3; i++)
    {
      if (data.missionList[i].isComplete)
      {
        Debug.Log("Total: " + data.totalCoins);
        data.totalCoins += data.missionList[i].coins;
        Debug.Log("Total after: " + data.totalCoins);
        //GameManager.totalCoins = data.totalCoins;
        data.missionListName.Remove(data.missionList[i].missionName);
        data.missionList[i] = MissionManager.missions[i];
        data.missionListName.Add(MissionManager.missions[i].missionName);
      }
    }
    data.totalCoins += GameManager.coins;
    if (data.highscore < GameManager.score)
    {
      print("HIGHSCORE");
      data.highscore = GameManager.score;
    }
  }

}
