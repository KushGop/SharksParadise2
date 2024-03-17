using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFishFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "CoinFish";
    newEnemy.transform.tag = "Prey";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(1, 1, 1);
    isSpecial = true;
    spawnDelayTime = 6f
      - (UpgradesManager.upgradesData.upgrades[UpgradeList.coinFishFrequency] * 0.1f)
      - (UpgradesManager.upgradesData.upgrades[UpgradeList.greatCoinFishFrequency] * 0.5f);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = "CoinFish";
    identifier.fishType = "Prey";
    identifier.value = 15 + Random.Range(1, 11);

  }
}
