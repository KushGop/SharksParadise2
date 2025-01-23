using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AquariumManager
{
  private static FishCard fishCard;
  public static AquariumData aquariumData;


  public delegate void TryUpgrade();
  public delegate void UpgradeCard(Fishes fish, FishCard_S card);
  public static TryUpgrade tryUpgrade;
  public static TryUpgrade funds;
  public static UpgradeCard upgradeCard;

  public static void IncrementFish(Fishes fish)
  {
    FishCard_S fs = aquariumData.fishCards[fish];
    fs.count++;
    aquariumData.fishCards[fish] = fs;
  }

  //Upgrade button
  public static void HoldCard(FishCard fc)
  {
    fishCard = fc;
  }
  //Yes or No
  public static void SelectUpgrade(bool yes)
  {
    if (yes)
    {
      fishCard.UpgradeFish();
    }
    else
    {
      fishCard = null;
    }
  }
}
