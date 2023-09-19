using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddIntAndText : MonoBehaviour
{
  public enum Stat
  {
    score,
    highscore,
    coins,
    xp,
    totalCoins,
    totalXP
  }
  public Stat stat;
  public GameSessionStats stats;
  public string left;
  public string right;
  public TextMeshProUGUI text;

  private void Start()
  {
    text.text = left + stats.GetType().GetField(stat.ToString()).GetValue(stats) + " " + right;
  }

}
