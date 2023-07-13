using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaIndicator : MonoBehaviour
{

  private PlayerMovement movement;

  private void Start() {
    movement = transform.parent.GetComponent<PlayerMovement>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.tag == "Predator")
    {
      movement.enemyCounter(true);
    }
  }
  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.transform.tag == "Predator")
    {
      movement.enemyCounter(false);
    }
  }
}
