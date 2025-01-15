using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
  public List<FishType> objectToDetect;
  private Identifier fish, otherFish;
  private JellyMovement jelly;
  private AbstractMovement movement;

  private void Start()
  {
    fish = transform.GetComponentInParent<Identifier>();
    if (fish.fishName == Fishes.JELLY)
      jelly = transform.GetComponentInParent<JellyMovement>();
    else if (fish.fishType == FishType.SENSOR)
      movement = transform.GetComponentInParent<AbstractMovement>();
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    otherFish = other.transform.GetComponent<Identifier>();
    if (objectToDetect.Contains(otherFish.fishType))
    {
      if (fish.fishName == Fishes.JELLY)
        jelly.RotateEnemy(180);
      else if (fish.fishType == FishType.SENSOR)
        if (!movement.getTrigger())
          movement.RotateEnemy(180);
    }
  }
}

