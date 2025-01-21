using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Treasure";
    newEnemy.transform.tag = "Treasure";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(1, 1, 1);
    isSpecial = true;
    spawnDelayTime = 15f
      - (UpgradesManager.upgradesData.upgrades[UpgradeList.treasureFrequency] * 0.1f)
      - (UpgradesManager.upgradesData.upgrades[UpgradeList.greatTreasureFrequency] * 0.5f);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.TREASURE;
    identifier.fishType = FishType.TREASURE;
    identifier.value = 0;

  }
}
