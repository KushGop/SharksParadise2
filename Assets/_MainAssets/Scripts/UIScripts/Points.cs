using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] Shaker shake;

  void Start()
  {
    GameManager.score = 0;
    scoreText.text = "0";
    GameManager.UpdateScore += UpdateScore;
  }
  private void OnDestroy()
  {
    GameManager.UpdateScore -= UpdateScore;
  }

  void UpdateScore()
  {
    shake.Shake();
    scoreText.text = GameManager.score.ToString();
  }
}
