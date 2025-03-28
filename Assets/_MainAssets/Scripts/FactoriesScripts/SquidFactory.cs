using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Squid";
    newEnemy.transform.tag = "Prey";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * Random.Range(0.2f, 0.4f);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.SQUID;
    identifier.fishType = FishType.PREY;
    identifier.value = value + Random.Range(0, upper);

  }
}
