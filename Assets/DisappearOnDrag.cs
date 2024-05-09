using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DisappearOnDrag : MonoBehaviour, IPointerDownHandler
{
  public void OnPointerDown(PointerEventData eventData)
  {
    GameManager.disappear();
  }
}
