using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbstractMovement : MonoBehaviour
{
  public Rigidbody2D rb2d;
  public float movementSpeed;
  public float idleSpeed;
  protected bool isTriggered, isActive;
  public float rotationSpeed, idleRot;
  public PlayerStats player;
  protected FishType fishType;

  protected void Start()
  {
    isTriggered = false;
    isActive = true;
    fishType = transform.GetComponent<Identifier>().fishType;
    //Move();
  }

  //If player is near, swim towards the player
  //FIX UPDATE, EXPENSIVE FOR NO REASON, LINE 28 & 29
  protected virtual void Update()
  {
    Move();
    if (isTriggered)
    {
      Vector3 relativePos = fishType == FishType.PREDATOR ? player.playerPosition - transform.position : transform.position - player.playerPosition;
      float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
      RotateEnemy(angle);
    }
    //else
    //{

    //}
  }

  protected virtual void Move()
  {
    rb2d.velocity = transform.right * movementSpeed;
  }

  //Rotates enemy
  public void RotateEnemy(float angle)
  {
    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);
  }

  //triggers follow mechanic
  public virtual void TriggerAI(bool trigger)
  {
    isTriggered = trigger;
  }
  public bool getTrigger()
  {
    return isTriggered;
  }
  //Go through boat
  protected virtual void OnCollisionEnter2D(Collision2D other)
  {
    if (other.transform.GetComponent<Identifier>().fishType == FishType.OBJECT && isActive)
    {
      foreach (Collider2D cc in transform.GetComponents<Collider2D>())
      {
        cc.isTrigger = true;
      }
      return;
    }
  }
  protected virtual void OnTriggerExit2D(Collider2D other)
  {
    if (other.transform.GetComponent<Identifier>().fishType == FishType.OBJECT)
    {
      foreach (Collider2D cc in transform.GetComponents<Collider2D>())
      {
        cc.isTrigger = false;
      }
      return;
    }
  }

}
