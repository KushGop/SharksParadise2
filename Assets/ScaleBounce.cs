using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBounce : MonoBehaviour
{
  [SerializeField] float minScale;
  [SerializeField] float maxScale;
  [SerializeField] float speed;

  void Update()
  {
    float scale = Mathf.PingPong(Time.time * speed, maxScale - minScale) + minScale;
    transform.localScale = Vector3.one * scale;
  }
}
