using System.Collections;
using System.Collections.Generic;
using Gley.EasyIAP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSystem : MonoBehaviour
{
  //public TextMeshProUGUI text;

  void Start()
  {
    API.Initialize(InitializationComplete);
  }


  private void InitializationComplete(IAPOperationStatus status, string message, List<StoreProduct> shopProducts)
  {
    if (status == IAPOperationStatus.Success)
    {
      //text.text = "Shop init";
      print("shop initialized");
      Gley.MobileAds.API.RemoveAds(API.IsActive(ShopProductNames.RemoveAds));
    }
    else
    {
      //text.text = "Shop error";
      Debug.Log("Error occurred: " + message);
    }
    SceneManager.LoadScene("MainMenu");
  }

}
