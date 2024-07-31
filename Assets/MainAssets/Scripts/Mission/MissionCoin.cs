using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCoin : MonoBehaviour
{
  [SerializeField] private Image sprite;
  [SerializeField] private CircleCollider2D collider2d;
  private Color newColor = new Color32(255, 255, 255, 255);
  private Color startColor = new Color32(255, 255, 255, 0);
  [SerializeField] private Vector3 endPosition;

  private void Start()
  {
    endPosition = transform.parent.parent.GetComponent<PopulateScoreMissions>().coinTarget.transform.position;
    sprite.color = startColor;
    StartCoroutine(FadeIn());
  }

  IEnumerator FadeIn()
  {
    yield return new WaitForSeconds(Random.Range(0, 0.2f));
    float waitTime = 3f;
    while (sprite.color.a < 0.99f)
    {
      sprite.color = Color.Lerp(sprite.color, newColor, Time.deltaTime * waitTime);
      yield return null;
    }
    StartCoroutine(Collect());
  }

  IEnumerator Collect()
  {
    float waitTime = Random.Range(2f, 3f);
    while (true)
    {
      transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * waitTime);
      yield return null;
    }
  }
}
