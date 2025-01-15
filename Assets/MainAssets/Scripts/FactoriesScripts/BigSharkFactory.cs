using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSharkFactory : AbstractFactory
{
  private void Awake()
  {
    GameManager.switchToDay += IncreaseNumEnemy;
  }
  private void OnDestroy()
  {
    GameManager.switchToDay -= IncreaseNumEnemy;
  }

  protected override void SetPreferences()
  {
    newEnemy.transform.name = "BigShark";
    newEnemy.transform.tag = "Predator";
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * Random.Range(player.playerSize / 100, player.playerSize / 100 + sizeOffset);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.SHARK_L;
    identifier.fishType = FishType.PREDATOR;
    identifier.value = value + Random.Range(lower, upper);
    identifier.id = i;
  }

}
