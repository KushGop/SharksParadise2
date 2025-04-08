using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishScript : MonoBehaviour
{
  [SerializeField] Colors_SO colors;
  private int i, size;
  [SerializeField] SpriteRenderer sprite;
  [SerializeField] SpriteRenderer glow;
  [SerializeField] float speed;
  float waitTime = 0.4f;

  private void Start()
  {
    i = 0;
    size = colors.colors.Length;
    StartCoroutine(SpriteRotateColors());
  }


  IEnumerator SpriteRotateColors()
  {
    for (float elapsedTime = 0; elapsedTime < waitTime; elapsedTime += Time.deltaTime)
    {
      sprite.color = Color.Lerp(sprite.color, colors.colors[i % size], elapsedTime / waitTime);
      glow.color = Color.Lerp(glow.color, colors.colors[i % size], elapsedTime / waitTime);
      yield return null;
    }
    i = (i + 1) % size;
    StartCoroutine(SpriteRotateColors());
  }
}
