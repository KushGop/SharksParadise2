using System.Collections;
using System.Collections.Generic;
using Gley.EasyIAP;
using UnityEngine;

public class ShopSystem : MonoBehaviour, IDataPersistence
{
  int coins;
  GameData data;
  bool removeAds;
  void Start()
  {
    this.data = new GameData();
    Gley.EasyIAP.API.Initialize(InitializationComplete);
    //Debug.Log("PackCoins " + GameManager.totalCoins);
  }


  private void InitializationComplete(IAPOperationStatus status, string message, List<StoreProduct> shopProducts)
  {
    if (status == IAPOperationStatus.Success)
    {
      //IAP was successfully initialized
      //loop through all products
      for (int i = 0; i < shopProducts.Count; i++)
      {
        if (shopProducts[i].productName == "RemoveAds")
        {
          //if the active property is true, the product is bought
          if (shopProducts[i].active)
          {
            removeAds = true;
          }
        }
      }
    }
    else
    {
      Debug.Log("Error occurred: " + message);
    }
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
    Gley.EasyIAP.API.BuyProduct(ShopProductNames.StarterPack, ProductBought);
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
      if (product.productType == ProductType.Consumable)
      {
        if (product.productName == "StarterPack")
        {
          Debug.Log("PackCoins " + GameManager.totalCoins);

          //coins += product.value;
          // GameManager.totalCoins += 5000;
          // GameManager.totalGems += 15;
          // Debug.Log("After purchase PackCoins " + GameManager.totalCoins);
          // data.totalCoins = GameManager.totalCoins;
          // data.totalGems = GameManager.totalGems;
          // UpgradesManager.updateCosts();
          //// DataPersistenceManager.instance.LoadGame();
          // DataPersistenceManager.instance.SaveGame();

          AddCoin(5000);
          AddGem(15);
          DataPersistenceManager.instance.SaveGame();
          UpgradesManager.updateCosts();
          Debug.Log("After purchase PackCoins " + GameManager.totalCoins);

        }

      }

      if (product.productName == "RemoveAds")
      {
        removeAds = true;
        //disable ads here
      }
    }
  }

  public void RestorePurchases()
  {
    Gley.EasyIAP.API.RestorePurchases(ProductRestored);
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
