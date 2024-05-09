using System.Collections;
using System.Collections.Generic;
using Gley.EasyIAP;
using UnityEngine;
using TMPro;

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
    }
    else
    {
      //text.text = "Shop error";
      Debug.Log("Error occurred: " + message);
    }
  }

}
