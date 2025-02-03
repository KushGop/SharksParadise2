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
  [SerializeField] CanvasGroup group;
  [SerializeField] bool isAd;
  private int index;
  private int value;

  private void Start()
  {
    index = scoreMission.getMissionIndex();

    if (MissionManager.missions[index].gamesPlayed >= 3)
    {
      if (isAd)
      {
        button.interactable = API.IsRewardedVideoAvailable();
        text.text = "Watch ad";
      }
      else
      {
        value = (MissionManager.missions[index].coins / 100) * 5;
        text.text = string.Format("skip *{0} <sprite name=\"Gem\">", value);
        //check if they have enough to purchase
        button.interactable = value <= GameManager.totalGems;
      }
    }
    else
    {
      button.interactable = false;
      text.text = "Play more";
    }
    group.alpha = button.interactable ? 1 : 0.5f;
  }

  public void SkipMissionGem()
  {
    button.interactable = false;
    int temp = GameManager.gems;
    GameManager.gems = 0;
    GameManager.gems -= value;
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
