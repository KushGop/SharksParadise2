using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValue : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI text;
  [SerializeField] Slider slider;

  private void Start()
  {
    slider.value = 0;
    text.text = "-";
  }

  public void UpdateValue()
  {
    if (slider.value != 0)
      text.text = slider.value.ToString();
    else
      text.text = "-";
  }
}
