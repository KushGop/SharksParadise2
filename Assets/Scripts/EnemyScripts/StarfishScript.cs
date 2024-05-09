using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishScript : MonoBehaviour
{

  [SerializeField] private Color[] colors;
  private int i;
  [SerializeField] SpriteRenderer sprite;
  [SerializeField] SpriteRenderer glow;
  [SerializeField] float speed;

  private void Start()
  {
    StartCoroutine(SpriteRotateColors());
  }


  IEnumerator SpriteRotateColors()
  {
    i = 0;
    float timePassed = 0f;
    StartCoroutine(ColorDelay());
    while (true)
    {
      sprite.color = Color.Lerp(sprite.color, colors[i % 6], speed * Time.deltaTime);
      glow.color = Color.Lerp(glow.color, colors[i % 6], speed * Time.deltaTime);
      timePassed += Time.deltaTime;
      yield return null;
    }
  }

  IEnumerator ColorDelay()
  {
    yield return new WaitForSeconds(0.2f);
    i++;
    StartCoroutine(ColorDelay());
  }
}
