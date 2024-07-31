using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
  //Player can't jump over boat
  private void OnTriggerEnter2D(Collider2D other) {
    if(other.transform.tag == "Player"){
      other.transform.GetComponent<PolygonCollider2D>().isTrigger = false;
    }
  }
  
  private void OnTriggerStay2D(Collider2D other) {
    if(other.transform.tag == "Player"){
      other.transform.GetComponent<PolygonCollider2D>().isTrigger = false;
      // other.transform.GetComponentInParent<PlayerMovement>().setIsOverBoat(true);
    }
  }
}
