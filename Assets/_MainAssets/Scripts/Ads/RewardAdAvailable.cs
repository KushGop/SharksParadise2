using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gley.MobileAds;

public class RewardAdAvailable : MonoBehaviour
{
  [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI adText;

  private void OnEnable()
  {
    if (!API.IsRewardedVideoAvailable())
    {
            adText.text = "ad not available";
      button.interactable = false;
    }
    else
    {
            adText.text = "Watch ad";
            button.interactable = true;
    }
  }
}
