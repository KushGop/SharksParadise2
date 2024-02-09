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

  public void CompleteMission(string s)
  {
    GameObject cm = Instantiate(missionComplete, transform);
    cm.transform.GetComponent<CompletedMission>().SetText(s);
  }
}
