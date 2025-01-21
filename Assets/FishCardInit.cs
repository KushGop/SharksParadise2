using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCardInit : MonoBehaviour
{

  [SerializeField] GameObject fishCard;
  [SerializeField] Transform common;
  [SerializeField] Transform uncommon;
  [SerializeField] Transform rare;
  [SerializeField] Transform special;

  public void Start()
  {
    //init
    foreach (Fishes s in AquariumManager.aquariumData.fishCards.Keys)
    {
      GameObject go = Instantiate(fishCard, GetTransform(AquariumManager.aquariumData.fishCards[s].rarity));
      go.GetComponent<FishCard>().CardInit(s, AquariumManager.aquariumData.fishCards[s]);
    }

  }

  private Transform GetTransform(Rarity r)
  {
    switch (r)
    {
      case Rarity.COMMON:
        return common;
      case Rarity.UNCOMMON:
        return uncommon;
      case Rarity.RARE:
        return rare;
      case Rarity.SPECIAL:
        return special;
      case Rarity.LEGENDARY:
        break;
    }
    return common;
  }
}
