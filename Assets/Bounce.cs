using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
  [SerializeField] float speed;
  [SerializeField] float bounce;
  Vector3 pos;

  private void Start()
  {
    pos = transform.localPosition;
  }


  void Update()
  {
    pos.y += Mathf.Cos(Time.time * speed) * bounce;

    transform.localPosition = pos;
  }
}
