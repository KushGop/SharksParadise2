using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Gley.GameServices;
using TMPro;

public class ShowLeaderboard : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI leaderboard;
  [SerializeField] private Transform leaderboardList;
  private int i;

  public void AddScore(int score)
  {
    API.SubmitScore(score, LeaderboardNames.Highscores);
    //Social.ReportScore(score, GPGSIds.leaderboard_highscores, (bool success) =>
    //{
    //  if (success)
    //  {

    //  }
    //  else
    //  {

    //  }
    //});
    ListLeaderboard();
  }

  public void ListLeaderboard()
  {
    i = 0;
    string leaderboardID = GPGSIds.leaderboard_highscores;
    LeaderboardStart leaderboardStart = LeaderboardStart.PlayerCentered;
    int scoresToDisplay = 10;
    LeaderboardCollection leaderboardType = LeaderboardCollection.Public;
    LeaderboardTimeSpan leaderboardTimeSpan = LeaderboardTimeSpan.AllTime;
    PlayGamesPlatform.Instance.LoadScores(leaderboardID,
      leaderboardStart,
      scoresToDisplay,
      leaderboardType,
      leaderboardTimeSpan,
      (LeaderboardScoreData data) =>
        {
          leaderboard.text = data.ToString();
          leaderboardList.GetChild(i).GetComponent<LeaderboardItem>().SetLeaderboardItem(data.PlayerScore.userID, data.PlayerScore.value);
          i++;
          print(data.ToString());
        });

    //LoadUsersAndDisplay(PlayGamesPlatform.Instance.CreateLeaderboard());
  }

  //TODO: only load 20 at a time, display 10
  private void LoadUsersAndDisplay(ILeaderboard lb)
  {
    // get the user ids
    List<string> userIds = new List<string>();

    foreach (IScore score in lb.scores)
    {
      userIds.Add(score.userID);
    }
    // load the profiles and display (or in this case, log)
    Social.LoadUsers(userIds.ToArray(), (users) =>
    {
      string status = "Leaderboard loading: " + lb.title + " count = " +
          lb.scores.Length;
      foreach (IScore score in lb.scores)
      {
        IUserProfile user = FindUser(users, score.userID);
        status += "\n" + score.formattedValue + " by " +
            (string)(
                (user != null) ? user.userName : "**unk_" + score.userID + "**");
      }
      print(status);
    });
  }
  private IUserProfile FindUser(IUserProfile[] users, string id)
  {
    for (int i = 0; i < users.Length; i++)
    {
      if (users[i].id == id)
        return users[i];
    }
    return users[0];
  }
}
