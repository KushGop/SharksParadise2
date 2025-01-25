using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.EasyIAP;
using UnityEngine.UI;
using AYellowpaper.SerializedCollections;
using TMPro;

public class ClosetManager : MonoBehaviour
{
  HatScript selectedHat;
  [SerializeField] Button button;
  [SerializeField] TextMeshProUGUI label;
  [SerializeField] HatLoader loader;
  [SerializeField] Image hatImage;
  [SerializeField] SerializedDictionary<ShopProductNames, Sprite> hatSprites;

  public void SelectHat(HatScript hat)
  {
    selectedHat = hat;
    hatImage.sprite = hatSprites[hat.hatName];
    if (selectedHat.isUnlocked)
    {
      if (selectedHat.isSelected)
      {
        label.text = "Selected";
      }
      else
      {
        label.text = "wear";
      }
    }
    else
    {
      label.text = selectedHat.price;
    }
  }

  public void ButtonClick()
  {
    if (selectedHat.isUnlocked)
    {
      HatManager.ClearSelection();
      selectedHat.isSelected = true;
      selectedHat.OnChoice();
      label.text = "Selected";
      loader.SetHat(selectedHat.hatName);
      DataPersistenceManager.instance.SaveGame();
    }
    else
    {
      BuyHat();
    }
  }

  public void BuyHat()
  {
    API.BuyProduct(selectedHat.hatName, ProductBought);
  }

  private void ProductBought(IAPOperationStatus status, string message, StoreProduct product)
  {
    if (status == IAPOperationStatus.Success)
    {
      selectedHat.Purchased();
      selectedHat.isUnlocked = true;
      label.text = "wear";
    }
    else if (status == IAPOperationStatus.Fail)
      print("Purchase Failed");
  }
}
