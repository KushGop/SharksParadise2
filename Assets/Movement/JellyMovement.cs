using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMovement : MonoBehaviour
{
  public Rigidbody2D rb2d;
  public float speed;

  private void Update()
  {
    rb2d.velocity = transform.up * speed;
  }

  //Stuns player if they touch tentacles
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.tag == "Player")
    {
      if (!other.transform.GetComponent<PlayerMovement>().GetIsJump())
      {
        Debug.Log("Stun");
        other.transform.GetComponent<PlayerMovement>().Stun();
      }
    }
  }
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.transform.tag == "Player")
    {
      transform.parent.GetComponent<AbstractFactory>().UpdateObject(transform);
    }
  }


}
