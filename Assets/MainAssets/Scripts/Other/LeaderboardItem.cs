using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardItem : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private TextMeshProUGUI idText;
  [SerializeField] private TextMeshProUGUI rankText;

  private void Start()
  {
    scoreText.text = "-";
    idText.text = "-";
    rankText.text = "-";
  }
  public void SetScore(LeaderboardUserData s)
  {
    rankText.text = s.rank;
    idText.text = s.id;
    scoreText.text = s.value;
  }
}
