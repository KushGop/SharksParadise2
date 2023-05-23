using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaIndicator : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.tag == "Predator")
    {
      transform.parent.GetComponent<PlayerMovement>().enemyCounter(true);
    }
  }
  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.transform.tag == "Predator")
    {
      transform.parent.GetComponent<PlayerMovement>().enemyCounter(false);
    }
  }
}
