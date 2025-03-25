using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFishFactory : AbstractFactory
{
  private void Awake()
  {
    GameManager.switchToNight += IncreaseNumEnemy;
    GameManager.switchToNight += Respawn;
  }
  private void OnDestroy()
  {
    GameManager.switchToNight -= IncreaseNumEnemy;
    GameManager.switchToNight -= Respawn;
  }

  void Start()
  {
    Rarity rarity = AquariumManager.aquariumData.fishRarity[fishName];
    int level = AquariumManager.aquariumData.fishCards[fishName].level;
    value = EnemyList.rarityPoint[rarity][level];
    r = player.r;
    s = player.s;
  }

  private void Respawn()
  {
    while (transform.childCount < numEnemies)
    {
      AddEnemy();
    }
  }

  protected override void SetPreferences()
  {
    newEnemy.transform.name = "LightFish";
    newEnemy.transform.tag = "Prey";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f) * Random.Range(player.playerSize / 100 - sizeOffset, player.playerSize / 100);
    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.CRAWLER;
    identifier.fishType = FishType.PREY;
    identifier.value = value + Random.Range(0, upper);

  }
}
