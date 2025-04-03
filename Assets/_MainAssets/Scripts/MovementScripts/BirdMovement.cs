using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : AbstractMovement
{

  //Moves in one direction
  protected override void Update()
  {
    Move();
  }

  protected override void Move()
  {
    rb2d.velocity = transform.up * movementSpeed;
  }

  protected override void OnCollisionEnter2D(Collision2D other)
  { }
  protected override void OnTriggerExit2D(Collider2D other)
  { }
}
