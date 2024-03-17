using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimLines : MonoBehaviour
{
  public Vector3 offset;
  public GameObject swimLine;
  public Transform parent;

  private void Start()
  {
    StartCoroutine(SpawnLines());
  }

  IEnumerator SpawnLines()
  {

    yield return new WaitForSeconds(0.1f);
    Instantiate(swimLine, transform.position, transform.rotation, parent);
    StartCoroutine(SpawnLines());
  }
}
