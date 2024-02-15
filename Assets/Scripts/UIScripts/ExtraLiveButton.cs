using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExtraLiveButton : MonoBehaviour
{

  [SerializeField] private Button button;
  [SerializeField] private TextMeshProUGUI buttonText;
  [SerializeField] private TextMeshProUGUI coinText;
  [SerializeField] private DeathTimer timer;
  [SerializeField] private int reviveCost;
  private int reviveCount;

  private void Start()
  {
    reviveCount = 1;
  }

  private void OnEnable()
  {
    buttonText.text = "Use " + (reviveCost * reviveCount).ToString() + " coins";
    if (GameManager.totalCoins + GameManager.coins >= reviveCost * reviveCount)
    {
      //TODO - show total coins
      //coinText.text = (GameManager.totalCoins + GameManager.coins).ToString();
      button.interactable = true;
    }
    else
    {
      button.interactable = false;
    }
  }

  public void UseExtraLife()
  {
    GameManager.totalCoins -= reviveCost * reviveCount;
    if (GameManager.totalCoins < 0)
    {
      GameManager.coins -= Mathf.Abs(GameManager.totalCoins);
      GameManager.totalCoins = 0;
    }
    //SaveSystem.SaveData(new(GameManager.totalCoins, GameManager.score));
    coinText.text = GameManager.coins.ToString();
    timer.ContinueGame();
    reviveCount++;
  }
}
