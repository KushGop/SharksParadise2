using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
  private RectTransform joystick;
  private Vector2 offset;

  [SerializeField] private int dragMovementDistance = 30;
  [SerializeField] private int dragOffsetDistance = 200; // limits the joystick drag distance

  public event Action<Vector2> OnMove;

  private void Update()
  {
    OnMove?.Invoke(offset);
  }

  public void OnDrag(PointerEventData eventData)
  {
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
      joystick,
      eventData.position,
      null,
      out offset);

    //normalize magnitude
    //clamp limits offset value to dragOffsetDistance, sets range ( -100 : 100 )
    //divide it by dragOffsetDistance to normalize, sets range ( -1 : 1 )
    offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
    
    joystick.anchoredPosition = offset * dragMovementDistance;
    // fixedDistance(offset);
  }

  //NOT IMPLEMENTED 
  //Adjust parent position rect to where player presses
  public void OnPointerDown(PointerEventData eventData)
  {
    //OnMove?.Invoke(offset);
  }


  public void OnPointerUp(PointerEventData eventData)
  {
    joystick.anchoredPosition = Vector2.zero;
  }

  private void Awake()
  {
    joystick = (RectTransform)transform;
  }
}
