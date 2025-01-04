using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutAdjust : MonoBehaviour
{
  [SerializeField] RectTransform rect;
  [SerializeField] float distanceDefault;
  [SerializeField] float distance2;
  [SerializeField] bool isTop;
  [SerializeField] bool isBoth;
  void Awake()
  {
    if (isBoth)
    {
      if (Screen.safeArea.yMin > 0)
      {
        rect.offsetMin = new Vector2(rect.offsetMin.x, distanceDefault);
      }
      if (Screen.safeArea.yMax < Screen.height)
      {
        rect.offsetMax = new Vector2(rect.offsetMax.x, distance2);
      }
    }
    else if ((Screen.safeArea.yMin > 0 && !isTop) || (Screen.safeArea.yMax < Screen.height && isTop))
    {
      rect.sizeDelta = new Vector2(rect.sizeDelta.x, distanceDefault);
    }
  }
}
