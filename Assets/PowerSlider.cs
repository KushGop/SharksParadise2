using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{
  [SerializeField] RectMask2D mask;
  [SerializeField] Slider slider;
  [SerializeField] float max = 100;
  enum Direction { VERTICAL, HORIZONTAL };
  [SerializeField] Direction direction = Direction.HORIZONTAL;
  Vector4 padding;

  private void Start()
  {
    padding = new();
  }
  public void Update()
  {
    if (direction == Direction.HORIZONTAL)
      padding.z = max - ((max / slider.maxValue) * slider.value);
    if (direction == Direction.VERTICAL)
      padding.w = max - ((max / slider.maxValue) * slider.value);
    mask.padding = padding;
  }
}
