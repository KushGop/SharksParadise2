using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointPrefab : MonoBehaviour
{

  private TextMeshProUGUI text;
  public Color newColor;
  public float fadeTime = 0.1f;
  public float upDistance = 0.2f;

  // Start is called before the first frame update
  void Start()
  {
    text = transform.GetComponent<TextMeshProUGUI>();
    // Debug.Log(text.text);
    StartCoroutine(FadeOut());
  }
  //note the change from 'void' to 'IEnumerator'
  IEnumerator FadeOut()
  {
    //ugly while, Update would be ideal
    while (text.color.a > 0.01)
    {
      text.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
      transform.position = Vector3.Lerp(transform.position,transform.position+(Vector3.up*upDistance),Time.deltaTime*fadeTime);
      yield return null;
    }
    //code after fading is finished
    Destroy(gameObject);
  }
}
