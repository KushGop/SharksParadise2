using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "GoldFish";
    newEnemy.transform.tag = "Prey";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(1, 1, 1) * Random.Range(player.playerSize / 100 - sizeOffset, player.playerSize / 100);
    newEnemy.transform.GetComponent<EnemySharkMovement>().SetActive(true);

    newEnemy.transform.GetComponent<Identifier>().fishName = "GoldFish";    
    newEnemy.transform.GetComponent<Identifier>().fishType = "Prey";    
    newEnemy.transform.GetComponent<Identifier>().value = 10 +  Random.Range(1,11);

  }
}
