using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
  [SerializeField] private GameObject ding;
  [SerializeField] private SpriteRenderer sprite;
  [SerializeField] private CircleCollider2D collider2d;
  private Color newColor = new Color32(255, 255, 255, 0);

  private void Start() {
    StartCoroutine(FadeAway());
  }

  public void Collected()
  {
    Instantiate(ding,transform.parent);
    sprite.enabled = false;
    Destroy(gameObject);
  }

  IEnumerator FadeAway()
  {
    yield return new WaitForSeconds(5);
    float waitTime = 3f;
    while (sprite.color.a > 0.01)
    {
      sprite.color = Color.Lerp(sprite.color, newColor, Time.deltaTime * waitTime);
      yield return null;
    }
    Destroy(gameObject);
  }
}
