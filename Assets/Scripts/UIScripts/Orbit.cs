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
    if (!TutorialManager.isInTutorial)
    {
      cycleTime = GameManager.dayCycleTime;
      StartCoroutine(Cycle());
    }
  }
  private void OnEnable()
  {
    if (TutorialManager.isInTutorial)
      TutorialManager.night += StartOrbit;
  }
  private void OnDestroy()
  {
    if (TutorialManager.isInTutorial)
      TutorialManager.night -= StartOrbit;
  }

  private void StartOrbit()
  {
    cycleTime = 6f;
    StartCoroutine(Cycle());
  }

  IEnumerator Cycle()
  {
    float elapsedTime = 0f;
    while (elapsedTime < cycleTime)
    {
      transform.rotation = Quaternion.Slerp(Quaternion.identity, day, elapsedTime / cycleTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    elapsedTime = 0f;
    while (elapsedTime < cycleTime)
    {
      transform.rotation = Quaternion.Slerp(day, night, elapsedTime / cycleTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    if (!TutorialManager.isInTutorial)
      StartCoroutine(Cycle());
  }
}
