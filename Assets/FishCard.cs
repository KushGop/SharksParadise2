using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishCard : MonoBehaviour
{
  Fishes fish;
  [SerializeField] TextMeshProUGUI count;
  [SerializeField] TextMeshProUGUI multiplyer;
  [SerializeField] TextMeshProUGUI value;
  [SerializeField] Transform fishImage;
  [SerializeField] Aquarium_SO images;

  public Fishes GetFish()
  {
    return fish;
  }

  public void CardInit(FishCard_S cardData)
  {
    fish = cardData.fish;
    count.text = cardData.count.ToString() + "/" + cardData.baseCount.ToString();
    multiplyer.text = cardData.multiplyer.ToString() + "*";
    value.text = cardData.value.ToString() + " points";
    Instantiate(images.images[cardData.fish], fishImage);
  }
}
