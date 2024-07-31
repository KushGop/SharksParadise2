using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.MobileAds;

public class BannerAd : MonoBehaviour
{
  void Start()
  {
    API.Initialize(OnInitialized);
    API.ShowBanner(BannerPosition.Bottom, BannerType.Banner);
  }

  private void OnInitialized()
  {
    API.ShowBanner(BannerPosition.Bottom, BannerType.Banner);
    //Show ads only after this method is called
    //This callback is not mandatory if you do not want to show banners as soon as your app starts.
  }
}
