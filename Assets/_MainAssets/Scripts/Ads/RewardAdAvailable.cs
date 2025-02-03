using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gley.MobileAds;

public class RewardAdAvailable : MonoBehaviour
{
  //[SerializeField] TextMeshProUGUI adText;
  [SerializeField] GameObject alternative;
  [SerializeField] GameObject adButton;

  private void Start()
  {
    UpgradesManager.updateCosts += SetButtons;
  }
  private void OnDestroy()
  {
    UpgradesManager.updateCosts -= SetButtons;
  }
  private void OnEnable()
  {
    SetButtons();
  }
  private void SetButtons()
  {
    if (API.CanShowAds())
    {
      adButton.SetActive(true);
      alternative.SetActive(false);
    }
    else
    {
      adButton.SetActive(false);
      alternative.SetActive(true);
    }
  }
}
