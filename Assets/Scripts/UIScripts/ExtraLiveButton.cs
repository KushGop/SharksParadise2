using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExtraLiveButton : MonoBehaviour
{

  [SerializeField] private Button button;
  [SerializeField] private TextMeshProUGUI buttonText;
  //[SerializeField] private TextMeshProUGUI coinText;
  //[SerializeField] private CanvasGroup group;
  [SerializeField] private CurrencyCounter coins;
  [SerializeField] private DeathTimer timer;
  private int reviveCost = 100;
  private int reviveCount = 1;

  private void Start()
  {
    reviveCost = 50;
    reviveCount = 2;
  }

  private void OnEnable()
  {
    reviveCost *= reviveCount;
    buttonText.text = "Use " + (reviveCost).ToString() + " coins";
    if (GameManager.totalCoins + GameManager.coins >= reviveCost)
    {
      button.interactable = true;
    }
    else
    {
      button.interactable = false;
    }
  }

  public void UseExtraLife()
  {
    int value = 0;
    GameManager.totalCoins -= reviveCost;
    if (GameManager.totalCoins < 0)
    {
      value = Mathf.Abs(GameManager.totalCoins);
      GameManager.totalCoins = 0;
    }
    coins.AddCurrency(Currency.Coin, -value);
    timer.ContinueGame();
  }
}
