using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{
  [SerializeField] RectMask2D mask;
  [SerializeField] Slider slider;
  [SerializeField] Image powerIcon;
  [SerializeField] float max = 100;
  enum Direction { VERTICAL, HORIZONTAL };
  [SerializeField] Direction direction = Direction.HORIZONTAL;
  Vector4 padding;
  [SerializeField] bool isRadial;

  private void Start()
  {
    padding = new();
  }
  public void Update()
  {
    if (isRadial)
    {
      powerIcon.fillAmount = slider.value;
    }
    else
    {
      if (direction == Direction.HORIZONTAL)
        padding.z = max - ((max / slider.maxValue) * slider.value);
      if (direction == Direction.VERTICAL)
        padding.w = max - ((max / slider.maxValue) * slider.value);
      mask.padding = padding;
    }
  }
}
