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
    //transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.z + 90);
  }

  public void PointToward(Transform obj, Transform player)
  {
    target = obj;
    this.player = player;
  }

  // private void OnTriggerStay2D(Collider2D other)
  // {
  //   if (other.transform.tag == "IndicatorZone")
  //   {
  //     playerPosition = stats.playerPosition;
  //     angle = Vector3.Angle(playerPosition, transform.position);
  //     indicatorPosition.x = radius * Mathf.Cos(angle) + playerPosition.x;
  //     indicatorPosition.y = radius * Mathf.Sin(angle) + playerPosition.y;

  //     transform.position = indicatorPosition;
  //     transform.rotation = Quaternion.Euler(0,0,angle);
  //   }
  // }

  // private void OnTriggerEnter2D(Collider2D other)
  // {
  //   Debug.Log("hello");
  //   if (other.transform.tag == "IndicatorZone")
  //   {
  //     transform.GetComponent<SpriteRenderer>().enabled = true;
  //   }
  // }
  // private void OnTriggerExit2D(Collider2D other)
  // {
  //   if (other.transform.tag == "IndicatorZone")
  //   {
  //     transform.GetComponent<SpriteRenderer>().enabled = false;
  //   }
  // }
}
