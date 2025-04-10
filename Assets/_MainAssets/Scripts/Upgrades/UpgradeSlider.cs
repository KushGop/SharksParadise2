using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSlider : MonoBehaviour
{
  private UpgradeList key;
  [SerializeField] Slider slider;
  [SerializeField] Image coinIcon;
  [SerializeField] Image gemIcon;
  [SerializeField] Image tokenIcon;
  [SerializeField] TextMeshProUGUI costText;
  [SerializeField] CanvasGroup canvasGroup;
  [SerializeField] Button button;
  [SerializeField] TextMeshProUGUI description;
  private int cost;
  private RewardList rewardType;

  private void Start()
  {
    UpgradesManager.updateCosts += UpdateVisuals;
    UpgradesManager.prestige += Reset;
    SetDescription();
  }
  private void OnDestroy()
  {
    UpgradesManager.updateCosts -= UpdateVisuals;
    UpgradesManager.prestige -= Reset;
  }

  private void SetDescription()
  {
    string des = "";
    switch ((ushort)key)
    {
      case 00:
        des = "Increase base speed";
        break;
      case 01:
        des = "Increase boost speed";
        break;
      case 02:
        des = "Decrease boost cost";
        break;
      case 03:
        des = "Decrease jump cost";
        break;
      case 04:
        des = "Increase refill speed";
        break;
      case 05:
        des = "Decrease refill delay";
        break;
      case 10:
        des = "Increase treasure spawn Rate";
        break;
      case 11:
        des = "Increase starfish spawn rate";
        break;
      case 12:
        des = "Increase warning radius";
        break;
      case 13:
        des = "Increase coin fish frequency";
        break;
      case 14:
        des = "Increase power up time";
        break;
      case 20:
        des = "prestige";
        break;
      case 21:
        des = "greatly Increase starfish spawn Rate";
        break;
      case 22:
        des = "greatly Increase treasure spawn Rate";
        break;
      case 23:
        des = "greatly Increase coin fish frequency";
        break;
    }
    description.text = des;
  }

  private void Reset()
  {
    if ((ushort)key < 10)
    {
      UpgradesManager.upgradesData.upgrades[key] = 0;
      slider.value = 0;
      UpdateCost();
      UpdateVisuals();
    }

  }

  public void IncrementUpgrade()
  {
    if (UpgradesManager.upgradesData.upgrades.ContainsKey(key))
    {
      UpgradesManager.upgradesData.upgrades[key]++;
      if (UpgradesManager.upgradesData.upgrades[key] > 10)
      {
        Debug.Log("Error: Over max limit for " + key.ToString());
      }
    }
    else
    {
      Debug.Log("Does not contain upgrade key: " + key.ToString());
    }
    slider.value = UpgradesManager.upgradesData.upgrades[key];
    if (slider.value >= 10)
    {
      UpgradesManager.activateReward(UpgradesManager.rewards[RewardType.fullUpgrade].Item1, UpgradesManager.rewards[RewardType.fullUpgrade].Item2);
    }
    if (rewardType == RewardList.coins)
      GameManager.totalCoins -= cost;
    else if (rewardType == RewardList.gems)
      GameManager.totalGems -= cost;
    else if (rewardType == RewardList.token)
      GameManager.totalTokens -= cost;
    UpdateCost();
    UpgradesManager.updateCosts();
    DataPersistenceManager.instance.SaveGame();
  }

  public void SetKeyPair(UpgradeList key, int count)
  {
    this.key = key;
    slider.value = count;

    if (((ushort)key) < 10)
      rewardType = RewardList.coins;
    else if (((ushort)key) < 20)
      rewardType = RewardList.gems;
    else
      rewardType = RewardList.token;

    UpdateCost();
  }

  private void UpdateVisuals()
  {
    int total = 0;
    if (rewardType == RewardList.coins)
      total = GameManager.totalCoins;
    else if (rewardType == RewardList.gems)
      total = GameManager.totalGems;
    else if (rewardType == RewardList.token)
      total = GameManager.totalTokens;

    if (total < cost)
    {
      canvasGroup.alpha = 0.5f;
      button.interactable = false;
    }
    else
    {
      canvasGroup.alpha = 1f;
      button.interactable = true;
    }
  }

  private void UpdateCost()
  {
    //check if complete
    if (slider.value >= 10)
    {
      costText.text = "";
      coinIcon.color = new(0, 0, 0, 0);
      gemIcon.color = new(0, 0, 0, 0);
      tokenIcon.color = new(0, 0, 0, 0);
      button.gameObject.SetActive(false);

      return;
    }
    button.gameObject.SetActive(true);
    //Coin item
    if (rewardType == RewardList.coins)
    {
      coinIcon.color = new(255, 255, 255, 255);
      //initial value 1000 or 2000
      cost = ((ushort)key) % 10 == 0 ? 200 : 100;
      cost = RecursiveCost(cost, UpgradesManager.upgradesData.upgrades[UpgradeList.prestigeCount]);
      cost = RecursiveCoinCost(cost, (UpgradesManager.upgradesData.upgrades[key]));
    }
    //Gem item
    else if (rewardType == RewardList.gems)
    {
      gemIcon.color = new(255, 255, 255, 255);
      //initial value 1 or 2
      cost = ((ushort)key) % 10 == 0 ? 4 : 2;
      cost = RecursiveGemCost(cost, (UpgradesManager.upgradesData.upgrades[key]));
    }
    else
    {
      tokenIcon.color = new(255, 255, 255, 255);
      cost = 1;
    }
    costText.text = cost.ToString();
    UpdateVisuals();
  }

  #region Recursive
  private int RecursiveCost(int baseCost, int n)
  {
    if (n == 0)
    {
      return baseCost;
    }
    else
    {
      return Mathf.FloorToInt((RecursiveCost(baseCost, n - 1) * 1.25f) / 10) * 10;
    }
  }
  private int RecursiveCoinCost(int baseCost, int n)
  {
    if (n == 0)
    {
      return baseCost;
    }
    else
    {
      return Mathf.FloorToInt((RecursiveCoinCost(baseCost, n - 1) * (1.1f + (UpgradesManager.upgradesData.upgrades[UpgradeList.prestigeCount] * 0.01f))) / 10) * 10;
    }
  }
  private int RecursiveGemCost(int baseCost, int n)
  {
    if (n == 0)
    {
      return baseCost;
    }
    else
    {
      return Mathf.CeilToInt(RecursiveGemCost(baseCost, n - 1) * 1.4f);
    }
  }
  #endregion

}

