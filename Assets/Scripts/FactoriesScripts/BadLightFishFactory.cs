using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadLightFishFactory : AbstractFactory
{

  private void Awake()
  {
    GameManager.switchToNight += IncreaseNumEnemy;
  }
  private void OnDestroy()
  {
    GameManager.switchToNight -= IncreaseNumEnemy;
  }

  protected override void SetPreferences()
  {
    newEnemy.transform.name = "BadLight";
    newEnemy.transform.tag = "Predator";
    newEnemy.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f) * Random.Range(player.playerSize / 100, player.playerSize / 100 + sizeOffset);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = "BadLight";
    identifier.fishType = "Predator";
    identifier.value = value;
    identifier.id = i;
  }


}
