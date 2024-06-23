using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreDisplay : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI text;
  [SerializeField] private bool displayText;
  private bool done;

  private void Start()
  {
    done = false;
    if (displayText)
      text.text = "Best | " + GameManager.highscore;
  }

  private void OnEnable()
  {
    done = false;
  }

  void Update()
  {
    if (!done)
    {
      if (GameManager.score > GameManager.highscore)
      {
        StartCoroutine(Flash(true));
        text.text = "Highscore!";
        done = true;
      }
    }
  }
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
