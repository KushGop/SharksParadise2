using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradesData
{
  public SerializableDictionary<UpgradeList, int> upgrades;

  public UpgradesData()
  {
    upgrades = new();

    //Coin Upgrades
    upgrades.Add(UpgradeList.baseSpeed, 0);
    upgrades.Add(UpgradeList.boostSpeed, 0);
    upgrades.Add(UpgradeList.boostCost, 0);
    upgrades.Add(UpgradeList.airTime, 0);
    upgrades.Add(UpgradeList.jumpCost, 0);

    //Gem Upgrades
    upgrades.Add(UpgradeList.starfishFrequency, 0);
    upgrades.Add(UpgradeList.warningRadius, 0);
    upgrades.Add(UpgradeList.coinFishCount, 0);
    upgrades.Add(UpgradeList.treasureFrequency, 0);


    /*
     * Prestige
     * level 0 - nothing
     * level 1 - one extra starfish
     * level 2 - one extra treasure chest
     * level 3 - extra gems
     * repeat 1 2 3
     */

    upgrades.Add(UpgradeList.prestigeCount, 0);
    upgrades.Add(UpgradeList.starfishCount, 0);
    upgrades.Add(UpgradeList.treasureCount, 0);
  }
}
