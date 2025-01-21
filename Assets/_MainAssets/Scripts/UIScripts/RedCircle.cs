using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCircle : MonoBehaviour
{
  private Color c;
  [SerializeField] SpriteRenderer sprite;

  void Start()
  {
    transform.localScale = Vector3.zero;
    c = Color.white;
    c.a = 0;
    sprite.color = c;
    StartCoroutine(FadeIn());
  }

  IEnumerator FadeIn()
  {
    float elapsedTime = 0f;
    float waitTime = 0.2f;
    while (elapsedTime <= waitTime)
    {
      sprite.color = Color.Lerp(c, Color.white, elapsedTime / waitTime);
      transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.75f, elapsedTime / waitTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    StartCoroutine(FadeOut());
  }
  IEnumerator FadeOut()
  {
    float elapsedTime = 0f;
    float waitTime = 0.2f;
    while (elapsedTime < waitTime)
    {
      sprite.color = Color.Lerp(Color.white, c, elapsedTime / waitTime);
      transform.localScale = Vector3.Lerp(Vector3.one * 0.75f, Vector3.one * 1.5f, elapsedTime / waitTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    Destroy(gameObject);
  }
}
