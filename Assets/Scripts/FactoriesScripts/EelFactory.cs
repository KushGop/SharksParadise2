using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Eel";
    newEnemy.transform.tag = "Prey";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f) * Random.Range(player.playerSize / 100 - sizeOffset, player.playerSize / 100);
    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = "Eel";
    identifier.fishType = "Prey";
    identifier.value = 10 + Random.Range(1, 11);

  }
}
