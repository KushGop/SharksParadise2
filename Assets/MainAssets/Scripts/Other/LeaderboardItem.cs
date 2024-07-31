using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardItem : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI user;
  [SerializeField] TextMeshProUGUI score;

  internal void SetLeaderboardItem(string username, long value)
  {
    user.text = username;
    score.text = value.ToString();
  }
}
