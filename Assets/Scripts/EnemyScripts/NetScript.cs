using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetScript : MonoBehaviour
{
  public PolygonCollider2D net;
  public FishNetFactory factory;
  [SerializeField] AudioSource netSnap;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.tag == "Player")
    {
      if (other.transform.GetComponentInParent<PlayerMovement>().GetIsJump())
      {
        netSnap.Play();
        Destroy(net); 
        factory.ActivateChildren();
      }
    }
  }
}
