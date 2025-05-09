using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadLightFishMovement : AbstractMovement
{
  [SerializeField] Collider2D col;

  public void Awake()
  {
    GameManager.switchToDay += LateDestroy;
  }

  private void LateDestroy()
  {
    col.enabled = false;
    GameManager.switchToDay -= LateDestroy;
    Destroy(gameObject, 3f);
  }


}
