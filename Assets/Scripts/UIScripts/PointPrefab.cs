using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointPrefab : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI text;
  [SerializeField] private Image bubble;
  [SerializeField] private CanvasGroup group;
  private float fadeTime = 2.3f;
  private float upDistance = 0.2f;


  // Start is called before the first frame update
  void Start()
  {
    //text = transform.GetComponent<TextMeshProUGUI>();
    // Debug.Log(text.text);
    StartCoroutine(FadeOut());
  }

  public void SetText(string t)
  {
    text.text = t;
  }
  public void SetBubbleColor(Color c)
  {
    bubble.color = c;
  }

  IEnumerator FadeOut()
  {
    float elaspedTime = 0f;
    while (elaspedTime <= fadeTime)
    {
      elaspedTime += Time.deltaTime;
      group.alpha = Mathf.Lerp(1, 0, elaspedTime / fadeTime);
      transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3.up * upDistance), elaspedTime / fadeTime);
      yield return null;
    }
    Destroy(gameObject);
  }
}
