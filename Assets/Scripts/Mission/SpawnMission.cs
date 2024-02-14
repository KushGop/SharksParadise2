using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMission : MonoBehaviour
{
  public GameObject missionComplete;

  private void Start()
  {
    MissionManager.MissionCompletionDelegate += CompleteMission;
  }

  public void CompleteMission(Mission m)
  {
    GameObject cm = Instantiate(missionComplete, transform);
    cm.transform.GetComponent<CompletedMissionDropDrown>().SetText(m.text);
  }
}
