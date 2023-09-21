using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission : MonoBehaviour
{
  private readonly int xpWorth;
  private bool isComplete;

  private readonly MissionName missionName;
  private readonly int countNeeded;
  private string missionString;

  public TextMeshProUGUI text;

  private void Start()
  {
    text.text = missionString;
  }

  public void SetMissionString(string s){
    missionString = s;
  }
}
