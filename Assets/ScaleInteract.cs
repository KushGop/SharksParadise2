using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleInteract : MonoBehaviour
{

  [SerializeField] float endX;
  [SerializeField] float endY;
  Vector3 startScale;
  Vector3 endScale;


  void Start()
  {
    startScale = transform.localScale;
    endScale.x = endX;
    endScale.y = endY;
  }

  public void Interact()
  {
    StopAllCoroutines();
    Reset();
    StartCoroutine(ScaleBounce());
  }

  private void Reset()
  {
    transform.localScale = startScale;
  }

  IEnumerator ScaleBounce()
  {
    float waitTime = 0.05f;
    for (float elapsedTime = 0; elapsedTime < waitTime; elapsedTime += Time.deltaTime)
    {
      transform.localScale = Vector3.Lerp(endScale, startScale, elapsedTime / waitTime);
      yield return null;
    }
    Reset();
  }
}
