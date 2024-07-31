using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
  public List<string> objectToDetect;
  private string fishName, objectName;
  private JellyMovement jelly;
  private AbstractMovement movement;

  private void Start()
  {
    fishName = transform.GetComponentInParent<Identifier>().fishName;
    switch (fishName)
    {
      case "Jelly":
        jelly = transform.GetComponentInParent<JellyMovement>();
        break;
      case "Fish":
        movement = transform.GetComponentInParent<AbstractMovement>();
        break;
    }
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    objectName = other.transform.GetComponent<Identifier>().fishName;
    if (objectToDetect.Contains(objectName))
    {
      switch (fishName)
      {
        case "Jelly":
          jelly.RotateEnemy(180);
          break;
        case "Fish":
          if (!movement.getTrigger())
            movement.RotateEnemy(180);
          break;
      }
    }
  }
}
