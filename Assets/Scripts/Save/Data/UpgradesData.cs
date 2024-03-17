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

    //Cap at level 5 prestige
    //Coin Upgrades
    upgrades.Add(UpgradeList.baseSpeed, 0);
    upgrades.Add(UpgradeList.boostSpeed, 0);
    upgrades.Add(UpgradeList.boostCost, 0);
    upgrades.Add(UpgradeList.jumpCost, 0);
    upgrades.Add(UpgradeList.refillSpeed, 0);
    upgrades.Add(UpgradeList.refillDelay, 0);

    //Don't reset on prestige
    //Gem Upgrades
    upgrades.Add(UpgradeList.treasureFrequency, 0);
    upgrades.Add(UpgradeList.starfishFrequency, 0);
    upgrades.Add(UpgradeList.warningRadius, 0);
    upgrades.Add(UpgradeList.coinFishFrequency, 0);
    upgrades.Add(UpgradeList.powerTime, 0);


    /*
     * Prestige
     * level 0 - nothing
     * level 1 - one extra treasure chest
     * level 2 - one extra starfish
     * level 3 - extra gems
     * repeat 1 2 3
     * 
     * or prestige token
     */

    upgrades.Add(UpgradeList.prestigeCount, 0);
    upgrades.Add(UpgradeList.greatCoinFishFrequency, 0);
    upgrades.Add(UpgradeList.greatStarfishFrequency, 0);
    upgrades.Add(UpgradeList.greatTreasureFrequency, 0);
  }
}
