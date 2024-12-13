using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.GameServices;

public class FirstAchievement : MonoBehaviour
{
  void Start()
  {
    if (!API.IsComplete(AchievementNames.SharksParadise) && API.IsLoggedIn())
    {
      API.SubmitAchievement(AchievementNames.SharksParadise);
    }
    API.GetPlayerScore(LeaderboardNames.Highscores, e =>
    {
      if (GameManager.highscore > e)
      {
        API.SubmitScore(GameManager.highscore, LeaderboardNames.Highscores);
      }
    });
  }

  public void DisplayLeaderboard()
  {
    API.ShowSpecificLeaderboard(LeaderboardNames.Highscores);
  }
}
