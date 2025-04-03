using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Indicator attached to predator shark
public class IndicatorScript : MonoBehaviour
{
  Transform target, player;
  float scale;

  private void LateUpdate()
  {
    scale = Mathf.Abs(Vector3.Distance(target.position, player.position)) / 12;
    transform.localScale = Vector3.one * scale;
    transform.right = target.position - player.position;
  }

  public void PointToward(Transform obj, Transform player)
  {
    target = obj;
    this.player = player;
  }
}
