using System.Collections;
using System.Collections.Generic;
using Gley.MobileAds;
using UnityEngine;

public class AdmobAds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        API.Initialize(OnInitialized);
        API.ShowBanner(BannerPosition.Top, BannerType.Banner);
    }

    private void OnInitialized()
    {
        Gley.MobileAds.API.ShowBanner(BannerPosition.Top, BannerType.Banner);
        //Show ads only after this method is called
        //This callback is not mandatory if you do not want to show banners as soon as your app starts.
    }
}