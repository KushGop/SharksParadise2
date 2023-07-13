using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSharkFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Shark";
    newEnemy.transform.tag = "Prey";
    newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * Random.Range(player.playerSize / 100 - sizeOffset, player.playerSize / 100);  
    
    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = "SmallShark";    
    identifier.fishType = "Prey";    
    identifier.value =  50 +  Random.Range(1,9);

  }

}
