using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialJoystick : MonoBehaviour, IPointerDownHandler
{

  public void OnPointerDown(PointerEventData eventData)
  {
    TutorialManager.moved();
    Destroy(this);
  }
}
