using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownMenu : MonoBehaviour
{
  [SerializeField] Transform children;
  public float distance;
  Vector3 defaultPos, hiddenPos;
  bool isIn;

  private void Start()
  {
    isIn = false;
    defaultPos = children.localPosition;
    hiddenPos = defaultPos;
    hiddenPos.x -= 500;
    children.localPosition = hiddenPos;
  }

  public void ToggleMenu()
  {
    StopAllCoroutines();
    if (!isIn)
      StartCoroutine(SlideIn());
    else
      StartCoroutine(SlideOut());
    isIn = !isIn;
  }

  IEnumerator SlideIn()
  {
    float elapsedTime = 0;
    float waitTime = .5f;
    while (elapsedTime < waitTime)
    {
      children.localPosition = Vector3.Lerp(children.localPosition, defaultPos, elapsedTime / waitTime);
      elapsedTime += Time.deltaTime;

      // Yield here
      yield return null;
    }
  }
  IEnumerator SlideOut()
  {
    float elapsedTime = 0;
    float waitTime = 0.5f;
    while (elapsedTime < waitTime)
    {
      children.localPosition = Vector3.Lerp(children.localPosition, hiddenPos, elapsedTime / waitTime);
      elapsedTime += Time.deltaTime;

      // Yield here
      yield return null;
    }
  }
}
