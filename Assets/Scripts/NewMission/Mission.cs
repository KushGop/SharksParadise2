using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Mission
{
  internal int indexInManager;
  internal int indexInMissionList;
  internal MissionName missionName;
  internal int level;
  internal int coins;
  private bool isComplete;
  private int count;
  private string text;

  public Mission(MissionName name, int lvl, int count, string txt)
  {
    missionName = name;
    level = lvl;
    isComplete = false;
    this.count = count;
    text = string.Format(txt, count);
  }

  public Mission(int iInML, MissionName name, int baseCount, string txt)
  {
    indexInMissionList = iInML;
    missionName = name;
    level = Random.Range(1, 4);
    isComplete = false;
    count = baseCount * level;
    Debug.Log(string.Format(txt, count));
    text = string.Format(txt, count);
  }


  public void SetMission(MissionName missionName, int level)
  {
    this.missionName = missionName;
    this.level = level;
    coins = level * 100;
    isComplete = false;
  }

  public void CompletMission()
  {
    isComplete = true;
  }

  public bool GetIsComplete() { return isComplete; }
  public string GetText() { return text; }
}
