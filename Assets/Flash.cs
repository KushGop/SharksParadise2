using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
  [SerializeField] Image image;
  Color baseColor;
  Color clearColor;

  private void Start()
  {
    image.enabled = false;
    baseColor = image.color;
    clearColor = baseColor;
    clearColor.a = 0;
    GameManager.fishEaten += FlashScreen;
  }
  private void OnDestroy()
  {
    GameManager.fishEaten -= FlashScreen;
  }

  private void FlashScreen()
  {
    StopAllCoroutines();
    StartCoroutine(FlashHelper());
  }
  IEnumerator FlashHelper()
  {
    image.enabled = true;
    for (float elapsedTime = 0; elapsedTime < 0.25f; elapsedTime += Time.deltaTime)
    {
      image.color = Color.Lerp(baseColor, clearColor, elapsedTime / 0.25f);
      yield return null;
    }
    image.enabled = false;
  }
}
