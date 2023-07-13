using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimLines : MonoBehaviour
{
  public Vector3 offset;
  public GameObject swimLine;
  private GameObject newSwimLine;
  public Transform parent;

  private void Start()
  {
    StartCoroutine(spawnLines());
  }

  IEnumerator spawnLines()
  {

    yield return new WaitForSeconds(0.1f);
    newSwimLine = Instantiate(swimLine, transform.position, transform.rotation, parent);
    StartCoroutine(spawnLines());
  }
}
