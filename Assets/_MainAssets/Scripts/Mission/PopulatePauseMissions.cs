using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulatePauseMissions : MonoBehaviour
{
  [SerializeField] GameObject missionPrefab;

  private void Start()
  {
    foreach (Mission m in MissionManager.missions)
    {
      GameObject missionObject = Instantiate(missionPrefab, transform);
      missionObject.transform.GetComponent<PauseMission>().SetMission(m);
    }
  }
}
