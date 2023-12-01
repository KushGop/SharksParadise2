using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
  public RectTransform joystick;
  private Vector2 offset;

  [SerializeField] private int dragMovementDistance = 88;
  [SerializeField] private float dragOffsetDistance = 120; // limits the joystick drag distance
  private float angle;
  private Vector2 unitValue;
  public event Action<Vector2> OnMove;
  public float offsetDivider;
  private Vector2 pivotPoint;
  private Vector3 origin,test;

  private void Update()
  {
    OnMove?.Invoke(unitValue);
  }

  public void OnDrag(PointerEventData eventData)
  {
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
      joystick,
      eventData.position,
      Camera.main,
      out offset);

    //normalize magnitude
    //clamp limits offset value to dragOffsetDistance, sets range ( -100 : 100 )
    //divide it by dragOffsetDistance to normalize, sets range ( -1 : 1 )
    // offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
    angle = Mathf.Atan2(offset.y, offset.x);
    unitValue.x = Mathf.Cos(angle);
    unitValue.y = Mathf.Sin(angle);
    offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
    joystick.anchoredPosition = offset * dragMovementDistance;
    // fixedDistance(offset);
  }

  //Adjust parent position rect to where player presses
  public void OnPointerDown(PointerEventData eventData)
  {
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
      joystick,
      eventData.pressPosition,
      Camera.main,
      out pivotPoint);

    // offset = pivotPoint / offsetDivider;
    test.y += pivotPoint.y / offsetDivider;
    test.x += pivotPoint.x / offsetDivider;
    transform.GetComponent<RectTransform>().anchoredPosition = test;
    // joystick.anchoredPosition = pivotPoint / offsetDivider;
  }


  public void OnPointerUp(PointerEventData eventData)
  {
    joystick.anchoredPosition = Vector2.zero;
    test = origin;
    transform.GetComponent<RectTransform>().anchoredPosition = origin;
  }

  private void Awake()
  {
    origin = transform.GetComponent<RectTransform>().anchoredPosition;
    test = origin;
  }
}
