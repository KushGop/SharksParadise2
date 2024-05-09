using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCycle : MonoBehaviour
{
  float fadeTime;
  private float dayTime;

  [SerializeField] CanvasGroup group0;
  [SerializeField] CanvasGroup group1;

  void Start()
  {
    fadeTime = 3f;
    group0.alpha = 0;
    group1.alpha = 0;
    dayTime = GameManager.dayCycleTime;
    StartCoroutine(CycleDays());
  }

  IEnumerator CycleDays()
  {
    //day
    yield return new WaitForSeconds(dayTime);

    //night
    GameManager.switchToNight();
    StartCoroutine(FadeNight(0, 1));
    yield return new WaitForSeconds(dayTime);
    //day
    GameManager.day++;
    GameManager.switchToDay();
    StartCoroutine(FadeNight(1, 0));
    StartCoroutine(CycleDays());
  }
  IEnumerator FadeNight(float start, float end)
  {
    float elaspedTime = 0f;
    while (elaspedTime <= fadeTime)
    {
      if (Time.timeScale != 0)
      {
        group0.alpha = Mathf.Lerp(start, end, elaspedTime / fadeTime);
        group1.alpha = Mathf.Lerp(start, end, elaspedTime / fadeTime);
        elaspedTime += Time.deltaTime;
      }
      yield return null;
    }
  }
}
