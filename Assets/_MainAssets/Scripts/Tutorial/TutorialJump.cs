using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialJump : MonoBehaviour, IPointerDownHandler
{

  public void OnPointerDown(PointerEventData eventData)
  {
    TutorialManager.jumpPressed();
    Destroy(this);
  }
}
