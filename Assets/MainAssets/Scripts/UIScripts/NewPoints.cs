using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewPoints : MonoBehaviour
{
  public GameObject pointPrefab;
  private GameObject newPoint;
  public Color[] colors;
  [SerializeField] Transform newPointOrigin;
  [SerializeField] Transform player;
  [SerializeField] Vector3 buff;

  private void OnEnable()
  {
    foreach (Transform t in transform)
    {
      Destroy(t.gameObject);
    }
  }

  public void OnNewPoint(int value)
  {
    newPoint = Instantiate(pointPrefab, Camera.main.WorldToScreenPoint(player.position) + buff, Quaternion.identity, newPointOrigin);
    newPoint.transform.GetComponent<PointPrefab>().SetText("+" + value);
  }

  public void OnMultiply(int value, Vector3 pos)
  {
    newPoint = Instantiate(pointPrefab, Camera.main.WorldToScreenPoint(player.position) + buff, Quaternion.identity, newPointOrigin);
    //newPoint.transform.Translate(pos);
    newPoint.transform.GetComponent<PointPrefab>().SetText("*" + value);
    newPoint.transform.GetComponent<PointPrefab>().SetBubbleColor(colors[value % colors.Length]);
  }
}
