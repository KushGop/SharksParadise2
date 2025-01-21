using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : AbstractMovement
{

  [SerializeField] private Color nightColor;
  [SerializeField] private SpriteRenderer sprite;
  private float fadeTime;

  private new void Start()
  {
    sprite.color = Color.white;
    fadeTime = 3f;
    GameManager.switchToNight += TurnOnGlow;
    GameManager.switchToDay += TurnOffGlow;
  }

  private void OnDestroy()
  {
    GameManager.switchToNight -= TurnOnGlow;
    GameManager.switchToDay -= TurnOffGlow;
  }

  private void TurnOnGlow()
  {
    StartCoroutine(FadeGlowIn());
  }
  private void TurnOffGlow()
  {
    StartCoroutine(FadeGlowOut());
  }

  IEnumerator FadeGlowIn()
  {
    float elaspedTime = 0f;
    //Night Time
    while (elaspedTime <= fadeTime)
    {
      if (Time.timeScale != 0)
      {
        elaspedTime += Time.deltaTime;
        sprite.color = Color.Lerp(Color.white, nightColor, elaspedTime / fadeTime);
      }
      yield return null;
    }
  }
  IEnumerator FadeGlowOut()
  {
    float elaspedTime = 0f;
    //Night Time
    while (elaspedTime <= fadeTime)
    {
      if (Time.timeScale != 0)
      {
        elaspedTime += Time.deltaTime;
        sprite.color = Color.Lerp(nightColor, Color.white, elaspedTime / fadeTime);
      }
      yield return null;
    }
  }

  //Moves in one direction
  protected override void Update()
  {
    Move();
  }

  protected override void Move()
  {
    rb2d.velocity = transform.up * movementSpeed;
  }

  protected override void OnCollisionEnter2D(Collision2D other)
  { }
  protected override void OnTriggerExit2D(Collider2D other)
  { }
}
