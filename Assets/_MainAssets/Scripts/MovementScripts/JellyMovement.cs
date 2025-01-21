using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMovement : AbstractMovement
{
  [SerializeField] private Animator shock;
  [SerializeField] private AudioSource shockSound;

  internal void Shock()
  {
    shock.SetTrigger("PlayShock");
    shockSound.Play();
  }
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
