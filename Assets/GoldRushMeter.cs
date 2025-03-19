using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldRushMeter : MonoBehaviour
{
  [SerializeField] Slider slider;
  [SerializeField] Transform coinParent;
  [SerializeField] GameObject coinPrefab;

  void Start()
  {
    GameManager.coinPrefab = coinPrefab;
    GameManager.coinParent = coinParent;
    ResetSlider();
    GameManager.IncrementGoldMeter += IncreaseSlider;
    GameManager.isGoldRush = false;
  }
  private void OnDestroy()
  {
    GameManager.IncrementGoldMeter -= IncreaseSlider;
  }

  void IncreaseSlider()
  {
    if (slider.value < slider.maxValue)
    {
      slider.value++;
      if (slider.value == slider.maxValue)
      {
        GameManager.isGoldRush = true;
        GameManager.eventText("Gold Rush", 1f);
        StartCoroutine(GoldRushTimer());
      }
    }
  }

  IEnumerator GoldRushTimer()
  {
    yield return new WaitForSeconds(10);
    GameManager.isGoldRush = false;
    GameManager.StopGoldRush();
    ResetSlider();
  }

  public void ResetSlider()
  {
    slider.value = 0;
  }
}
