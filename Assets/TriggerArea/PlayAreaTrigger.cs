using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaTrigger : MonoBehaviour
{
  public EnemyList list;
  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.TryGetComponent(out Identifier id))
    {
      if (list.playAreaList.Contains(id.fishType))
      {
        if (other.transform.parent.TryGetComponent<AbstractFactory>(out AbstractFactory a))
        {
          a.UpdateObject(other.transform);
        }
        else
        {
          print(other.name);
          Debug.Log(id.fishType + " " + id.fishName, other);
        }
      }

      else if (list.spawnList.Contains(id.fishType))
        if (id.fishType == FishType.TREASURE)
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
}