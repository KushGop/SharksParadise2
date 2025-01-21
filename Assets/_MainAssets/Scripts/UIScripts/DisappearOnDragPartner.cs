using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisappearOnDragPartner : MonoBehaviour
{

  [SerializeField] Image arrow;

  private void Start()
  {
    gameObject.SetActive(true);
    StartCoroutine(Flash(true));
    GameManager.disappear += Disappear;
  }
  private void OnDestroy()
  {
    GameManager.disappear -= Disappear;
  }
  private void Disappear()
  {
    gameObject.SetActive(false);
  }

  IEnumerator Flash(bool fadeAway)
  {
    if (fadeAway)
    {
      // loop over 1 second backwards
      for (float i = 1; i >= 0; i -= Time.deltaTime)
      {
        // set color with i as alpha
        arrow.color = new Color(255, 255, 85, i);
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
        arrow.color = new Color(255, 255, 85, i);
        yield return null;
      }
    }
    StartCoroutine(Flash(!fadeAway));
  }
}
