using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{

  [SerializeField] private PlayerStats stats;
  [SerializeField] private Text scoreText;

  // Start is called before the first frame update
  void Start()
  {
    stats.points = 0;
    scoreText.text = "0";
  }

  // Update is called once per frame
  void Update()
  {
    scoreText.text = stats.points.ToString();
  }
}
