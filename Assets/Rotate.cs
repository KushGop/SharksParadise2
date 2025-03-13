using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

  [SerializeField] float speed = 90;

  private void Start()
  {
    transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + Random.Range(0, 360));
  }

  void Update()
  {
    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + (1 * speed)), Time.deltaTime);
  }
}
