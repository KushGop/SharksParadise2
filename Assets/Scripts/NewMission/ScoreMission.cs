using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMission : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI skipOrClaimText;
  [SerializeField] private TextMeshProUGUI missionText;
  private Mission mission;
  private int coins;

  public void SetMission(Mission mission)
  {
    this.mission = mission;

    missionText.text = this.mission.text;
    //TODO: Add button
    //SetSkipOrClaimButton();
  }

  private void SetSkipOrClaimButton()
  {
    if (mission.isComplete)
    {
      //claim coins
      coins = mission.coins;
      skipOrClaimText.text = "Claim " + coins.ToString();
      //update total coins
    }
    else
    {
      //skip with ad
      skipOrClaimText.text = "Skip";
    }
  }
}
