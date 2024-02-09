using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBehavior
{
  /*
  this needs to be a certain mission
  how much each mission is worth
  when game starts, pop up current missions
  during game, update stats
  when you update stats, current missions get called to check if mission has been completed
  mission should hold: isComplete, xpWorth, missionName, (nesscesary stat) 
  when mission is called, check nesscesary stat, update isComplete if valid
  who calls this
  most missions are based on player collider (eatingHandler)
  eatingHandler will have to be hardcoded by linking each event with a certain mission stat 
  if the mission is in the mission list, then increase variable in missionStats and call checkCount
  there needs to be a holder with all current missions
  holder will be called first by eatingHandler to see if mission is available
  if mission is available, call checkCount
  */

  private readonly int coins;
  private bool isComplete;


  private readonly MissionName missionName;
  private readonly int countNeeded;
  private readonly string missionString;

  public MissionBehavior(MissionDescription missionDescription)
  {
    missionName = missionDescription.missionName;
    countNeeded = missionDescription.countNeeded;
    coins = missionDescription.coins;
    isComplete = missionDescription.isComplete;
    missionString = missionDescription.beginningString + countNeeded.ToString() + missionDescription.endString;
  }

  // public void MissionInit()
  // {
  //   missionStats.setVariable(missionString, 0);
  //   isComplete = false;
  // }

  public string CheckCount(int currentCount)
  {
    if (currentCount == countNeeded)
    {
      isComplete = true;
      return missionString;
    }
    return null;
  }
  public string GetMissionString()
  {
    return missionString;
  }

}

// public MissionBehavior(KeyValuePair<MissionName, int[,]> dicionaryElement, int level)
// {
//   missionName = dicionaryElement.Key;
//   countNeeded = dicionaryElement.Value[0, level];
//   xpWorth = dicionaryElement.Value[1, level];
//   missionString = missionName.ToString();
// }
// public MissionBehavior(KeyValuePair<MissionName, MissionDescription> dictionaryElement)
// {
//   missionName = dictionaryElement.Key;
//   countNeeded = dictionaryElement.Value.countNeeded;
//   xpWorth = dictionaryElement.Value.xpWorth;
//   missionString = dictionaryElement.Value.beginningString + countNeeded.ToString() + dictionaryElement.Value.endString;
// }