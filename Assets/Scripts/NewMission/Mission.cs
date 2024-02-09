using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Mission
{
  public int indexInManager;
  public int indexInMissionList;
  public MissionName missionName;
  public int level;
  public int coins;
  public bool isComplete;
  public int count;
  public string text;

  public Mission(int iInML, MissionName name, int baseCount, string txt)
  {
    indexInMissionList = iInML;
    missionName = name;
    isComplete = false;
    count = baseCount;
    text = txt;
  }
  public Mission()
  {
    indexInMissionList = -1;
    missionName = MissionName.timesInked;
    isComplete = false;
    level = -1;
    count = -1;
    text = "NA";
  }


  public void SetMission(int iInML, int iInM, MissionName name, int baseCount, string txt)
  {
    level = Random.Range(1, 4);
    coins = level * 100;
    indexInMissionList = iInML;
    indexInManager = iInM;
    missionName = name;
    isComplete = false;
    count = baseCount * level;
    text = string.Format(txt, count);
    MissionData.MissionDelegate += MissionEvent;
  }

  private void MissionEvent(MissionName name, int currentCount)
  {
    if (name == missionName && currentCount == count && !isComplete)
      CompleteMission();
  }

  public void CompleteMission()
  {
    isComplete = true;
    MissionManager.MissionCompletionDelegate(text);
  }

}
