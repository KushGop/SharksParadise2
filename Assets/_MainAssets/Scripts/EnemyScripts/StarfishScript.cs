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

  private void Start()
  {
    size = colors.colors.Length;
    StartCoroutine(SpriteRotateColors());
  }


  IEnumerator SpriteRotateColors()
  {
    i = 0;
    float timePassed = 0f;
    StartCoroutine(ColorDelay());
    while (true)
    {
      sprite.color = Color.Lerp(sprite.color, colors.colors[i % size], speed * Time.deltaTime);
      glow.color = Color.Lerp(glow.color, colors.colors[i % size], speed * Time.deltaTime);
      timePassed += Time.deltaTime;
      yield return null;
    }
  }

  IEnumerator ColorDelay()
  {
    yield return new WaitForSeconds(0.3f);
    i++;
    StartCoroutine(ColorDelay());
  }
}
