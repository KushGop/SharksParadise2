using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantaRayFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "MantaRay";
    newEnemy.transform.tag = "Prey";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f) * Random.Range(player.playerSize / 100 - sizeOffset, player.playerSize / 100);
    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.MANTA;
    identifier.fishType = FishType.PREY;
    identifier.value = value + Random.Range(0, upper);

  }
}
