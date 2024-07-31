using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;

  void Start()
  {
    GameManager.score = 0;
    scoreText.text = "0";
  }

  void Update()
  {
    scoreText.text = GameManager.score.ToString();
  }
}
