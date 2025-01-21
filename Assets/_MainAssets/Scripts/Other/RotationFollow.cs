using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollow : MonoBehaviour
{
  [SerializeField] Transform transformToFollow;
  [SerializeField] bool isOpposite;

  void Update()
  {
    if (isOpposite)
      transform.rotation = Quaternion.Euler(0, 0, -transformToFollow.rotation.z);
    else
      transform.rotation = Quaternion.Lerp(transform.rotation, transformToFollow.rotation, 15 * Time.deltaTime);
  }
}
