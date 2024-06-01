using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlToggle : MonoBehaviour
{
  public Control controlLeft;
  public Control controlRight;

  void Start()
  {
    SetControls();
  }

  public void SwitchControl()
  {
    GameManager.control = GameManager.control == 0 ? 1 : 0;
    SetControls();
  }

  private void SetControls()
  {
    if (GameManager.control == 0)
    {
      controlRight.control.SetActive(true);
      controlLeft.control.SetActive(false);
    }
    else if (GameManager.control == 1)
    {
      controlLeft.control.SetActive(true);
      controlRight.control.SetActive(false);
    }
  }
}
