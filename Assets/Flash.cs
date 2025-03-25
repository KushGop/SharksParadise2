using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
  [SerializeField] Image image;
  [SerializeField] Color nightColor;
  Color baseColor;
  Color clearColor;
  Color clearNightColor;

  private void Start()
  {
    image.enabled = false;
    baseColor = image.color;
    clearColor = baseColor;
    clearNightColor = nightColor;
    clearColor.a = 0;
    clearNightColor.a = 0;
    GameManager.fishEaten += FlashScreen;
  }
  private void OnDestroy()
  {
    GameManager.fishEaten -= FlashScreen;
  }

  private void FlashScreen()
  {
    StopAllCoroutines();
    StartCoroutine(FlashHelper(
      GameManager.isNight ? nightColor : baseColor,
      GameManager.isNight ? clearNightColor : clearColor));
  }
  IEnumerator FlashHelper(Color start, Color end)
  {
    image.enabled = true;
    for (float elapsedTime = 0; elapsedTime < 0.25f; elapsedTime += Time.deltaTime)
    {
      image.color = Color.Lerp(start, end, elapsedTime / 0.25f);
      yield return null;
    }
    image.enabled = false;
  }
}
