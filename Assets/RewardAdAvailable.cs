using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gley.MobileAds;

public class RewardAdAvailable : MonoBehaviour
{
  [SerializeField] Button button;

  private void OnEnable()
  {
    if (!API.IsRewardedVideoAvailable())
    {
      button.interactable = false;
    }
    else
    {
      button.interactable = true;
    }
  }
}
