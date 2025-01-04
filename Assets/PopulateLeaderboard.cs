using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Gley.GameServices;
using TMPro;

public class PopulateLeaderboard : MonoBehaviour
{
  [SerializeField] private bool isTopThree;
  [SerializeField] private LeaderboardInit leaderboardInit;

  bool loaded;
  private int rows;
  LeaderboardUserData[] scores;
  UnityAction<LeaderboardUserData[]> action1;
  UnityAction<string[]> action2;
  private string[] usernames;

  private void Start()
  {
    loaded = false;
    rows = isTopThree ? 3 : 7;
    usernames = new string[rows];
    action1 += SetScore;
    action2 += GetUsers;
  }
  public void LoadLeaderBoard()
  {
    StartCoroutine(Loading());
    GetResults();
  }

  public void GetResults()
  {
    if (isTopThree)
      API.GetTopThree(LeaderboardNames.Highscores, action1);
    else
      API.GetPlayerCenter(LeaderboardNames.Highscores, rows, action1);
  }

  private void GetUsers(string[] ids)
  {
    usernames = ids;
    for (int i = 0; i < rows; i++)
    {
      scores[i].id = usernames[i];
      transform.GetChild(i).GetComponent<LeaderboardItem>().SetScore(scores[i]);
    }
    loaded = true;
  }

  private void SetScore(LeaderboardUserData[] s)
  {
    scores = s;
    string[] ids = new string[rows];
    for (int i = 0; i < s.Length; i++)
    {
      ids[i] = s[i].id;
    }
    API.LoadUsers(ids, s.Length, action2);
  }

  IEnumerator Loading()
  {
    yield return new WaitUntil(() => loaded);
    leaderboardInit.DoneLoading(isTopThree);
  }

}
