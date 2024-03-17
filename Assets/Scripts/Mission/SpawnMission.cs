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
    StartCoroutine(StartSequence());
  }

  private IEnumerator StartSequence()
  {
    yield return new WaitForSecondsRealtime(1f);
    for (int i = 0; i < 3; i++)
    {
      if (!MissionManager.missions[i].isComplete)
      {
        GameObject cm = Instantiate(missionComplete, transform);
        cm.transform.GetComponent<CompletedMissionDropDrown>().SetTextAndState(MissionManager.missions[i].text, false);
        yield return new WaitForSecondsRealtime(2f);
      }
    }
  }

  public void CompleteMission(Mission m)
  {
    GameObject cm = Instantiate(missionComplete, transform);
    DataPersistenceManager.instance.SaveGame();
    cm.transform.GetComponent<CompletedMissionDropDrown>().SetTextAndState(m.text, true);
  }
}
