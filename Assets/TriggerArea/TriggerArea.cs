using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
  public EnemyList list;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (list.triggerAreaList.Contains(other.transform.name))
    {
      other.GetComponent<AbstractMovement>().TriggerAI(true);
    }
  }
  private void OnTriggerExit2D(Collider2D other)
  {
    if (list.triggerAreaList.Contains(other.transform.name))
    {
      other.GetComponent<AbstractMovement>().TriggerAI(false);
    }
  }
}
