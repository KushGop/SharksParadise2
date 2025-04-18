using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddIntAndText : MonoBehaviour
{
  public enum Stat
  {
    SCORE,
    HIGHSCORE,
    COINS,
    GEMS,
    XP,
    TOTAL_COINS,
    TOTAL_GEMS,
    TOTAL_TOKENS,
    TOTAL_XP
  }
  public Stat stat;
  public string str;
  public TextMeshProUGUI text;

  private void Start()
  {
    SetText();
    UpgradesManager.updateCosts += SetText;

  }
  private void OnDestroy()
  {
    UpgradesManager.updateCosts -= SetText;
  }

  private void SetText()
  {
    switch (stat)
    {
      case Stat.COINS:
        text.text = string.Format(str, GameManager.coins);
        break;
      case Stat.GEMS:
        text.text = string.Format(str, GameManager.gems);
        break;
      case Stat.XP:
        text.text = string.Format(str, GameManager.score / 100);
        break;
      case Stat.SCORE:
        text.text = string.Format(str, GameManager.score);
        break;
      case Stat.HIGHSCORE:
        text.text = string.Format(str, GameManager.highscore);
        break;
      case Stat.TOTAL_COINS:
        text.text = string.Format(str, GameManager.totalCoins);
        break;
      case Stat.TOTAL_GEMS:
        text.text = string.Format(str, GameManager.totalGems);
        break;
      case Stat.TOTAL_TOKENS:
        text.text = string.Format(str, GameManager.totalTokens);
        break;
    }
  }
}
