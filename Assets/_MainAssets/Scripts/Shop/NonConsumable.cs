using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.EasyIAP;

public class NonConsumable : MonoBehaviour
{
  [SerializeField] private ShopProductNames nonConsumable;

  private void Start()
  {
    if (API.GetProductType(nonConsumable) != ProductType.NonConsumable)
    {
      throw new System.Exception("Choose non-consumable");
    }
    if (API.IsActive(nonConsumable))
    {
      gameObject.SetActive(false);
    }
    UpgradesManager.updateCosts += UpdateScreen;
  }

  private void OnDestroy()
  {
    UpgradesManager.updateCosts -= UpdateScreen;
  }

  private void UpdateScreen()
  {
    if (API.IsActive(nonConsumable))
    {
      gameObject.SetActive(false);
    }
  }
}
