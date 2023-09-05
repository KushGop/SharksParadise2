using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowMovement : MonoBehaviour
{
  private Vector3 maxScale, scale, rotation;
  float timer;

  private void Start()
  {
    maxScale = transform.localScale;
    rotation = Vector3.zero;
    timer = 0;
  }

  void FixedUpdate()
  {
    timer += Time.deltaTime;
    rotation.z = Mathf.Lerp(0, 360f, Time.deltaTime * 0.1f);
    transform.Rotate(rotation);
    scale.x = maxScale.x - (oscillate(timer, 2) * 0.3f);
    scale.y = maxScale.y - (oscillate(timer, 2) * 0.3f);
    transform.localScale = scale;
  }

  float oscillate(float time, float speed)
  {
    speed = 6f;
    return (Mathf.Cos(time * speed / Mathf.PI) / 2) + 0.5f;
  }

}
