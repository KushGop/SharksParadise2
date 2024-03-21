using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarfishPower : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI text;

  void Start()
  {
    GameManager.powerEvent += DisplayPower;
    text.enabled = false;
  }

  private void DisplayPower(int i)
  {
    text.enabled = true;
    text.color = new Color(0, 0, 0, 255);
    text.text = GameManager.powers[i];
    StartCoroutine(Display());
  }

  IEnumerator Display()
  {
    yield return new WaitForSeconds(3f);
    float waitTime = 3f;

    for (float i = waitTime; i >= 0; i -= Time.deltaTime)
    {
      // set color with i as alpha
      text.color = new Color(0, 0, 0, i);
      yield return null;
    }
    text.enabled = false;
  }
}
