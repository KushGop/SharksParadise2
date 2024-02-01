using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompletedMission : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI text;
  [SerializeField] private Image image;
  [SerializeField] private Image strike;
  [SerializeField] private RectTransform mask;
  [SerializeField] private AudioSource scratch;
  private Color newColor = new Color32(255, 255, 255, 0);
  private float elapsedTime = 0;
  public float waitTime;
  public float distance;
  private Vector3 finalPos;

  public void SetText(string mission)
  {
    text.text = mission;

  }

  // Start is called before the first frame update
  void Start()
  {
    finalPos = transform.localPosition + (Vector3.down * distance);
    StartCoroutine(DropDown());
  }

  IEnumerator DropDown()
  {
    while (elapsedTime < waitTime)
    {
      transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos, elapsedTime / waitTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    elapsedTime = 0;
    scratch.Play();
    while (elapsedTime < waitTime)
    {
      mask.sizeDelta = Vector2.Lerp(mask.sizeDelta, new Vector2(860, 110), elapsedTime / waitTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    while (image.color.a > 0.01)
    {
      image.color = Color.Lerp(image.color, newColor, Time.deltaTime * waitTime);
      strike.color = Color.Lerp(strike.color, newColor, Time.deltaTime * waitTime);
      text.color = Color.Lerp(text.color, newColor, Time.deltaTime * waitTime);
      yield return null;
    }
    Destroy(gameObject);
  }

}
