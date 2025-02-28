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
  private string icon;

  void Awake()
  {
    if (currency == Currency.Coin)
    {
      Total = GameManager.totalCoins;
      BaseTotal = GameManager.coins;
      icon = "<sprite name=\"Coin\">";
    }
    else if (currency == Currency.Gem)
    {
      Total = GameManager.totalGems;
      BaseTotal = GameManager.gems;
      icon = "<sprite name=\"Gem\">";
    }
  }

  private void OnEnable()
  {
    buttonText.text = icon + (reviveCost).ToString();
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
