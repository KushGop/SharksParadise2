using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNetFactory : AbstractFactory
{
  public float newR;
  public float newS;
  public Transform boatOrigin;
  public float x, y;
  
  protected override void UpdateOrigin()
  {
    origin = boatOrigin.position;
  }

  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Shark";
    newEnemy.transform.tag = "Food";
    newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * 0.2f;
    newEnemy.transform.GetComponent<EnemySharkMovement>().SetActive(false);

    
    newEnemy.transform.GetComponent<Identifier>().fishName = "Shark";    
    newEnemy.transform.GetComponent<Identifier>().fishType = "Food";    
    newEnemy.transform.GetComponent<Identifier>().value = 5;
  }

  //Use origin from boat to get new spawn origin
  protected override Vector3 SetPosition()
  {
    r = newR;
    s = newS;
    return base.SetPosition();
  }

  //Activate script when sharks are released from the net
  public void ActivateChildren(){
    foreach (Transform e in transform)
    {
      e.GetComponent<EnemySharkMovement>().SetActive(true);
    }
  }

}
