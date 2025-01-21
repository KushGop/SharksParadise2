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
  [SerializeField] TextMeshProUGUI cost;
  [SerializeField] Transform fishImage;
  [SerializeField] Aquarium_SO images;
  [SerializeField] Button button;
  bool canUpgade;


  public Fishes GetFish()
  {
    return fish;
  }

  public void CardInit(Fishes f, FishCard_S cardData)
  {
    fishS = cardData;
    fish = f;

    if (cardData.count >= cardData.baseCount)
    {
      count.text = cardData.baseCount.ToString() + "/" + cardData.baseCount.ToString();
      increase.text = "upgrade";
      button.enabled = (GameManager.totalGems >= fishS.upgradeCost);
      cost.text = "<sprite name=\"Gem\"*" + cardData.upgradeCost.ToString();
      canUpgade = true;
    }
    else
    {
      count.text = cardData.count.ToString() + "/" + cardData.baseCount.ToString();
      increase.text = "add 1";
      button.enabled = (GameManager.totalCoins >= fishS.increaseCost);
      cost.text = "<sprite name=\"Coin\"> *" + cardData.increaseCost.ToString();
      canUpgade = false;
    }

    multiplyer.text = cardData.multiplyer.ToString() + "*";
    value.text = cardData.value.ToString() + " points";
    Instantiate(images.images[fish], fishImage);
  }

  public void UpgradeFish()
  {
    if (canUpgade)
    {
      if (GameManager.totalGems >= fishS.upgradeCost)
      {
        GameManager.totalGems -= fishS.upgradeCost;
        fishS.multiplyer++;
        fishS.count = 0;
        fishS.baseCount *= 2;
        canUpgade = false;
      }
    }
    else
    {
      //add one to count
      if (GameManager.totalCoins >= fishS.increaseCost)
      {
        GameManager.totalCoins -= fishS.increaseCost;
        fishS.count += 1;
      }
    }
    UpgradesManager.updateCosts();
    AquariumManager.aquariumData.fishCards[fish] = fishS;
    DataPersistenceManager.instance.SaveGame();
    CardInit(fish, fishS);
  }
}
