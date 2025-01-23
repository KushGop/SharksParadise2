using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.TryGetComponent(out Identifier id))
    {
      if (EnemyList.triggerAreaList.Contains(id.fishName))
      {
        if (other.TryGetComponent(out AbstractMovement a))
          a.TriggerAI(true);
      }
    }
  }
  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.TryGetComponent(out Identifier id))
    {
      if (EnemyList.triggerAreaList.Contains(id.fishName))
      {
        if (other.TryGetComponent(out AbstractMovement a))
          a.TriggerAI(false);
      }
    }
  }
}
