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
    newEnemy.transform.name = "SharkNet";
    newEnemy.transform.tag = "Food";
    newEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * 0.2f;
    newEnemy.transform.GetChild(0).gameObject.SetActive(false);

    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.STARFISH;
    identifier.fishType = FishType.OBJECT;
    identifier.value = 5;

    newEnemy.transform.GetComponent<AbstractMovement>().enabled = false;
  }

  //Use origin from boat to get new spawn origin
  protected override Vector3 SetPosition()
  {
    r = newR;
    s = newS;
    return base.SetPosition();
  }

  //Activate script when sharks are released from the net
  public void ActivateChildren()
  {
    foreach (Transform e in transform)
    {
      e.GetComponent<AbstractMovement>().enabled = true;
      e.GetComponent<Identifier>().fishType = FishType.FOOD;
      e.GetChild(0).gameObject.SetActive(true);
    }
  }

}
