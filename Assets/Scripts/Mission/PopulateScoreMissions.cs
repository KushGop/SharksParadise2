using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateScoreMissions : MonoBehaviour, IDataPersistence
{
  [SerializeField] private GameObject missionPrefab;
  public GameObject coinTarget;
  private GameObject missionItem;
  private Mission newMission;
  private List<MissionName> names;

  public void LoadData(GameData data)
  {
    if (transform.childCount > 0)
    {
      Destroy(transform.GetChild(0).gameObject);
      Destroy(transform.GetChild(1).gameObject);
      Destroy(transform.GetChild(2).gameObject);
    }
    names = data.missionListName;
    GameManager.totalCoins = GameManager.coins + data.totalCoins;
    GameManager.totalGems = GameManager.gems + data.totalGems;
    for (int i = 0; i < 3; i++)
    {
      missionItem = Instantiate(missionPrefab, transform);
      missionItem.GetComponent<ScoreMission>().SetMission(MissionManager.GetMission(i));
      if (MissionManager.GetMission(i).isComplete)
      {
        //for skipped missions
        if (!data.missionList[i].isComplete)
        {
          data.missionList[i].isComplete = true;
        }
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
    data.totalGems = GameManager.totalGems;
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
