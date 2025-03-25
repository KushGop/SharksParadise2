using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayEvent : MonoBehaviour
{
  [SerializeField] Transform listParent;
  [SerializeField] GameObject eventPrefab;

  void Start()
  {
    GameManager.eventText += DisplayEventText;
  }
  private void OnDestroy()
  {
    GameManager.eventText -= DisplayEventText;
  }

  private void DisplayEventText(string displayText, float waitTime)
  {
    GameObject prefab = Instantiate(eventPrefab, listParent);
    prefab.GetComponentInChildren<TextMeshProUGUI>().text = displayText;
    StartCoroutine(Display(waitTime, prefab));
  }

  IEnumerator Display(float waitTime, GameObject prefab)
  {
    CanvasGroup group = prefab.GetComponent<CanvasGroup>();
    yield return new WaitForSeconds(waitTime);

    for (float i = waitTime; i >= 0; i -= Time.deltaTime)
    {
      group.alpha = i;
      yield return null;
    }
    Destroy(prefab);
  }
}
