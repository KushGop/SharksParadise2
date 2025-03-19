using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayEvent : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI text;
  Color color;

  void Start()
  {
    GameManager.eventText += DisplayEventText;
    text.enabled = false;
    color = text.color;
  }
  private void OnDestroy()
  {
    GameManager.eventText -= DisplayEventText;
  }

  private void DisplayEventText(string displayText, float waitTime)
  {
    text.enabled = true;
    text.color = color;
    text.text = displayText;
    StartCoroutine(Display(waitTime));
  }

  IEnumerator Display(float waitTime)
  {
    yield return new WaitForSeconds(waitTime);

    for (float i = waitTime; i >= 0; i -= Time.deltaTime)
    {
      // set color with i as alpha
      color.a = i;
      text.color = color;
      yield return null;
    }
    text.enabled = false;
  }
}
