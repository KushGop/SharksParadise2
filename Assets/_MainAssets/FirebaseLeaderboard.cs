using Firebase.Firestore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseLeaderboard : MonoBehaviour
{
  FirebaseFirestore db;
  string playerId = "player123"; // Replace with the actual player ID
  readonly int timeoutMs = 5000; // 5 seconds timeout

  void Start()
  {
    db = FirebaseFirestore.DefaultInstance;
    GetTopThreeScores(timeoutMs);
    GetPlayerCenteredScores(playerId, 2, timeoutMs); // Show 2 players above and below
  }

  async void GetTopThreeScores(int timeoutMs)
  {
    Task<QuerySnapshot> leaderboardTask = db.Collection("leaderboard")
        .OrderByDescending("score")
        .Limit(3)
        .GetSnapshotAsync();

    Task timeoutTask = Task.Delay(timeoutMs);
    Task completedTask = await Task.WhenAny(leaderboardTask, timeoutTask);

    if (completedTask == timeoutTask)
    {
      Debug.LogWarning("Timeout: Failed to retrieve leaderboard data.");
      return;
    }

    QuerySnapshot querySnapshot = await leaderboardTask;
    if (!querySnapshot.Documents.Any())
    {
      Debug.Log("Leaderboard is empty.");
      return;
    }

    foreach (DocumentSnapshot document in querySnapshot.Documents)
    {
      string username = document.GetValue<string>("username");
      int score = document.GetValue<int>("score");

      Debug.Log($"Player: {username}, Score: {score}");
    }
  }

  async void GetPlayerCenteredScores(string playerId, int range, int timeoutMs)
  {
    // Step 1: Get the player's score
    Task<DocumentSnapshot> playerTask = db.Collection("leaderboard").Document(playerId).GetSnapshotAsync();
    Task timeoutTask = Task.Delay(timeoutMs);

    Task completedTask = await Task.WhenAny(playerTask, timeoutTask);

    if (completedTask == timeoutTask)
    {
      Debug.LogWarning("Timeout: Failed to retrieve player data.");
      return;
    }

    DocumentSnapshot playerDoc = await playerTask;

    if (!playerDoc.Exists)
    {
      Debug.Log("Player not found in leaderboard.");
      return;
    }

    int playerScore = playerDoc.GetValue<int>("score");

    // Step 2: Get players with higher scores (above)
    Task<QuerySnapshot> aboveTask = db.Collection("leaderboard")
        .WhereGreaterThan("score", playerScore)
        .OrderByDescending("score")
        .Limit(range)
        .GetSnapshotAsync();

    // Step 3: Get players with lower scores (below)
    Task<QuerySnapshot> belowTask = db.Collection("leaderboard")
        .WhereLessThan("score", playerScore)
        .OrderByDescending("score")
        .Limit(range)
        .GetSnapshotAsync();

    // Step 4: Wait for both queries with timeout
    completedTask = await Task.WhenAny(Task.WhenAll(aboveTask, belowTask), Task.Delay(timeoutMs));

    if (completedTask == timeoutTask)
    {
      Debug.LogWarning("Timeout: Failed to retrieve leaderboard data.");
      return;
    }

    QuerySnapshot aboveQuery = await aboveTask;
    QuerySnapshot belowQuery = await belowTask;

    // Step 5: Display results (combine all)
    Debug.Log("=== Leaderboard ===");

    foreach (DocumentSnapshot doc in aboveQuery.Documents)
    {
      Debug.Log($"↑ {doc.GetValue<string>("username")}: {doc.GetValue<int>("score")}");
    }

    Debug.Log($"★ {playerDoc.GetValue<string>("username")}: {playerScore} (You)");

    foreach (DocumentSnapshot doc in belowQuery.Documents)
    {
      Debug.Log($"↓ {doc.GetValue<string>("username")}: {doc.GetValue<int>("score")}");
    }
  }
}
