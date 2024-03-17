using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaTrigger : MonoBehaviour
{
  public EnemyList list;
  private void OnTriggerExit2D(Collider2D other)
  {
    if (list.playAreaList.Contains(other.transform.tag))
    {
      other.transform.parent.GetComponent<AbstractFactory>().UpdateObject(other.transform);
    }
    else
    {
      Destroy(other.gameObject);
    }
  }
}