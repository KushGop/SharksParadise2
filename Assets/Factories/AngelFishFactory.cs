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
    newEnemy.transform.localScale = new Vector3(1, 1, 1) * Random.Range(0.2f,0.4f);
    newEnemy.transform.GetComponent<EnemySharkMovement>().SetActive(true);

    newEnemy.transform.GetComponent<Identifier>().fishName = "AngelFish";    
    newEnemy.transform.GetComponent<Identifier>().fishType = "Prey";    
    newEnemy.transform.GetComponent<Identifier>().value = 15 +  Random.Range(1,11);
  }
}
