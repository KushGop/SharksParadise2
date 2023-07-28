using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkFactory : AbstractFactory
{
  public float newR;
  public float newS;
  public bool hasCollider;
  private float delayTime = 1f;
  private CircleCollider2D circle;
  // Start is called before the first frame update
  void Start()
  {
    r = newR;
    s = newS;
    for (int i = 0; i < numEnemies; i++)
    {
      StartCoroutine(InkSpawnDelay());
    }
    if (hasCollider)
    {
      circle = transform.GetComponent<CircleCollider2D>();
      circle.radius = 0;
      StartCoroutine(GrowCollider());
    }

  }

  IEnumerator InkSpawnDelay()
  {
    yield return new WaitForSeconds(0.2f);
    AddEnemy();
  }

  protected override void UpdateOrigin()
  {
    origin = transform.position;
  }

  IEnumerator GrowCollider()
  {
    float elapsedTime = 0;
    float waitTime = delayTime;
    while (elapsedTime < waitTime)
    {
      circle.radius = Mathf.Lerp(0, 0.75f, (elapsedTime / waitTime));
      elapsedTime += Time.deltaTime;

      // Yield here
      yield return null;
    }
    yield return null;
    StartCoroutine(Delay());
  }
  IEnumerator Delay()
  {
    yield return new WaitForSeconds(8f);
    Destroy(gameObject);
  }

}
