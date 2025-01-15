using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadLightFishFactory : AbstractFactory
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
    newEnemy.transform.name = "BadLight";
    newEnemy.transform.tag = "Predator";
    newEnemy.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f) * Random.Range(player.playerSize / 100, player.playerSize / 100 + sizeOffset);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.CUCUMBER;
    identifier.fishType = FishType.PREDATOR;
    identifier.value = value;
    identifier.id = i;
  }


}
