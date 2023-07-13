using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostHandleAnim : MonoBehaviour
{

  public Transform followObject;
  public float xDiff;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  private void LateUpdate()
  {
    transform.position = followObject.position + Vector3.left * xDiff;
  }
}
