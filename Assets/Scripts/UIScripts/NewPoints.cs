using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewPoints : MonoBehaviour
{
  public GameObject pointPrefab;
  private GameObject newPoint;
  public Color[] colors;

  public void OnNewPoint(int value)
  {
    newPoint = Instantiate(pointPrefab, transform);
    newPoint.transform.GetComponent<PointPrefab>().SetText("+" + value);
  }

  public void OnMultiply(int value, Vector3 pos)
  {
    newPoint = Instantiate(pointPrefab, transform);
    //newPoint.transform.Translate(pos);
    newPoint.transform.GetComponent<PointPrefab>().SetText("*" + value);
    newPoint.transform.GetComponent<PointPrefab>().SetBubbleColor(colors[value % colors.Length]);
  }
}
