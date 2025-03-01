using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFishMovement : AbstractMovement
{

  private Transform CoinParent;
  public GameObject Coin;
  public SpriteRenderer sprite;
  private CapsuleCollider2D col;
  private Color newColor = new Color32(255, 255, 255, 0);
  private bool hasBeenTriggered;

  private new void Start()
  {
    hasBeenTriggered = false;
    CoinParent = transform.parent.GetChild(0);
    col = transform.GetComponent<CapsuleCollider2D>();
  }

  protected override void Update()
  {
    if (isTriggered)
    {
      Move();
      Vector3 relativePos = transform.position - player.playerPosition;
      float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
      RotateEnemy(angle);
    }
    else
    {
      rb2d.velocity = transform.right * idleSpeed;
    }
  }

  public override void TriggerAI(bool trigger)
  {
    if (trigger && !hasBeenTriggered)
    {
      isTriggered = trigger;
      hasBeenTriggered = true;
      StartCoroutine(Coins());
      StartCoroutine(Delay());
    }
  }

  IEnumerator Coins()
  {
    Instantiate(Coin, transform.position, transform.rotation, CoinParent);
    yield return new WaitForSeconds(0.2f);
    StartCoroutine(Coins());
  }
  IEnumerator Delay()
  {
    yield return new WaitForSeconds(5f);
    StopCoroutine(Coins());
    // col.enabled = false;
    col.isTrigger = true;
    StartCoroutine(FadeAway());
  }

  IEnumerator FadeAway()
  {
    float waitTime = 3f;
    while (sprite.color.a > 0.01)
    {
      sprite.color = Color.Lerp(sprite.color, newColor, Time.deltaTime * waitTime);
      yield return null;
    }
    transform.parent.GetComponent<AbstractFactory>().SpawnObject(transform);
  }


}
