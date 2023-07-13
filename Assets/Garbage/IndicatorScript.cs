using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Indicator attached to predator shark
public class IndicatorScript : MonoBehaviour
{

  private float angle;
  public float radius;
  public PlayerStats stats;
  private Vector3 playerPosition;
  private Vector3 indicatorPosition = Vector3.zero;

  private void Start()
  {
    transform.GetComponent<SpriteRenderer>().enabled = false;
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
