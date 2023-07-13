using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkFactory : AbstractFactory
{
  public float newR;
  public float newS;
  // Start is called before the first frame update
  void Start()
  {
    r = newR;
    s = newS;
    for (int i = 0; i < numEnemies; i++)
    {
      StartCoroutine(InkSpawnDelay());
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
}
