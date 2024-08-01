using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSprite : MonoBehaviour
{
  private Color color;
  private float alpha;
  private float fadeTime;
  [SerializeField] private SpriteRenderer sprite;
  private void Start()
  {
    color = sprite.color;
    alpha = color.a;
    fadeTime = 3f;
    GameManager.switchToDay += FadeOut;
  }
  private void OnDestroy()
  {
    GameManager.switchToDay -= FadeOut;
    
  }

  void FadeOut()
  {
    StartCoroutine(Fade(alpha, 0));
  }

  IEnumerator Fade(float start, float end)
  {
    float elaspedTime = 0f;
    while (elaspedTime <= fadeTime)
    {
      if (Time.timeScale != 0)
      {
        color.a = Mathf.Lerp(start, end, elaspedTime / fadeTime);
        sprite.color = color;
        elaspedTime += Time.deltaTime;
      }
      yield return null;
    }
  }
}
