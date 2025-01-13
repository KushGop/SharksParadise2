using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCardInit : MonoBehaviour, IDataPersistence
{
  [SerializeField] GameObject fishCard;
  [SerializeField] Transform common;
  [SerializeField] Transform uncommon;
  [SerializeField] Transform rare;
  [SerializeField] Transform special;

  public void LoadData(GameData data)
  {
    print("Load aquarium");
    AquariumManager.aquariumData = data.aquarium;

    //init
    foreach (FishCard_S s in AquariumManager.aquariumData.fishCards)
    {
      GameObject go = Instantiate(fishCard, GetTransform(s.rarity));
      go.GetComponent<FishCard>().CardInit(s);
    }

  }

  public void SaveData(GameData data)
  {
    print("Save aquarium");
    data.aquarium = AquariumManager.aquariumData;
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
