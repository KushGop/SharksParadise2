using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LastFish : MonoBehaviour
{
  [SerializeField] Aquarium_SO fishes;
  [SerializeField] TextMeshProUGUI multiText;
  [SerializeField] Image fire;
  [SerializeField] Colors_SO colors;
  [SerializeField] CanvasGroup group;
  int multi;
  private void Awake()
  {
    multi = 0;
    multiText.text = "*" + 0;
    group.alpha = 0;
    //fire.gameObject.SetActive(false);
    GameManager.fishEaten += FishEaten;
  }
  private void OnDestroy()
  {
    GameManager.fishEaten -= FishEaten;
  }

  private void FishEaten()
  {
    //fire.gameObject.SetActive(true);
    group.alpha = 1;
    Instantiate(fishes.images[GameManager.lastFish], transform);
    if (transform.childCount > 3)
    {
      Destroy(transform.GetChild(0).gameObject);
    }
    multi = GameManager.multiplyer * (GameManager.isMultiActive ? 2 : 1);
    if (multi == 1)
    {
      multiText.text = "";
      fire.color = Color.clear;
    }
    else
    {
      multiText.text = "*" + multi;
      fire.color = colors.colors[multi % colors.colors.Length];

    }
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
