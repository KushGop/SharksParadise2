using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{
  public TextMeshProUGUI count;
  //public Image icon, seperator;
  public CanvasGroup group;
  public Currency c;

  [SerializeField] GameObject addOrigin;
  [SerializeField] GameObject pointPrefab;
  [SerializeField] Transform player;
  [SerializeField] Vector3 buff;

  // Start is called before the first frame update
  void Start()
  {
    GameManager.TurnCoinsOn += TurnOn;
    GameManager.TurnCoinsOff += TurnOff;
    if (c == Currency.Coin)
      count.text = GameManager.coins.ToString();
    else if (c == Currency.Gem)
      count.text = GameManager.gems.ToString();
    StartCoroutine(FadeOut());
  }
  private void OnDestroy()
  {
    GameManager.TurnCoinsOn -= TurnOn;
    GameManager.TurnCoinsOff -= TurnOff;
  }

  public void AddCurrency(Currency currency, int value)
  {
    GameObject newPoint = Instantiate(pointPrefab, Camera.main.WorldToScreenPoint(player.position) + buff, Quaternion.identity, addOrigin.transform);
    newPoint.transform.GetComponent<PointPrefab>().SetText("+" + value.ToString());
    newPoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
      group.alpha = 1;
      StartCoroutine(FadeOut());
    }
  }

  private IEnumerator FadeOut()
  {
    yield return new WaitForSeconds(3f);
    float waitTime = 3f;
    float i;
    for (i = waitTime; i >= 0; i -= Time.deltaTime)
    {
      // set color with i as alpha
      group.alpha = i / waitTime;
      yield return null;
    }
    group.alpha = 0;
  }

  private void TurnOn()
  {
    StopAllCoroutines();
    group.alpha = 1;
    if (c == Currency.Coin)
      count.text = (GameManager.totalCoins + GameManager.coins).ToString();
    else if (c == Currency.Gem)
      count.text = (GameManager.totalGems + GameManager.gems).ToString();
  }
  private void TurnOff()
  {
    if (c == Currency.Coin)
      count.text = GameManager.coins.ToString();
    else if (c == Currency.Gem)
      count.text = GameManager.gems.ToString();
    StartCoroutine(FadeOut());
  }
}
