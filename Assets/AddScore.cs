using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gley.GameServices;

public class AddScore : MonoBehaviour
{
  public TextMeshProUGUI text;
  public void IncreaseScore()
  {
    text.text = (int.Parse(text.text) + 100).ToString();
  }

  public void AddScoreToLeaderboard()
  {
    API.SubmitScore(int.Parse(text.text), LeaderboardNames.HighscoreTest);
  }
}
