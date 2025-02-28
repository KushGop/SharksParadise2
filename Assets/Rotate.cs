using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
  private void Start()
  {
    transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + Random.Range(0, 360));
  }

  void Update()
  {
    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + 1), 90 * Time.deltaTime);
  }
}
