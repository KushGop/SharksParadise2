using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
  [SerializeField] private CanvasGroup boostJoystickCanvas;
  [SerializeField] private CanvasGroup jumpJoystickCanvas;
  [SerializeField] private CanvasGroup jumpText;
  [SerializeField] private Transform joystickOuter;

  public RectTransform joystick;
  public RectTransform panel;
  private Vector2 offset;

  protected int jumpDistance = 90000;
  protected int jumpTextDistance = 70000;

  private int dragMovementDistance = 90;
  private int dragDistanceMove = 155;
  private int dragDistanceJump = 230;
  private float dragOffsetDistance = 90;

  private float angle;
  private Vector2 unitValue;
  public float offsetDivider;

  public event Action<Vector2> OnMove;
  public event Action OnBoostPressed;
  public event Action OnBoostReleased;
  public event Action JumpPlayer;
  private bool hasJumped = false;
  private float actionDistance;

  private void Start()
  {
    unitValue = new();
  }

  private void Update()
  {
    OnMove?.Invoke(unitValue);
    joystickOuter.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(-unitValue.x, unitValue.y) * Mathf.Rad2Deg);
  }

  public virtual void BoostReleased()
  {
    OnBoostReleased?.Invoke();
  }
  public virtual void BoostPressed()
  {
    OnBoostPressed?.Invoke();
  }
  public virtual void JumpPressed()
  {
    JumpPlayer?.Invoke();
  }

  #region JoytickImage
  private void HighlightCanvas(bool highlight, CanvasGroup canvas)
  {
    canvas.alpha = highlight ? 1 : 0.5f;
  }
  #endregion

  private void JoystickHandler(PointerEventData eventData)
  {
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
          panel,
          eventData.position,
          Camera.main,
          out offset);

    angle = Mathf.Atan2(offset.y, offset.x);
    unitValue.x = Mathf.Cos(angle);
    unitValue.y = Mathf.Sin(angle);

    actionDistance = Vector2.SqrMagnitude(offset);
    jumpText.alpha = actionDistance / jumpTextDistance;
    if (actionDistance < jumpDistance)
    {
      hasJumped = false;
      if (hasJumped)
      {
        hasJumped = false;
        HighlightCanvas(false, jumpJoystickCanvas);
      }
      dragMovementDistance = dragDistanceMove;
      dragOffsetDistance = dragDistanceMove;
    }
    else
    {
      if (!hasJumped)
      {
        HighlightCanvas(true, jumpJoystickCanvas);
        //HighlightCanvas(false, boostJoystickCanvas);
        JumpPressed();
        hasJumped = true;
        //isBoosting = false;
      }
      dragMovementDistance = dragDistanceJump;
      dragOffsetDistance = dragDistanceJump;
    }

    offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;

    joystick.anchoredPosition = offset * dragMovementDistance;

  }

  public void OnDrag(PointerEventData eventData) => JoystickHandler(eventData);
  public void OnPointerDown(PointerEventData eventData) => JoystickHandler(eventData);

  public void OnPointerUp(PointerEventData eventData)
  {
    joystick.anchoredPosition = Vector2.zero;
  }
}
