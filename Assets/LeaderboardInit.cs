using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GooglePlayGames.Android;
using Gley.GameServices;

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

  private bool loaded1 = false, loaded2 = false;

  private void Start()
  {
    SetUpUI();
  }

  public void SetUpUI()
  {
    leaderboard1.SetActive(false);
    leaderboard2.SetActive(false);
    login.SetActive(false);
    loading.SetActive(false);

    if (API.IsLoggedIn())
    {
      loading.SetActive(true);
      topThree.LoadLeaderBoard();
      playerLeaderboard.LoadLeaderBoard();
    }
    else
    {
      login.SetActive(true);
    }
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

  private void ShowLeaderboard()
  {
    loading.SetActive(false);
    leaderboard1.SetActive(true);
    leaderboard2.SetActive(true);
  }
}
