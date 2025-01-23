using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishCard : MonoBehaviour
{
  Fishes fish;
  FishCard_S fishS;
  [SerializeField] TextMeshProUGUI count;
  [SerializeField] TextMeshProUGUI multiplyer;
  [SerializeField] TextMeshProUGUI value;
  [SerializeField] TextMeshProUGUI increase;
  [SerializeField] Transform fishImage;
  [SerializeField] Aquarium_SO images;
  [SerializeField] Button button;
  [SerializeField] Image bubble;
  [SerializeField] Colors_SO colors;
  bool canUpgade;


  public Fishes GetFish()
  {
    return fish;
  }

  public void CardInit(Fishes f, FishCard_S cardData)
  {
    fishS = cardData;
    fish = f;
    bubble.color = colors.colors[fishS.level];
    if (cardData.count >= EnemyList.rarityCount[cardData.rarity][cardData.level])
    {
      count.text = EnemyList.rarityCount[cardData.rarity][cardData.level].ToString() + "/" + EnemyList.rarityCount[cardData.rarity][cardData.level].ToString();
      //button.enabled = (GameManager.totalGems >= EnemyList.rarityGemCost[cardData.rarity][cardData.level]);
      increase.text = "upgrade <sprite name=\"Gem\">*" + EnemyList.rarityGemCost[cardData.rarity][cardData.level].ToString();
      canUpgade = true;
    }
    else
    {
      count.text = cardData.count.ToString() + "/" + (EnemyList.rarityCount[cardData.rarity][cardData.level]).ToString();
      //button.enabled = (GameManager.totalCoins >= EnemyList.rarityCoinCost[cardData.rarity]);
      increase.text = "add 1 <sprite name=\"Coin\"> *" + EnemyList.rarityCoinCost[cardData.rarity].ToString();
      canUpgade = false;
    }

    multiplyer.text = "lvl\n" + (cardData.level + 1).ToString();
    value.text = f switch
    {
      Fishes.COIN => EnemyList.specialFish[f][cardData.level].ToString() + "* coin value",
      Fishes.TREASURE => EnemyList.specialFish[f][cardData.level].ToString() + "* treasure value",
      Fishes.STARFISH => EnemyList.specialFish[f][cardData.level].ToString() + " seconds",
      _ => EnemyList.rarityPoint[cardData.rarity][cardData.level].ToString() + " points",
    };
    Instantiate(images.images[fish], fishImage);
  }

  public void TryUpgrade()
  {
    if (canUpgade)
    {
      AquariumManager.HoldCard(this);
      AquariumManager.tryUpgrade();
      AquariumManager.upgradeCard(fish, fishS);
    }
    else
    {
      UpgradeFish();
    }
  }

  public void UpgradeFish()
  {
    if (canUpgade)
    {
      if (GameManager.totalGems >= EnemyList.rarityGemCost[fishS.rarity][fishS.level])
      {
        GameManager.totalGems -= EnemyList.rarityGemCost[fishS.rarity][fishS.level];
        fishS.level++;
        fishS.count = 0;
        canUpgade = false;
      }
      else
      {
        AquariumManager.funds();
        return;
      }
    }
    else
    {
      //add one to count
      if (GameManager.totalCoins >= EnemyList.rarityCoinCost[fishS.rarity])
      {
        GameManager.totalCoins -= EnemyList.rarityCoinCost[fishS.rarity];
        fishS.count += 1;
      }
      else
      {
        AquariumManager.funds();
        return;
      }
    }
    UpgradesManager.updateCosts();
    AquariumManager.aquariumData.fishCards[fish] = fishS;
    DataPersistenceManager.instance.SaveGame();
    CardInit(fish, fishS);
  }
}
