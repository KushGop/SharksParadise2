using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
  public TextMeshProUGUI coinCount;
  public Image coinIcon, coinSeperator;
  private Color newColor = Color.clear;

  // Start is called before the first frame update
  void Start()
  {
    coinCount.text = GameManager.coins.ToString();
    StartCoroutine(FadeOut());
  }

  public void AddCoin(int value)
  {
    StopAllCoroutines();
    GameManager.coins += value;
    coinCount.text = GameManager.coins.ToString();
    coinCount.color = Color.black;
    coinIcon.color = Color.white;
    coinSeperator.color = Color.white;
    StartCoroutine(FadeOut());
  }

  private IEnumerator FadeOut()
  {
    yield return new WaitForSeconds(3f);
    float waitTime = 3f;

    for (float i = waitTime; i >= 0; i -= Time.deltaTime)
    {
      // set color with i as alpha
      coinCount.color = new Color(0, 0, 0, i);
      coinIcon.color = new Color(255, 255, 255, i);
      coinSeperator.color = new Color(255, 255, 255, i);
      yield return null;
    }
  }
}
