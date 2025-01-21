using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{
  private bool isEmitting = true;
  public TrailRenderer t1, t2;

  public void changeParent()
  {
    isEmitting = !isEmitting;
    t1.emitting = isEmitting;
    t2.emitting = isEmitting;
  }
}
