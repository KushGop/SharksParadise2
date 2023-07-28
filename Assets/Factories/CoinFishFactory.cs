using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFishFactory : AbstractFactory
{
    protected override void SetPreferences()
  {
    newEnemy.transform.name = "CoinFish";
    newEnemy.transform.tag = "Special";
    // newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(1, 1, 1);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = "CoinFish";    
    identifier.fishType = "Special";    
    identifier.value = 15 +  Random.Range(1,11);
    
  }
}
