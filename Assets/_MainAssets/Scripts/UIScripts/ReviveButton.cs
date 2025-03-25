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
  [SerializeReference] private int total;
  [SerializeReference] private int inGameTotal;
  private string icon;

  //Sets scene objects and texts
  private void OnEnable()
  {
    //set variables
    if (currency == Currency.Coin)
    {
      total = GameManager.totalCoins;
      inGameTotal = GameManager.coins;
      icon = "<sprite name=\"Coin\">";
    }
    else if (currency == Currency.Gem)
    {
      total = GameManager.totalGems;
      inGameTotal = GameManager.gems;
      icon = "<sprite name=\"Gem\">";
    }
    buttonText.text = icon + (reviveCost).ToString();
    //set interactable based on total currency
    button.interactable = total + inGameTotal >= reviveCost;
  }

  //Updates currencies then continues the game
  public void Revive()
  {
    //update manager count for currencies
    //Uses AddCurrency() to update in-game text currency
    if (currency == Currency.Coin)
    {
      if (reviveCost > GameManager.totalCoins)
      {
        inGameTotal = reviveCost - GameManager.totalCoins;
        GameManager.totalCoins = 0;
        counter.AddCurrency(currency, -inGameTotal);
      }
      else
      {
        GameManager.totalCoins -= reviveCost;
        counter.AddCurrency(currency, 0);
      }
    }
    else if (currency == Currency.Gem)
    {
      if (reviveCost > GameManager.totalCoins)
      {
        inGameTotal = reviveCost - GameManager.totalCoins;
        GameManager.totalGems = 0;
        GameManager.gems = inGameTotal;
        counter.AddCurrency(currency, -inGameTotal);
      }
      else
      {
        GameManager.totalGems -= reviveCost;
        counter.AddCurrency(currency, 0);
      }
    }
    //Save game
    DataPersistenceManager.instance.SaveGame();
    //Double revive cost
    reviveCost *= currency == Currency.Coin ? 2 : 1;
    timer.ContinueGame();
  }

}
