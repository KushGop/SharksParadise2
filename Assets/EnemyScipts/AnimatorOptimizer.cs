using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOptimizer : MonoBehaviour
{

  public SpriteRenderer spriteRenderer;
  public Animator animator;
  // private float speed;

  private void Start() {
    // speed = animator.speed;
    // spriteRenderer.enabled = false;
    animator.enabled = false;
  }

  private void OnBecameVisible()
  {
    // spriteRenderer.enabled = true;
    animator.enabled = true;
  }
  private void OnBecameInvisible() {
    // spriteRenderer.enabled = false;
    animator.enabled = false;
  }
}
