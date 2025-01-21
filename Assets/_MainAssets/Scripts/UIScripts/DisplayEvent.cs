using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayEvent : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI text;

  void Start()
  {
    GameManager.eventText += DisplayEventText;
    text.enabled = false;
  }
  private void OnDestroy()
  {
    GameManager.eventText -= DisplayEventText;
  }

  private void DisplayEventText(string displayText, float waitTime)
  {
    text.enabled = true;
    text.color = new Color(255, 255, 255, 255);
    text.text = displayText;
    StartCoroutine(Display(waitTime));
  }

  IEnumerator Display(float waitTime)
  {
    yield return new WaitForSeconds(waitTime);

    for (float i = waitTime; i >= 0; i -= Time.deltaTime)
    {
      // set color with i as alpha
      text.color = new Color(255, 255, 255, i);
      yield return null;
    }
    text.enabled = false;
  }
}
