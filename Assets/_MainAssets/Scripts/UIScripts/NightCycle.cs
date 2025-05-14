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
  [SerializeField] bool tut;
  void Start()
  {
    fadeTime = 3f;
    group0.alpha = 0;
    group1.alpha = 0;
    GameManager.day = 1;
    GameManager.isNight = false;
    GameManager.switchToNight += FadeIn;
    GameManager.switchToDay += FadeOut;
    dayTime = GameManager.dayCycleTime;
    if (!tut)
      StartCoroutine(CycleDays());
  }
  private void OnDestroy()
  {
    GameManager.switchToNight -= FadeIn;
    GameManager.switchToDay -= FadeOut;
  }

  public void FadeIn()
  {
    StartCoroutine(FadeNight(0, 0.9f));
  }
  public void FadeOut()
  {
    StartCoroutine(FadeNight(0.9f, 0));
  }

  IEnumerator CycleDays()
  {
    //day
    yield return new WaitForSeconds(dayTime);

    //night
    GameManager.isNight = true;
    GameManager.switchToNight();
    yield return new WaitForSeconds(dayTime);
    //day
    GameManager.day++;
    MissionData.IncrementMission(MissionName.nightsSurvived);
    GameManager.isNight = false;
    GameManager.switchToDay();
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
