using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateScoreMissions : MonoBehaviour
{
  [SerializeField] private GameObject missionPrefab;
  private GameObject missionItem;
  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < 3; i++)
    {
      missionItem = Instantiate(missionPrefab, transform);
      missionItem.GetComponent<ScoreMission>().SetMission(MissionManager.GetMission(i));
    }
  }
}
