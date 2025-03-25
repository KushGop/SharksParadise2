using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Jelly";
    newEnemy.transform.tag = "Prey";
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * (player.playerSize / 100);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.JELLY;
    identifier.fishType = FishType.PREY;
    identifier.value = value + Random.Range(0, upper);
  }
}
