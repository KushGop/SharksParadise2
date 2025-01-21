using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPrefab : MonoBehaviour
{
  public SpriteRenderer sprite;
  public Color newColor;
  public float fadeTime;

  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(FadeOut());
  }

  IEnumerator FadeOut()
  {
    //ugly while, Update would be ideal
    while (sprite.color.a > 0.01)
    {
      sprite.color = Color.Lerp(sprite.color, newColor, fadeTime * Time.deltaTime);
      transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, fadeTime * Time.deltaTime * 0.5f);
      yield return null;
    }
    //code after fading is finished
    Destroy(gameObject);
  }
}
