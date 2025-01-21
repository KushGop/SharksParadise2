using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
  [SerializeField] Transform leftTop;
  [SerializeField] Transform leftBottom;
  [SerializeField] Transform rightTop;
  [SerializeField] Transform rightBottom;
  private void OnDrawGizmos()
  {
    Gizmos.DrawLine(leftTop.position, leftBottom.position);
    Gizmos.DrawLine(rightTop.position, rightBottom.position);
  }
}
