using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdMeter : MonoBehaviour
{
  [SerializeField] Slider slider;
  bool isBirdFrenzy;

  void Start()
  {
    ResetSlider();
    GameManager.IncrementBirdMeter += IncreaseSlider;
  }
  private void OnDestroy()
  {
    GameManager.IncrementBirdMeter -= IncreaseSlider;
  }

  void IncreaseSlider()
  {
    if (slider.value < slider.maxValue)
    {
      slider.value++;
      if (slider.value == slider.maxValue && !isBirdFrenzy)
      {
        isBirdFrenzy = true;
        GameManager.FullBirdMeter();
        GameManager.eventText("Bird Frenzy", 1f);
        StartCoroutine(BirdFrenzyTimer());
      }
    }
  }

  IEnumerator BirdFrenzyTimer()
  {
    yield return new WaitForSeconds(10);
    ResetSlider();
  }

  public void ResetSlider()
  {
    slider.value = 0;
    isBirdFrenzy = false;
  }
}
