using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    //sortingGroup.sortingLayerName = "Bird";
    newEnemy.transform.name = "Bird";
    newEnemy.transform.tag = "Prey";
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * (player.playerSize / 100);
    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.SEAGULL;
    identifier.fishType = FishType.PREY;
    identifier.value = value + Random.Range(lower, upper);

  }

}
