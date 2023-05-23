using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostHandleAnim : MonoBehaviour
{

  public Transform followObject;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.position = followObject.position;
  }
}
