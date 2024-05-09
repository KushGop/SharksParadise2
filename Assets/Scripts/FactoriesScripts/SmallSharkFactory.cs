using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSharkFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "SmallShark";
    newEnemy.transform.tag = "Prey";
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * Random.Range(player.playerSize / 100 - sizeOffset, player.playerSize / 100);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = "SmallShark";
    identifier.fishType = "Prey";
    identifier.value = value + Random.Range(lower, upper);

  }

}
