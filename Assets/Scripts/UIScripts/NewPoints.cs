using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewPoints : MonoBehaviour
{
  public GameObject pointPrefab;
  private GameObject newPoint;

  public void OnNewPoint(int value)
  {
    newPoint = Instantiate(pointPrefab, transform);
    newPoint.transform.GetComponent<TextMeshProUGUI>().text = "+ " + value;
  }

  public void OnMultiply(int value, Vector3 pos){
    newPoint = Instantiate(pointPrefab, transform);
    newPoint.transform.position = pos;
    newPoint.transform.GetComponent<TextMeshProUGUI>().text = "* " + value;
  }
}
