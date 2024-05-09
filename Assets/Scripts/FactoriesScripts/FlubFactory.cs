using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlubFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Flub";
    newEnemy.transform.tag = "Prey";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f) * Random.Range(player.playerSize / 100 - sizeOffset, player.playerSize / 100);
    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = "Flub";
    identifier.fishType = "Prey";
    identifier.value = value + Random.Range(lower, upper);

  }
}
