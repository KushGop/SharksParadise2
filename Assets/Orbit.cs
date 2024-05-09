using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
  private Quaternion day, night;
  private float cycleTime;

  private void Start()
  {
    day = Quaternion.Euler(0, 0, -180);
    night = Quaternion.Euler(0, 0, -360);
    cycleTime = GameManager.dayCycleTime;
    StartCoroutine(Cycle());
  }

  IEnumerator Cycle()
  {
    float elapsedTime = 0f;
    print("cycle sun");
    while (elapsedTime < cycleTime)
    {
      transform.rotation = Quaternion.Slerp(Quaternion.identity, day, elapsedTime / cycleTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    elapsedTime = 0f;
    print("cycle night");
    while (elapsedTime < cycleTime)
    {
      transform.rotation = Quaternion.Slerp(day, night, elapsedTime / cycleTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    StartCoroutine(Cycle());
  }
}
