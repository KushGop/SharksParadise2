using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Bird";
    newEnemy.transform.tag = "Prey";
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * (player.playerSize / 100);

    
    newEnemy.transform.GetComponent<Identifier>().fishName = "Bird";    
    newEnemy.transform.GetComponent<Identifier>().fishType = "Prey";    
    newEnemy.transform.GetComponent<Identifier>().value = 20;
  }
  
}
