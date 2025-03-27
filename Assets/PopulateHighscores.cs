using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Gley.GameServices;
using System.Runtime.InteropServices;
using Gley.GameServices.Internal;
using System.Linq;

public class PopulateHighscores : MonoBehaviour
{

#if GLEY_GAMESERVICES_IOS
    // Import the native Objective-C function
    [DllImport("__Internal")]
    private static extern void _loadLeaderboardScores(string leaderboardID, string gameObjectName);
#endif
  /*
   * first i want to submit score to leaderboard
   * then i want to check the rank of the player
   * if the player is under rank 14, get the top 14 players
   * else get use the top 14 list to get the top three
   * then call player center
   */
  [SerializeField] private LeaderboardInit leaderboardInit;
  LeaderboardUserData[] scores;
  bool loaded1 = false;
  bool loaded2 = false;
  int index;
  int childIndex;
  private string[] usernames;

  internal void LoadLeaderboard()
  {
    //init
    loaded1 = false;
    loaded2 = false;
    childIndex = 0;
    usernames = new string[14];

    StartCoroutine(Loading());

    //submit score
    API.GetPlayerScore(LeaderboardNames.Highscores, score =>
    {
      if (GameManager.highscore > score)
      {
        API.SubmitScore(GameManager.highscore, LeaderboardNames.Highscores);
      }
    });

#if GLEY_GAMESERVICES_ANDROID
    //check rank
    API.GetPlayerRank(LeaderboardNames.Highscores, (long rank) =>
    {
      if (rank > 14)
      {
        API.LoadLeaderboard(LeaderboardNames.Highscores, isPlayerCenter: false, 3, (LeaderboardUserData[] data) =>
         {
           index = 0;
           SetScore(data, (string[] scoreData) =>
           {
             API.LoadUsers(scoreData, scoreData.Length, (string[] users) =>
             {
               GetUsers(users);
               loaded1 = true;
             });
           });
         });
        API.LoadLeaderboard(LeaderboardNames.Highscores, isPlayerCenter: true, 11, (LeaderboardUserData[] data) =>
        {
          SetScore(data, (string[] scoreData) =>
          {
            API.LoadUsers(scoreData, scoreData.Length, (string[] users) =>
            {
              GetUsers(users);
              loaded2 = true;
            });
          });
        });
      }
      else
      {
        API.LoadLeaderboard(LeaderboardNames.Highscores, isPlayerCenter: false, 14, (LeaderboardUserData[] data) =>
         {
           SetScore(data, (string[] scoreData) =>
           {
             API.LoadUsers(scoreData, scoreData.Length, (string[] users) =>
             {
               GetUsers(users);
               loaded1 = true;
               loaded2 = true;
             });
           });
         });
      }
    });

#endif
#if GLEY_GAMESERVICES_IOS
        List<Leaderboard> gameLeaderboards;
        try
        {
            gameLeaderboards = Resources.Load<GameServicesData>(Constants.DATA_NAME_RUNTIME).allGameLeaderboards;
            _loadLeaderboardScores(
                gameLeaderboards.FirstOrDefault(cond => cond.name == LeaderboardNames.Highscores.ToString()).idIos,
                gameObject.name);
        }
        catch
        {
            Debug.LogError("Game Services Data not found -> Go to Tools->Gley->Game Services to setup the plugin");
        }
        
#endif
  }

  private void SetScore(LeaderboardUserData[] s, UnityAction<string[]> Complete)
  {
    scores = s;
    string[] ids = new string[s.Length];
    for (int i = 0; i < s.Length; i++)
    {
      ids[i] = s[i].id;
    }
    Complete(ids);
  }

  private void GetUsers(string[] ids)
  {
    for (int i = childIndex; i < ids.Length; i++)
    {
      scores[i].id = ids[i];
      transform.GetChild(childIndex++).GetComponent<LeaderboardItem>().SetScore(scores[i]);
    }
  }

  IEnumerator Loading()
  {
    yield return new WaitUntil(() => loaded1 && loaded2);
    //while (!loaded1 && !loaded2) yield return null;
    leaderboardInit.ShowLeaderboard();
  }



  #region iOS
  public void OnUsernameReceived(string username)
  {
    usernames[index] = username;
    index++;
  }
  public void OnLeaderboardLoaded(string leaderboardData)
  {
    // leaderboardData will be a string of the format: "rank,playerName,score;rank,playerName,score;..."
    string[] leaderboardEntries = leaderboardData.Split(';');
    scores = new LeaderboardUserData[leaderboardEntries.Length - 1];
    for (int i = 0; i < leaderboardEntries.Length - 1; i++)
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

      transform.GetChild(childIndex++).GetComponent<LeaderboardItem>().SetScore(scores[i]);
    }
  }
  #endregion
}
