using System.Collections;
using System.Collections.Generic;
using Gley.MobileAds;
using UnityEngine;

public class AdmobAds : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    API.Initialize();
    //API.ShowBanner(BannerPosition.Bottom, BannerType.Banner);
  }

  private void OnInitialized()
  {
    //API.ShowBanner(BannerPosition.Bottom, BannerType.Banner);
    //Show ads only after this method is called
    //This callback is not mandatory if you do not want to show banners as soon as your app starts.
  }
}
