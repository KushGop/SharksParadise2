using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.GameServices;

public class FirstAchievement : MonoBehaviour
{
  void Start()
  {
    if (API.IsLoggedIn())
    {
      if (!API.IsComplete(AchievementNames.SharksParadise))
      {
        API.SubmitAchievement(AchievementNames.SharksParadise);
      }
    }
  }

  public void DisplayLeaderboard()
  {
    API.ShowSpecificLeaderboard(LeaderboardNames.Highscores);
  }
}
