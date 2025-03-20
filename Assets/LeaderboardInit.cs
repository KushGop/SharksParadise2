using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using GooglePlayGames.Android;
using Gley.GameServices;
using TMPro;
using System;

public class LeaderboardInit : MonoBehaviour
{
  [Header("UI")]
  [SerializeField] GameObject leaderboard1;
  [SerializeField] GameObject login;
  [SerializeField] GameObject loading;
  [Space]
  [Header("Leaderboards")]
  [SerializeField] PopulateHighscores leaderboardScript;
  [SerializeField] CanvasGroup leaderboardsCanvasGroup;
  [Header("Login")]
  [SerializeField] TextMeshProUGUI loginText;

  private void Start()
  {
    SetUpUI();
  }

  public void SetUpUI()
  {
    leaderboard1.SetActive(true);
    leaderboardsCanvasGroup.alpha = 0;
    login.SetActive(false);
    loading.SetActive(false);

    if (API.IsLoggedIn())
    {
      LoadLeaderboard();
    }
    else
    {
      login.SetActive(true);
    }
  }

  public void Login()
  {
    API.LogIn((b) =>
    {
      if (b)
        LoadLeaderboard();
      else
        loginText.text = "Log in failed";
    });
  }

  private void LoadLeaderboard()
  {
    loading.SetActive(true);
    leaderboardScript.LoadLeaderboard();
  }

  internal void ShowLeaderboard()
  {
    loading.SetActive(false);
    leaderboardsCanvasGroup.alpha = 1;
  }
}
