using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDescription
{
  public int level, countNeeded, xpWorth;
  public string beginningString, endString;
  public bool isComplete;
  public MissionName missionName;

  public MissionDescription(MissionName missionName, int level, int countNeeded, int xpWorth, string beginningString, string endString)
  {
    this.missionName = missionName;
    isComplete = false;
    this.level = level;
    this.countNeeded = countNeeded;
    this.xpWorth = xpWorth;
    this.beginningString = beginningString;
    this.endString = endString;
  }
}
