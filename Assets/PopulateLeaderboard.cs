using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Gley.GameServices;
using TMPro;
using System.Runtime.InteropServices;
using Gley.GameServices.Internal;
using System.Linq;

public class PopulateLeaderboard : MonoBehaviour
{
    // Import the native Objective-C function
    [DllImport("__Internal")]
    private static extern void _loadLeaderboardScores(string leaderboardID, int rowCount, string gameObjectName, bool isCentered);

    [SerializeField] private bool isPlayerCenter;
  [SerializeField] private LeaderboardInit leaderboardInit;

  bool loaded;
  private int rows;
  LeaderboardUserData[] scores;
  UnityAction<LeaderboardUserData[]> action1;
  UnityAction<string[]> action2;
  private string[] usernames;
    private int index = 0;

  private void Start()
  {

        index = 0;
    loaded = false;
    rows = isPlayerCenter ? 11 : 3;
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
#if GLEY_GAMESERVICES_ANDROID
    API.GetPlayerCenter(LeaderboardNames.Highscores, isPlayerCenter, rows, action1);
#endif
#if GLEY_GAMESERVICES_IOS
        List<Leaderboard> gameLeaderboards;
        try
        {
            gameLeaderboards = Resources.Load<GameServicesData>(Constants.DATA_NAME_RUNTIME).allGameLeaderboards;
            _loadLeaderboardScores(
                gameLeaderboards.FirstOrDefault(cond => cond.name == LeaderboardNames.Highscores.ToString()).idIos,
                rows,
                gameObject.name,
                isPlayerCenter);
        }
        catch
        {
            Debug.LogError("Game Services Data not found -> Go to Tools->Gley->Game Services to setup the plugin");
        }
        
#endif
    }

    IEnumerator Loading()
    {
        while (!loaded) yield return null;

        leaderboardInit.DoneLoading(isPlayerCenter);
    }

    #region Android
    private void GetUsers(string[] ids)
    {
        usernames = ids;
        GetUser();
    }
    private void GetUser()
    {
    for (int i = 0; i < usernames.Length; i++)
    {
      scores[i].id = usernames[i];
      transform.GetChild(i).GetComponent<LeaderboardItem>().SetScore(scores[i]);
    }
    loaded = true;
  }

  private void SetScore(LeaderboardUserData[] s)
  {
    scores = s;
    string[] ids = new string[s.Length];
    for (int i = 0; i < s.Length; i++)
    {
      ids[i] = s[i].id;
    }
        API.LoadUsers(ids, s.Length, action2);
    }

   

  public void OnUsernameReceived(string username)
    {
        usernames[index] = username;
        index++;
    }

    #endregion
    #region iOS
    public void OnLeaderboardLoaded(string leaderboardData)
    {
        // leaderboardData will be a string of the format: "rank,playerName,score;rank,playerName,score;..."
        string[] leaderboardEntries = leaderboardData.Split(';');
        scores = new LeaderboardUserData[leaderboardEntries.Length-1];
        for (int i = 0; i < leaderboardEntries.Length-1; i++)
        {
            string[] entry = leaderboardEntries[i].Split(',');
            if (entry.Length == 3)
            {
                scores[i] = new LeaderboardUserData(entry[0], entry[1], entry[2]);
            }
            else
            {
                scores[i] = new LeaderboardUserData("-", "-", "-");
            }

            transform.GetChild(i).GetComponent<LeaderboardItem>().SetScore(scores[i]);
        }
        loaded = true;
    }
    #endregion
}
