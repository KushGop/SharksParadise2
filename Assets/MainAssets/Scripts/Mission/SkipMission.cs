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
  [SerializeField] Image d1;
  [SerializeField] Image d2;
  [SerializeField] Image d3;
  [SerializeField] CanvasGroup group;
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
      /*
      text.text = "skip";
      switch (MissionManager.missions[index].coins)
      {
        case 100:
          d1.enabled = true;
          d2.enabled = false;
          d3.enabled = false;
          break;
        case 200:
          d1.enabled = false;
          d2.enabled = true;
          d3.enabled = true;
          break;
        case 300:
          d1.enabled = true;
          d2.enabled = true;
          d3.enabled = true;
          break;
      }
      //check if they have enough to purchase
      button.interactable = MissionManager.missions[index].coins / 100 <= GameManager.totalGems;
      */
      group.alpha = button.interactable ? 1 : 0.5f;
    }
    else
    {
      /*
      button.interactable = false;
      d1.enabled = false;
      d2.enabled = false;
      d3.enabled = false;
      */
      ad.enabled = false;
      text.text = "Play more to skip";
    }

  }

  public void SkipMissionGem()
  {
    button.interactable = false;
    int temp = GameManager.gems;
    GameManager.gems = 0;
    GameManager.gems -= MissionManager.missions[index].coins / 100;
    CompleteMethod(true);
    GameManager.gems = temp;
    UpgradesManager.updateCosts();
  }

  public void SkipMissionButton()
  {
    UpgradesManager.updateCosts();
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
