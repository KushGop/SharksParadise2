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
  [SerializeField] Shaker shake;

  // Start is called before the first frame update
  void Start()
  {
    GameManager.ToogleCoins += Toggle;
    if (c == Currency.Coin)
      count.text = GameManager.coins.ToString();
    else if (c == Currency.Gem)
      count.text = GameManager.gems.ToString();
  }
  private void OnDestroy()
  {
    GameManager.ToogleCoins -= Toggle;
  }

  public void AddCurrency(Currency currency, int value)
  {
    shake.Shake();
    GameObject newPoint = Instantiate(pointPrefab, Camera.main.WorldToScreenPoint(player.position) + buff, Quaternion.identity, addOrigin.transform);
    newPoint.transform.GetComponent<PointPrefab>().SetText("+" + value.ToString());
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
    }
  }

  private void Toggle(bool b)
  {
    if (c == Currency.Coin)
      count.text = ((b ? GameManager.totalCoins : 0) + GameManager.coins).ToString();
    else if (c == Currency.Gem)
      count.text = ((b ? GameManager.totalGems : 0) + GameManager.gems).ToString();
  }
}
