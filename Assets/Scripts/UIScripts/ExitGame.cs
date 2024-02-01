using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{

  [SerializeField] Collider2D playerCollider;

  public void ExitGameSequence()
  {
    playerCollider.enabled = false;
  }
}
