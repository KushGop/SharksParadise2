using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{

  public Rigidbody2D rb2d;
  public float speed;

  //Moves in one direction
  private void Update() {
    rb2d.velocity = transform.up * speed;
  }

  //Eaten when player jumps on it
  private void OnTriggerStay2D(Collider2D other)
  {
    if (other.transform.tag == "Player")
    {
      if (other.transform.GetComponent<PlayerMovement>().GetIsJump())
      {
        transform.parent.GetComponent<AbstractFactory>().UpdateObject(transform);
      }
    }
  }

}
