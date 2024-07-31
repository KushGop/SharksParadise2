using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidMovement : AbstractMovement
{

  public Animator anim;
  private Transform inkParent;
  public GameObject ink;
  private bool isIdle;

  private new void Start()
  {
    isIdle = true;
    idleSpeed = movementSpeed;
    inkParent = transform.parent.GetChild(0);
  }

  public override void TriggerAI(bool trigger)
  {
    base.TriggerAI(trigger);
    if (trigger && isIdle)
    {
      isIdle = false;
      anim.SetTrigger("Ink");
      // sound.playSquirt();
      StartCoroutine(InkDelay());
      movementSpeed = idleSpeed + 5f;
      StartCoroutine(Delay());
    }
  }

  IEnumerator Delay()
  {
    yield return new WaitForSeconds(4f);
    if (isTriggered)
    {
      StartCoroutine(Delay());
    }
    else
    {
      anim.SetTrigger("SlowDown");
      movementSpeed = idleSpeed;
      isIdle = true;
    }
  }

  IEnumerator InkDelay()
  {
    yield return new WaitForSeconds(0.5f);
    Instantiate(ink, transform.position, transform.rotation, inkParent);
  }

}
