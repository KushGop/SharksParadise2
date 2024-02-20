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
  public bool isClaimed;
  public int count;
  public string text;

  public Mission(int iInML, MissionName name, int baseCount, string txt)
  {
    indexInMissionList = iInML;
    missionName = name;
    isComplete = false;
    count = baseCount;
    text = txt;
    isClaimed = false;
  }
  public Mission()
  {
    indexInMissionList = -1;
    missionName = MissionName.noMission;
    isComplete = false;
    isClaimed = false;
    level = -1;
    count = -1;
    text = "NA";
    MissionData.MissionDelegate += CompleteMission;
  }


  public void SetMission(bool isComplete, int iInML, int iInM, MissionName name, int baseCount, string txt)
  {
    level = Random.Range(1, 4);
    coins = level * 100;
    indexInMissionList = iInML;
    indexInManager = iInM;
    missionName = name;
    isClaimed = false;
    this.isComplete = isComplete;
    count = baseCount * level;
    text = string.Format(txt, count);
    if (isComplete)
    {
      MissionData.MissionDelegate -= CompleteMission;
    }
  }

  public void CompleteMission(MissionName name, int count)
  {
    if (name == missionName && count == this.count && !isComplete)
    {
      isComplete = true;
      MissionManager.MissionCompletionDelegate(this);
      MissionData.MissionDelegate -= CompleteMission;
    }
  }

}
