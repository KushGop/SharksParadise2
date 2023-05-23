using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetScript : MonoBehaviour
{
  public PolygonCollider2D net;
  public FishNetFactory factory;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.tag == "Player")
    {
      if (other.transform.GetComponent<PlayerMovement>().GetIsJump())
      {
        Destroy(net); 
        factory.ActivateChildren();
      }
    }
  }
}
