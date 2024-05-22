using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gley.MobileAds;

public class SkipMission : MonoBehaviour
{
  [SerializeField] ScoreMission scoreMission;
  [SerializeField] TextMeshProUGUI text;
  [SerializeField] Button button;
  [SerializeField] Image ad;
  private int index;

  private void Start()
  {
    index = scoreMission.getMissionIndex();

    if (MissionManager.missions[index].gamesPlayed >= 3)
    {
      if (API.IsRewardedVideoAvailable())
      {
        button.interactable = true;
        ad.enabled = true;
        text.text = "Watch ad to skip";
      }
      else
      {
        button.interactable = false;
        text.text = "Ad not available";
      }
    }
    else
    {
      button.interactable = false;
      ad.enabled = false;
      text.text = "Play more to skip";
    }
  }

  public void SkipMissionButton()
  {
    API.ShowRewardedVideo(CompleteMethod);
  }
  private void CompleteMethod(bool completed)
  {
    if (completed)
    {
      MissionManager.missions[index].isComplete = true;
      DataPersistenceManager.instance.LoadGame();
    }
  }
}
