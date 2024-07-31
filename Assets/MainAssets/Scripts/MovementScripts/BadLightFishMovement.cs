using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadLightFishMovement : AbstractMovement
{
  [SerializeField] Collider2D col;

  protected new void Start()
  {
    isTriggered = false;
    isActive = true;
    fishType = transform.GetComponent<Identifier>().fishType;
    GameManager.switchToDay += LateDestroy;
  }

  private void OnDestroy()
  {
    GameManager.switchToDay -= LateDestroy;
  }

  private void LateDestroy()
  {
    col.enabled = false;
    Destroy(gameObject, 3f);
  }


}
