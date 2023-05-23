using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSharkFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Shark";
    newEnemy.transform.tag = "Predator";
    newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
    newEnemy.transform.localScale = new Vector3(-1,1,1) * Random.Range(player.playerSize / 100, player.playerSize / 100 + sizeOffset);
    newEnemy.transform.GetComponent<EnemySharkMovement>().SetActive(true);
    
    newEnemy.transform.GetComponent<Identifier>().fishName = "Shark";    
    newEnemy.transform.GetComponent<Identifier>().fishType = "Predator";    
    newEnemy.transform.GetComponent<Identifier>().value = 120 +  Random.Range(1,11);
  }

}
