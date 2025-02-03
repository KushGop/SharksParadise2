using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAquarium : MonoBehaviour, IDataPersistence
{
  public void LoadData(GameData data)
  {
    print("load aquarium");

    AquariumManager.aquariumData = data.aquarium;
    //foreach (Fishes f in AquariumManager.aquariumData.fishCards.Keys)
    //{
    //  print(f + ": " + AquariumManager.aquariumData.fishCards[f].count);
    //}
  }

  public void SaveData(GameData data)
  {
    if (data.aquarium.fishCards.Count == 0)
    {
      print("New Aquarium");

      data.aquarium = new();
    }
  }
}
