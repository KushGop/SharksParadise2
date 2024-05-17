using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyScript : MonoBehaviour
{

  public PlayerStats playerStats;
  private float yPos;
  public float yScale;
  private Vector3 temp;

  private void Start()
  {
    yPos = transform.localPosition.y;
    //yScale = 1.55f;
  }

  // Update is called once per frame
  void Update()
  {
    temp = transform.localPosition;
    temp.y = yPos - ((100 - playerStats.energy) * yScale);
    transform.localPosition = temp;
  }
}
