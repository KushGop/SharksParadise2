using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileInput : MonoBehaviour, IAgentInput
{
  public Vector2 MovementVector { get; private set; }

  [SerializeField] MobileJoystick joystick;

  public event Action<Vector2> OnMovement;
  public event Action OnBoostPressed;
  public event Action OnBoostReleased;
  public event Action JumpPlayer;

  private void Start()
  {
    joystick.OnMove += Move;
  }


  //ONLY FOR TESTING WITH KEYBOARD
  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      BoostPressed();
    }
    if (Input.GetKeyUp(KeyCode.Space))
    {
      BoostReleased();
    }
    if (Input.GetKeyDown(KeyCode.LeftShift)){
      JumpPressed();
    }
  }


  private void Move(Vector2 input)
  {
    MovementVector = input;
    OnMovement?.Invoke(MovementVector);
    }

  public void BoostPressed()
  {
    OnBoostPressed?.Invoke();
  }

  public void BoostReleased()
  {
    OnBoostReleased?.Invoke();
  }

  public void JumpPressed(){
    JumpPlayer?.Invoke();
  }

  // public void GrowPlayerTester(int size)
  // { 
  //   GrowPlayerTest?.Invoke(10);
  // }
}
