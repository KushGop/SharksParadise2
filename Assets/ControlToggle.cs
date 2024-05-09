using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlToggle : MonoBehaviour
{
  [SerializeField] GameObject left;
  [SerializeField] GameObject right;

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
      right.SetActive(true);
      left.SetActive(false);
    }
    else if (GameManager.control == 1)
    {
      right.SetActive(false);
      left.SetActive(true);
    }
  }
}
