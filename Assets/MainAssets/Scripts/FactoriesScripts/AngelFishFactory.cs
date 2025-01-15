using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelFishFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "AngelFish";
    newEnemy.transform.tag = "Prey";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(1, 1, 1) * Random.Range(0.2f, 0.4f);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.ANGEL;
    identifier.fishType = FishType.PREY;
    identifier.value = value + Random.Range(lower, upper);

  }
}
