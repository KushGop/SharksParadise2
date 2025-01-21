using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AquariumManager
{

  public static AquariumData aquariumData;

  public static void IncrementFish(Fishes fish)
  {
    FishCard_S fs = aquariumData.fishCards[fish];
    fs.count++;
    aquariumData.fishCards[fish] = fs;
  }
}
