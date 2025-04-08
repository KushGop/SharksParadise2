using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
  Coroutine shake;
  Vector3 rot;


  public void Shake()
  {
    if (shake != null)
    {
      StopCoroutine(shake);
    }
    shake = StartCoroutine(ShakeHelper(0.05f, 5));
  }
  IEnumerator ShakeHelper(float interval, float pong)
  {
    float elapsedTime;
    for (int i = 0; i < 1; i++)
    {
      for (elapsedTime = 0; elapsedTime < interval; elapsedTime += Time.deltaTime)
      {
        rot.z = Mathf.Lerp(-pong, pong, elapsedTime / interval);
        transform.localEulerAngles = rot;
        yield return null;
      }
      for (elapsedTime = 0; elapsedTime < interval; elapsedTime += Time.deltaTime)
      {
        rot.z = Mathf.Lerp(pong, -pong, elapsedTime / interval);
        transform.localEulerAngles = rot;
        yield return null;
      }
    }
    rot.z = 0;
    transform.localEulerAngles = rot;
  }
}
