﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#if GLEY_GAMESERVICES_IOS
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
#endif
#if GLEY_GAMESERVICES_ANDROID
using GooglePlayGames.BasicApi;
#endif

namespace Gley.GameServices.Internal
{
  public class LeaderboardManager
  {
    //load the list of all game leaderboards form the Settings Window
    private List<Leaderboard> gameLeaderboards;


    /// <summary>
    /// Constructor, loads settings data
    /// </summary>
    public LeaderboardManager()
    {
      try
      {
        gameLeaderboards = Resources.Load<GameServicesData>(Constants.DATA_NAME_RUNTIME).allGameLeaderboards;
      }
      catch
      {
        Debug.LogError("Game Services Data not found -> Go to Tools->Gley->Game Services to setup the plugin");
      }
    }


    /// <summary>
    /// Submit a score for both ANdroid and iOS using the Unity Social interface
    /// </summary>
    /// <param name="score">value of the score</param>
    /// <param name="leaderboardName">leaderboard to submit score in</param>
    /// <param name="SubmitComplete">callback -> submit result</param>
    public void SubmitScore(long score, string leaderboardName, UnityAction<bool, GameServicesError> SubmitComplete)
    {
      string leaderboardId = null;
#if GLEY_GAMESERVICES_ANDROID
      leaderboardId = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName).idGoogle;
#endif
#if GLEY_GAMESERVICES_IOS
            leaderboardId = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName).idIos;
#endif
      Social.ReportScore(score, leaderboardId, (bool success) =>
      {
        if (success)
        {
          if (SubmitComplete != null)
          {
            SubmitComplete(true, GameServicesError.Success);
          }
        }
        else
        {
          if (SubmitComplete != null)
          {
            SubmitComplete(false, GameServicesError.ScoreSubmitFailed);
          }
        }
      });
    }


    /// <summary>
    /// Shows all game leaderboards
    /// </summary>
    public void ShowLeaderboards()
    {
      Social.ShowLeaderboardUI();
    }


    /// <summary>
    /// Displays a specific leaderboard on screen. Available only for Google Play
    /// </summary>
    /// <param name="leaderboardName">the name of the leaderboard to display</param>
    public void ShowSingleLeaderboard(LeaderboardNames leaderboardName)
    {
#if GLEY_GAMESERVICES_ANDROID
      string leaderboardId = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName.ToString()).idGoogle;
      ((GooglePlayGames.PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboardId);
      return;
#endif
#if GLEY_GAMESERVICES_IOS
            string leaderboardId = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName.ToString()).idIos;
            GameCenterPlatform.ShowLeaderboardUI(leaderboardId, UnityEngine.SocialPlatforms.TimeScope.AllTime);
            return;
#endif
    }


    public void LoadLeaderboardScores(LeaderboardNames leaderboardName, bool isPlayerCenter, int rows, UnityAction<LeaderboardUserData[]> CompleteMethod)
    {
#if GLEY_GAMESERVICES_ANDROID
      string leaderboardId = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName.ToString()).idGoogle;
      ((GooglePlayGames.PlayGamesPlatform)Social.Active).LoadScores(
        leaderboardId,
        isPlayerCenter ? LeaderboardStart.PlayerCentered : LeaderboardStart.TopScores,
        rows,
        LeaderboardCollection.Public,
        LeaderboardTimeSpan.AllTime,
        (LeaderboardScoreData data) =>
        {
          LeaderboardUserData[] leaderboard = new LeaderboardUserData[data.Scores.Length];
          if (CompleteMethod != null)
          {
            for (int i = 0; i < data.Scores.Length; i++)
            {
              try
              {
                leaderboard[i] = new(data.Scores[i].rank.ToString(), data.Scores[i].userID, data.Scores[i].value.ToString());
              }
              catch
              {
                leaderboard[i] = new("-", "-", "-");
              }
            }
            CompleteMethod(leaderboard);
          }
        }
    );

#endif
#if GLEY_GAMESERVICES_IOS
    ILeaderboard Leaderboard = Social.CreateLeaderboard();
    Leaderboard.id = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName.ToString()).idIos;
    Leaderboard.timeScope = TimeScope.AllTime;
    Leaderboard.LoadScores(success =>
    {
        if (success)
        {
            rows = Mathf.Min(rows, Leaderboard.scores.Length);
            LeaderboardUserData[] leaderboard = new LeaderboardUserData[rows];
            if (CompleteMethod != null)
            {
                for (int i = 0; i < rows; i++)
                {
                    try
                    {
                        leaderboard[i] = new(Leaderboard.scores[i].rank.ToString(), Leaderboard.scores[i].userID, Leaderboard.scores[i].value.ToString());
                    }
                    catch
                    {
                        leaderboard[i] = new("-", "-", "-");
                    }
                }
                CompleteMethod(leaderboard);
            }
        }
        
    });

