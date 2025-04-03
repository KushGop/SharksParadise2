using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaTrigger : MonoBehaviour
{
  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.TryGetComponent(out Identifier id))
    {
      if (EnemyList.playAreaList.Contains(id.fishType))
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

      else if (EnemyList.spawnList.Contains(id.fishType))
      {
        if (id.fishType == FishType.TREASURE)
        {
          if (!other.transform.GetComponent<Treasure>().isCollected)
          {
            other.transform.parent.GetComponent<AbstractFactory>().UpdateObject(other.transform);
          }
        }
      }
      else if (id.fishType != FishType.STUN)
        Destroy(other.gameObject);
    }
  }
}