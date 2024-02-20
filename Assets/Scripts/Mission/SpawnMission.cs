using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMission : MonoBehaviour
{
  public GameObject missionComplete;
  private Mission mission;

  private void Start()
  {
    MissionManager.MissionCompletionDelegate += CompleteMission;
  }

  public void CompleteMission(Mission m)
  {
    mission = m;
    GameObject cm = Instantiate(missionComplete, transform);
    DataPersistenceManager.instance.SaveGame();
    cm.transform.GetComponent<CompletedMissionDropDrown>().SetText(m.text);
  }
}
