using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using GooglePlayGames.Android;
using Gley.GameServices;
using TMPro;

public class LeaderboardInit : MonoBehaviour
{
  [Header("UI")]
  [SerializeField] GameObject leaderboard1;
  [SerializeField] GameObject leaderboard2;
  [SerializeField] GameObject login;
  [SerializeField] GameObject loading;
  [Space]
  [Header("Leaderboards")]
  [SerializeField] PopulateLeaderboard topThree;
  [SerializeField] PopulateLeaderboard playerLeaderboard;
  [SerializeField] CanvasGroup leaderboardsCanvasGroup;
  [Header("Login")]
  [SerializeField] TextMeshProUGUI loginText;

  private bool loaded1 = false, loaded2 = false;

  private void Start()
  {
    SetUpUI();
  }

  public void SetUpUI()
  {
    leaderboard1.SetActive(true);
    leaderboard2.SetActive(true);
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

  public void DoneLoading(bool isTopThree)
  {
    if (isTopThree)
      loaded1 = true;
    else
      loaded2 = true;
    if (loaded1 && loaded2)
      ShowLeaderboard();
  }
  private void LoadLeaderboard()
  {
    loading.SetActive(true);
    topThree.LoadLeaderBoard();
    playerLeaderboard.LoadLeaderBoard();
  }

  private void ShowLeaderboard()
  {
    loading.SetActive(false);
    leaderboardsCanvasGroup.alpha = 1;
  }
}
