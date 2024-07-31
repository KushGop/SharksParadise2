using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBackground : MonoBehaviour
{
  [SerializeField] bool pause;

  private void Awake()
  {
    if (pause)
      GameManager.pauseBackground();
    else
      GameManager.unpauseBackground();
  }
}
