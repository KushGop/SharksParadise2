using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Spike";
    newEnemy.transform.tag = "Prey";
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * Random.Range(player.playerSize / 100 - sizeOffset, player.playerSize / 100);


    newEnemy.transform.GetComponent<Identifier>().fishName = Fishes.STARFISH;
    newEnemy.transform.GetComponent<Identifier>().fishType = FishType.PREY;
    newEnemy.transform.GetComponent<Identifier>().value = 30;
  }

}
