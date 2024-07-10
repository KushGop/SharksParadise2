using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReviveButton : MonoBehaviour
{
  [SerializeField] private Button button;
  [SerializeField] private TextMeshProUGUI buttonText;
  [SerializeField] private Currency currency;
  [SerializeField] private int reviveCost;
  [SerializeField] private DeathTimer timer;
  [SerializeField] private CurrencyCounter counter;
  [SerializeReference] private int Total;
  [SerializeReference] private int BaseTotal;
  private string text;

  void Awake()
  {
    if (currency == Currency.Coin)
    {
      Total = GameManager.totalCoins;
      BaseTotal = GameManager.coins;
      text = " coins";
    }
    else if (currency == Currency.Gem)
    {
      Total = GameManager.totalGems;
      BaseTotal = GameManager.gems;
      text = " gems";
    }
  }

  private void OnEnable()
  {
    buttonText.text = (reviveCost).ToString() + text;
    if (Total + BaseTotal >= reviveCost)
    {
      button.interactable = true;
    }
    else
    {
      button.interactable = false;
    }
  }
  public void Revive()
  {
    reviveCost *= currency == Currency.Coin ? 2 : 1;
    int value = 0;
    Total -= reviveCost;
    if (Total < 0)
    {
      value = Mathf.Abs(Total);
      Total = 0;
    }
    counter.AddCurrency(currency, -value);
    timer.ContinueGame();
  }

}
