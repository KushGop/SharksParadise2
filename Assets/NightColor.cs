using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightColor : MonoBehaviour
{
  [SerializeField] private Color nightColor;
  [SerializeField] protected SpriteRenderer sprite;
  private float fadeTime;

  protected void Start()
  {
    fadeTime = 3f;
    GameManager.switchToNight += TurnOnGlow;
    GameManager.switchToDay += TurnOffGlow;
  }

  protected void OnDestroy()
  {
    GameManager.switchToNight -= TurnOnGlow;
    GameManager.switchToDay -= TurnOffGlow;
  }

  public void QuickNightSet()
  {
    sprite.color = nightColor;
  }

  protected void TurnOnGlow()
  {
    StartCoroutine(FadeGlowIn());
  }
  protected void TurnOffGlow()
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
}
