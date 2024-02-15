using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateScoreMissions : MonoBehaviour, IDataPersistence
{
  [SerializeField] private GameObject missionPrefab;
  [SerializeField] public GameObject coinTarget;
  private GameObject missionItem;

  public void LoadData(GameData data)
  {
    for (int i = 0; i < 3; i++)
    {
      missionItem = Instantiate(missionPrefab, transform);
      missionItem.GetComponent<ScoreMission>().SetMission(MissionManager.GetMission(i));
    }
  }

  public void SaveData(GameData data)
  {
  }

}
