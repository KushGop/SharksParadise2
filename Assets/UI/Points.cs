using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{

  [SerializeField] private GameSessionStats stats;
  [SerializeField] private Text scoreText;

  // Start is called before the first frame update
  void Start()
  {
    stats.score = 0;
    scoreText.text = "0";
  }

  // Update is called once per frame
  void Update()
  {
    scoreText.text = stats.score.ToString();
  }
}
