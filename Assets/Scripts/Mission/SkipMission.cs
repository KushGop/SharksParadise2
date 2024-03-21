using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.MobileAds;

public class SkipMission : MonoBehaviour
{
  [SerializeField] ScoreMission scoreMission;
  private int index;

  private void Start()
  {
    index = scoreMission.getMissionIndex();
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
