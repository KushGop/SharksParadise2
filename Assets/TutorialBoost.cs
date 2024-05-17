using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialBoost : MonoBehaviour, IPointerDownHandler
{

  public void OnPointerDown(PointerEventData eventData)
  {
    TutorialManager.boostPressed();
    Destroy(this);
  }
}
