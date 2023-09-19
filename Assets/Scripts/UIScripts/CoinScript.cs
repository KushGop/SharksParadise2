using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
  [SerializeField] private AudioSource ding;
  [SerializeField] private SpriteRenderer sprite;
  [SerializeField] private CircleCollider2D collider2d;

  public void Collected(){
    ding.Play();
    sprite.enabled = false;
    collider2d.enabled = false;
    Destroy(gameObject,1f);
  }
}
