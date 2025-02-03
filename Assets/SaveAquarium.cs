using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAquarium : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {

  }

  public void SaveData(GameData data)
  {
    print("save aquarium");
    //foreach (Fishes f in AquariumManager.aquariumData.fishCards.Keys)
    //{
    //  print(f + ": " + AquariumManager.aquariumData.fishCards[f].count);
    //}
    data.aquarium = AquariumManager.aquariumData;
    //foreach (Fishes f in data.aquarium.fishCards.Keys)
    //{
    //  print(f + ": " + data.aquarium.fishCards[f].count);
    //}
  }
}
