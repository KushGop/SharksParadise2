using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.EasyIAP;
using TMPro;

public class PurchaseButton : MonoBehaviour, IDataPersistence
{
  [SerializeField] ShopProductNames productName;
  [SerializeField] TextMeshProUGUI priceText;
  bool removeAds;

  private void Start()
  {
    priceText.text = API.GetLocalizedPriceString(productName);

  }
  public void LoadData(GameData data)
  {
    GameManager.totalCoins = data.totalCoins;
    GameManager.totalTokens = data.totalTokens;
    GameManager.totalGems = data.totalGems;
  }

  public void AddCoin(int value)
  {
    GameManager.totalCoins += value;
  }
  public void AddGem(int value)
  {
    GameManager.totalGems += value;
  }

  public void SaveData(GameData data)
  {
    data.totalCoins = GameManager.totalCoins;
    data.totalGems = GameManager.totalGems;
  }

  public void BuyStarterPack()
  {
    API.BuyProduct(ShopProductNames.StarterPackage, ProductBought);
  }

  public void BuyConsumable()
  {
    API.BuyProduct(productName, ProductBought);
  }

  //public void BuyRemoveAds()
  //{
  //    Gley.EasyIAP.API.BuyProduct(ShopProductNames.RemoveAds, ProductBought);
  //}

  private void ProductBought(IAPOperationStatus status, string message, StoreProduct product)
  {
    if (status == IAPOperationStatus.Success)
    {
      //since all consumable products reward the same coin, a simple type check is enough 
      if (product.productType == ProductType.NonConsumable)
      {
        if (product.productName == "StarterPack")
        {
          Debug.Log("PackCoins " + GameManager.totalCoins);

          AddCoin(5000);
          AddGem(15);


        }
      }
      else if (product.productType == ProductType.Consumable)
      {
        string[] consumable = product.productName.Split("_");
        switch (consumable[0])
        {
          case "coins":
            AddCoin(product.value);
            break;
          case "gems":
            AddGem(product.value);
            break;
          case "lives":
            break;
        }
      }

      DataPersistenceManager.instance.SaveGame(); //saves data
      UpgradesManager.updateCosts(); //update ui
      Debug.Log("After purchase PackCoins " + GameManager.totalCoins);

      if (product.productName == "RemoveAds")
      {
        removeAds = true;
        //disable ads here
      }
    }
  }

  public void RestorePurchases()
  {
    API.RestorePurchases(ProductRestored);
  }

  private void ProductRestored(IAPOperationStatus status, string message, StoreProduct product)
  {
    if (status == IAPOperationStatus.Success)
    {
      if (product.productName == "RemoveAds")
      {
        removeAds = true;
        //disable ads here
      }
    }
    else
    {
      Debug.Log("Error occurred: " + message);
    }
  }
}
