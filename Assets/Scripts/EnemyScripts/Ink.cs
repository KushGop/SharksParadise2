using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{

  public SpriteRenderer sprite;
  private Color newColor = new Color32(56, 56, 56, 0);
  private float delayTime = 2f;

  // Start is called before the first frame update
  void Start()
  {
    transform.localScale = Vector3.zero;
    sprite.color = new Color32(56, 56, 56, 170);
    StartCoroutine(GrowInk());
  }

  IEnumerator GrowInk()
  {
    float elapsedTime = 0;
    float waitTime = delayTime;
    while (elapsedTime < waitTime)
    {
      transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.75f, elapsedTime / waitTime);
      elapsedTime += Time.deltaTime;

      // Yield here
      yield return null;
    }
    yield return null;
    StartCoroutine(Delay());
  }
  IEnumerator Delay()
  {
    yield return new WaitForSeconds(delayTime);
    StartCoroutine(FadeAway());
  }
  IEnumerator FadeAway()
  {
    float waitTime = 3f;
    while (sprite.color.a > 0.01)
    {
      sprite.color = Color.Lerp(sprite.color, newColor, waitTime * Time.deltaTime);

      yield return null;
    }
    Destroy(gameObject);
  }
}
