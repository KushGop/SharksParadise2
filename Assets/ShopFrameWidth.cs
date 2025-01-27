using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFrameWidth : MonoBehaviour
{
  [SerializeField] RectTransform rect;
  private float xVal, distance;

  private void Start()
  {
    xVal = Screen.width / 2;
    distance = 400;
  }

  private void Update()
  {
    float scale = Mathf.Cos(Mathf.Abs((transform.position.x - xVal) / distance));

    rect.localScale = Vector3.one * Mathf.Max(0.75f, Mathf.Min(1, scale));
  }
}
