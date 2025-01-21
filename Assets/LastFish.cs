using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LastFish : MonoBehaviour
{
  [SerializeField] Aquarium_SO fishes;
  [SerializeField] TextMeshProUGUI multiText;
  [SerializeField] Image bubble;
  [SerializeField] Colors_SO colors;

  private void Awake()
  {
    multiText.text = "*" + 0;
    bubble.gameObject.SetActive(false);
    GameManager.fishEaten += FishEaten;
  }
  private void OnDestroy()
  {
    GameManager.fishEaten -= FishEaten;
  }

  private void FishEaten()
  {
    bubble.gameObject.SetActive(true);
    Instantiate(fishes.images[GameManager.lastFish], transform);
    if (transform.childCount > 3)
    {
      Destroy(transform.GetChild(0).gameObject);
    }
    multiText.text = "*" + GameManager.multiplyer;
    bubble.color = GameManager.multiplyer == 1 ? Color.white : colors.colors[GameManager.multiplyer % colors.colors.Length];
    UpdateChildenAlpha();
  }

  private void UpdateChildenAlpha()
  {
    for (int i = 0, j = transform.childCount; i < transform.childCount; i++, j--)
    {
      transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 1f - ((j - 1f) / 3f);
    }
  }
}