#endif
    }

    public void LoadUsers(string[] userIds, int rows, UnityAction<string[]> CompleteMethod)
    {
#if GLEY_GAMESERVICES_ANDROID
      ((GooglePlayGames.PlayGamesPlatform)Social.Active).LoadUsers(
          userIds,
          (UnityEngine.SocialPlatforms.IUserProfile[] data) =>
          {
            if (CompleteMethod != null)
            {
              string[] leaderboard = new string[rows];
              for (int i = 0; i < rows; i++)
              {
                try
                {
                  leaderboard[i] = data[i].userName;
                }
                catch
                {
                  leaderboard[i] = "-";
                }
              }
              CompleteMethod(leaderboard);
            }
          }
      );

#endif
#if GLEY_GAMESERVICES_IOS
        Social.LoadUsers(userIds, (users)=>
        {
            if(users != null)
            {
                string[] leaderboard = new string[rows];
                for (int i = 0; i < rows; i++)
                {
                    try
                    {
                        leaderboard[i] = users[i].userName;
                    }
                    catch
                    {
                        leaderboard[i] = "-";
                    }
                }
                CompleteMethod(leaderboard);
            }
        });

#endif
    }

    /// <summary>
    /// Retrieves the highest score from leaderboard of the current player
    /// </summary>
    /// <param name="leaderboardName">the name of the leaderboard</param>
    /// <param name="CompleteMethod">a complete method called after score is loaded</param>
    public void GetPlayerScore(LeaderboardNames leaderboardName, UnityAction<long> CompleteMethod)
    {
#if GLEY_GAMESERVICES_ANDROID
      string leaderboardId = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName.ToString()).idGoogle;
      ((GooglePlayGames.PlayGamesPlatform)Social.Active).LoadScores(
          leaderboardId,
          LeaderboardStart.PlayerCentered,
          1,
          LeaderboardCollection.Public,
          LeaderboardTimeSpan.AllTime,
          (LeaderboardScoreData data) =>
          {
            if (CompleteMethod != null)
            {
              CompleteMethod(data.PlayerScore.value);
            }
          }
      );
#endif
#if GLEY_GAMESERVICES_IOS
            ILeaderboard Leaderboard = Social.CreateLeaderboard();
            Leaderboard.id = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName.ToString()).idIos;
            Leaderboard.timeScope = TimeScope.AllTime;
            Leaderboard.LoadScores(success =>
            {
                if (success)
                {
                    IScore[] scores = Leaderboard.scores;
                    if (scores.Length > 0)
                    {
                        if (CompleteMethod != null)
                        {
                            CompleteMethod(Leaderboard.localUserScore.value);
                        }
                    }
                    else
                    {
                        if (CompleteMethod != null)
                        {
                            CompleteMethod(0);
                        }
                    }
                }
                else
                {
                    if (CompleteMethod != null)
                    {
                        CompleteMethod(0);
                    }
                }
            });
#endif
    }


    /// <summary>
    /// Retrieves the highest score from leaderboard of the current player
    /// </summary>
    /// <param name="leaderboardName">the name of the leaderboard</param>
    /// <param name="CompleteMethod">a complete method called after score is loaded</param>
    public void GetPlayerRank(LeaderboardNames leaderboardName, UnityAction<long> CompleteMethod)
    {
#if GLEY_GAMESERVICES_ANDROID
      string leaderboardId = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName.ToString()).idGoogle;
      ((GooglePlayGames.PlayGamesPlatform)Social.Active).LoadScores(
          leaderboardId,
          LeaderboardStart.PlayerCentered,
          1,
          LeaderboardCollection.Public,
          LeaderboardTimeSpan.AllTime,
          (LeaderboardScoreData data) =>
          {
            if (CompleteMethod != null)
            {
              CompleteMethod(data.PlayerScore.rank);
            }
          }
      );
#endif
#if GLEY_GAMESERVICES_IOS
            ILeaderboard Leaderboard = Social.CreateLeaderboard();
            Leaderboard.id = gameLeaderboards.FirstOrDefault(cond => cond.name == leaderboardName.ToString()).idIos;
            Leaderboard.timeScope = TimeScope.AllTime;
            Leaderboard.LoadScores(success =>
            {
                if (success)
                {
                    IScore[] scores = Leaderboard.scores;
                    if (scores.Length > 0)
                    {
                        if (CompleteMethod != null)
                        {
                            CompleteMethod(Leaderboard.localUserScore.rank);
                        }
                    }
                    else
                    {
                        if (CompleteMethod != null)
                        {
                            CompleteMethod(0);
                        }
                    }
                }
                else
                {
                    if (CompleteMethod != null)
                    {
                        CompleteMethod(0);
                    }
                }
            });
#endif
    }

  }
}
