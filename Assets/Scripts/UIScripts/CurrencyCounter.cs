using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{
  public TextMeshProUGUI count;
  public Image icon, seperator;
  private Color newColor = Color.clear;
  public Currency c;

  // Start is called before the first frame update
  void Start()
  {
    if (c == Currency.Coin)
      count.text = GameManager.coins.ToString();
    else if (c == Currency.Gem)
      count.text = GameManager.gems.ToString();
    StartCoroutine(FadeOut());
  }

  public void AddCurrency(Currency currency, int value)
  {
    if (currency == c)
    {
      StopAllCoroutines();
      if (currency == Currency.Coin)
      {
        GameManager.coins += value;
        count.text = GameManager.coins.ToString();
      }
      else if (currency == Currency.Gem)
      {
        GameManager.gems += value;
        count.text = GameManager.gems.ToString();
      }
      count.color = Color.black;
      icon.color = Color.white;
      seperator.color = Color.white;
      StartCoroutine(FadeOut());
    }
  }

  private IEnumerator FadeOut()
  {
    yield return new WaitForSeconds(3f);
    float waitTime = 3f;

    for (float i = waitTime; i >= 0; i -= Time.deltaTime)
    {
      // set color with i as alpha
      count.color = new Color(0, 0, 0, i);
      icon.color = new Color(255, 255, 255, i);
      seperator.color = new Color(255, 255, 255, i);
      yield return null;
    }
  }
}