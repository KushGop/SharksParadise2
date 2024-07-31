using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{

  public TextMeshProUGUI text;

  private void Start()
  {
    StartCoroutine(Flash(true));
  }

  // private void Update()
  // {
  //   color.a = Mathf.Lerp(alpha % 2, (alpha + 1) % 2, Time.deltaTime);
  //   text.color = color;
  // }

  IEnumerator Flash(bool fadeAway)
  {
    if (fadeAway)
    {
      // loop over 1 second backwards
      for (float i = 1; i >= 0; i -= Time.deltaTime)
      {
        // set color with i as alpha
        text.color = new Color(255, 255, 255, i);
        yield return null;
      }
    }
    // fade from transparent to opaque
    else
    {
      // loop over 1 second
      for (float i = 0; i <= 1; i += Time.deltaTime)
      {
        // set color with i as alpha
        text.color = new Color(255, 255, 255, i);
        yield return null;
      }
    }
    StartCoroutine(Flash(!fadeAway));
  }
}
