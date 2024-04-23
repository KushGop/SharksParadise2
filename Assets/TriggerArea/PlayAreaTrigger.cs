using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaTrigger : MonoBehaviour
{
  public EnemyList list;
  private void OnTriggerExit2D(Collider2D other)
  {
    if (list.playAreaList.Contains(other.tag))
      other.transform.parent.GetComponent<AbstractFactory>().UpdateObject(other.transform);
    else if (list.spawnList.Contains(other.tag))
      if (other.CompareTag("Treasure"))
      {
        if (!other.transform.GetComponent<Treasure>().isCollected)
        {
          other.transform.parent.GetComponent<AbstractFactory>().UpdateObject(other.transform);
        }
      }
      else
        Destroy(other.gameObject);
  }
}