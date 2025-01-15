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
  [SerializeField] Transform fishImage;
  [SerializeField] Aquarium_SO images;
  [SerializeField] Button button;

  public Fishes GetFish()
  {
    return fish;
  }

  public void CardInit(FishCard_S cardData)
  {
    fishS = cardData;
    fish = cardData.fish;

    if (cardData.count >= cardData.baseCount)
    {
      count.text = "upgrade";
      button.enabled = true;
    }
    else
    {
      count.text = cardData.count.ToString() + "/" + cardData.baseCount.ToString();
      button.enabled = false;
    }

    multiplyer.text = cardData.multiplyer.ToString() + "*";
    value.text = cardData.value.ToString() + " points";
    Instantiate(images.images[cardData.fish], fishImage);
  }

  public void UpgradeFish()
  {
    fishS.multiplyer++;
    fishS.count = 0;
    AquariumManager.aquariumData.fishCards[0] = fishS;
    DataPersistenceManager.instance.SaveGame();
  }
}
