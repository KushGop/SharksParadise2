using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExtraLiveButton : MonoBehaviour
{

  [SerializeField] private Button button;
  [SerializeField] private TextMeshProUGUI buttonTextCoin;
  [SerializeField] private TextMeshProUGUI buttonTextGem;
  //[SerializeField] private TextMeshProUGUI coinText;
  //[SerializeField] private CanvasGroup group;
  [SerializeField] private CurrencyCounter coins;
  [SerializeField] private CurrencyCounter gem;
  [SerializeField] private DeathTimer timer;
  private int reviveCostCoin = 100;
  private int reviveCostGem = 10;
  private int reviveCount = 1;

  private void Start()
  {
    reviveCostCoin = 50;
    reviveCostGem = 10;
    reviveCount = 2;
  }

  private void OnEnable()
  {
    print("here----------------------------------------------");
    reviveCostCoin *= reviveCount;
    buttonTextCoin.text = "Use " + (reviveCostCoin).ToString() + " coins";
    buttonTextGem.text = "Use " + (reviveCostGem).ToString() + " gems";
    if (GameManager.totalCoins + GameManager.coins >= reviveCostCoin)
    {
      button.interactable = true;
    }
    else
    {
      button.interactable = false;
    }
  }

  public void UseExtraLifeCoin()
  {
    int value = 0;
    GameManager.totalCoins -= reviveCostCoin;
    if (GameManager.totalCoins < 0)
    {
      value = Mathf.Abs(GameManager.totalCoins);
      GameManager.totalCoins = 0;
    }
    coins.AddCurrency(Currency.Coin, -value);
    timer.ContinueGame();
  }
  public void UseExtraLifeDiamond()
  {
    int value = 0;
    GameManager.totalGems -= reviveCostCoin;
    if (GameManager.totalGems < 0)
    {
      value = Mathf.Abs(GameManager.totalGems);
      GameManager.totalGems = 0;
    }
    gem.AddCurrency(Currency.Gem, -value);
    timer.ContinueGame();
  }
}
