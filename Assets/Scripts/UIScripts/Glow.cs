using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
  [SerializeField] SpriteRenderer glow;
  private Color c;

  //private float fadeTime;
  void Start()
  {
    //fadeTime = 3f;
    c = glow.color;
    c.a = .35f;
    glow.color = c;
  }
  //  GameManager.switchToNight += TurnOnGlow;
  //  GameManager.switchToDay += TurnOffGlow;
  //}
  //private void OnDestroy()
  //{
  //  GameManager.switchToNight -= TurnOnGlow;
  //  GameManager.switchToDay -= TurnOffGlow;
  //}

  //private void TurnOnGlow()
  //{
  //  StartCoroutine(FadeGlowIn());
  //}
  //private void TurnOffGlow()
  //{
  //  StartCoroutine(FadeGlowOut());
  //}

  //IEnumerator FadeGlowIn()
  //{
  //  float elaspedTime = 0f;
  //  //Night Time
  //  while (elaspedTime <= fadeTime)
  //  {
  //    if (Time.timeScale != 0)
  //    {
  //      elaspedTime += Time.deltaTime;
  //      c.a = Mathf.Lerp(0, 0.35f, elaspedTime / fadeTime);
  //    }
  //    yield return null;
  //  }
  //}
  //IEnumerator FadeGlowOut()
  //{
  //  float elaspedTime = 0f;
  //  //Night Time
  //  while (elaspedTime <= fadeTime)
  //  {
  //    if (Time.timeScale != 0)
  //    {
  //      elaspedTime += Time.deltaTime;
  //      c.a = Mathf.Lerp(0.35f, 0, elaspedTime / fadeTime);
  //    }
  //    yield return null;
  //  }
  //}
}
