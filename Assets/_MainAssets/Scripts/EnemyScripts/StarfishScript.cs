using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishScript : MonoBehaviour
{

  private Color[] colors;
  private int i;
  [SerializeField] SpriteRenderer sprite;
  [SerializeField] SpriteRenderer glow;
  [SerializeField] float speed;

  private void Start()
  {
    colors = new Color[] { Color.cyan, Color.blue, Color.magenta, Color.red, Color.yellow, Color.green };
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
    yield return new WaitForSeconds(0.3f);
    i++;
    StartCoroutine(ColorDelay());
  }
}
