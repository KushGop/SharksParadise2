using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialJoystick : MonoBehaviour, IDragHandler
{

  public void OnDrag(PointerEventData eventData)
  {
    if (TutorialManager.shouldMove)
    {
      TutorialManager.moved();
      Destroy(this);
    }
  }
}
