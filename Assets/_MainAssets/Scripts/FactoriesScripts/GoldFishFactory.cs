using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "ClownFish";
    newEnemy.transform.tag = "Prey";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(1, 1, 1) * Random.Range(player.playerSize / 100 - sizeOffset, player.playerSize / 100);
    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.CLOWN;
    identifier.fishType = FishType.PREY;
    identifier.value = value + Random.Range(0, upper);

  }
}
