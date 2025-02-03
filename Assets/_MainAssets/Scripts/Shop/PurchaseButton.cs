using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.EasyIAP;
using TMPro;

public class PurchaseButton : MonoBehaviour, IDataPersistence
{
  private enum ShopType
  {
    COIN,
    DIAMOND,
    PRESTIGE,
    STARTER,
    REMOVE_ADS
  }

  [SerializeField] ShopProductNames productName;
  [SerializeField] TextMeshProUGUI priceText;
  [SerializeField] private ShopType shopType;
  //bool removeAds;

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
  public void AddPrestige(int value)
  {
    GameManager.totalTokens += value;
  }

  public void SaveData(GameData data)
  {
    data.totalCoins = GameManager.totalCoins;
    data.totalGems = GameManager.totalGems;
    data.totalTokens = GameManager.totalTokens;
  }

  public void BuyStarterPack()
  {
    API.BuyProduct(ShopProductNames.StarterPackage, ProductBought);
  }
  public void BuyRemoveAds()
  {
    API.BuyProduct(ShopProductNames.RemoveAds, ProductBought);
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
        if (shopType == ShopType.STARTER)
        {
          Debug.Log("PackCoins " + GameManager.totalCoins);

          AddCoin(5000);
          AddGem(15);
          //AddPrestige(1);
        }
        else if (shopType == ShopType.REMOVE_ADS)
        {
          Gley.MobileAds.API.RemoveAds(true);
          print("Ads removed");
        }
      }
      else if (product.productType == ProductType.Consumable)
      {
        //string[] consumable = product.productName.Split("_");
        //print(consumable);
        switch (shopType)
        {
          case ShopType.COIN:
            AddCoin(product.value);
            break;
          case ShopType.DIAMOND:
            AddGem(product.value);
            break;
          case ShopType.PRESTIGE:
            break;
        }
      }

      DataPersistenceManager.instance.SaveGame(); //saves data
      UpgradesManager.updateCosts(); //update ui

      //if (product.productName == "RemoveAds")
      //{
      //  removeAds = true;
      //  //disable ads here
      //}
    }
    else if (status == IAPOperationStatus.Fail)
    {
      print("Purchase Failed");
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
      //if (product.productName == "RemoveAds")
      //{
      //  removeAds = true;
      //  //disable ads here
      //}
    }
    else
    {
      Debug.Log("Error occurred: " + message);
    }
  }
}
