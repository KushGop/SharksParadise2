using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSharkFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "BigShark";
    newEnemy.transform.tag = "Predator";
    newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * Random.Range(player.playerSize / 100, player.playerSize / 100 + sizeOffset);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = "BigShark";
    identifier.fishType = "Predator";
    identifier.value = 1000;
    identifier.id = i;
  }

}
