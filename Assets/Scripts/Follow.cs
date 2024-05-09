using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
  [SerializeField] Transform transformToFollow;

  void Update()
  {
    transform.position = transformToFollow.position;
  }
}
