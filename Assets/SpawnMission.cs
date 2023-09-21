using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMission : MonoBehaviour
{

  public MissionStats missionStats;
  public GameObject missionComplete;

  private void Start() {
    missionStats .OnCompletion += CompleteMission;
  }

  public void CompleteMission(object sender, string s){
    GameObject cm = Instantiate(missionComplete,transform);
    cm.transform.GetComponent<CompletedMission>().SetText(s);
  }
}
