using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostHandleAnim : MonoBehaviour
{

  public Transform followObject;
  public float xDiff;

  // Update is called once per frame
  private void LateUpdate()
  {
    transform.position = followObject.position + Vector3.left * xDiff;
  }
}
