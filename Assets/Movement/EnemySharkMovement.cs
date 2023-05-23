using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySharkMovement : MonoBehaviour
{
  public Rigidbody2D rb2d;
  public float movementSpeed;
  public float idleSpeed;
  private bool isTrigger, isActive;
  public float rotationSpeed,idleRot;
  public PlayerStats player;

  private void Start()
  {
    isTrigger = false;
  }

  //If player is near, swim towards the player
  private void Update()
  {
    if (isActive)
    {
      if (isTrigger)
      {
        rb2d.velocity = transform.right * movementSpeed;
        Vector3 relativePos = transform.tag == "Predator" ? player.playerPosition - transform.position : transform.position - player.playerPosition;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        RotateEnemy(angle);
      }
      else
      {
        rb2d.velocity =  transform.right * movementSpeed;
        // Idle();
      }
    }
  }

  //sets movement to be active
  public void SetActive(bool b)
  {
    isActive = b;
  }

  //triggers follow mechanic
  public void TriggerAI(bool trigger)
  {
    this.isTrigger = trigger;
  }

  //Rotates enemy
  private void RotateEnemy(float angle)
  {
    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);
  }

  //CHANGE METHOD TO IDLE IN ANY DIRECTION
  // private void Idle()
  // {
  //   rb2d.velocity = transform.right * idleSpeed;
  //   float curr = transform.localRotation.z;
  //   int angle = .5f > curr && curr > -.5f ? 0 : 180;
  //   RotateEnemy(angle);
  // }

  //If a small shark gets eaten, update position
  //If the shark is from the net, destroy object
  // private void OnCollisionEnter2D(Collision2D other)
  // {
  //   if (other.transform.tag == "Player")
  //   {
  //     if (!other.transform.GetComponent<PlayerMovement>().GetIsJump())
  //     {
  //       switch (transform.tag)
  //       {
  //         case "Predator":
  //           break;
  //         case "Prey":
  //           transform.parent.GetComponent<AbstractFactory>().UpdateObject(transform);
  //           break;
  //         case "Food":
  //           Destroy(gameObject);
  //           break;
  //       }
  //     }

  //   }
  // }

}
