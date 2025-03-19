using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOptimizer : MonoBehaviour
{

  public SpriteRenderer spriteRenderer;
  public Animator animator;
  public Animation anim;
  // private float speed;

  private void Start()
  {
    // speed = animator.speed;
    //spriteRenderer.enabled = false;
    animator.enabled = false;
    anim.animateOnlyIfVisible = true;
  }

  private void OnBecameVisible()
  {
    animator.enabled = true;
  }
  private void OnBecameInvisible()
  {
    animator.enabled = false;
  }
}
