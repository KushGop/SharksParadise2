using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Starfish";
    newEnemy.transform.tag = "Starfish";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    isSpecial = true;
    spawnDelayTime = 15f
      - (UpgradesManager.upgradesData.upgrades[UpgradeList.starfishFrequency] * 0.1f)
      - (UpgradesManager.upgradesData.upgrades[UpgradeList.greatStarfishFrequency] * 0.5f);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.STARFISH;
    identifier.fishType = FishType.STARFISH;
    identifier.value = value;

  }
}
