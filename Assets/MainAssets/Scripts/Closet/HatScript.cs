using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gley.EasyIAP;

public class HatScript : MonoBehaviour
{
  [SerializeField] Image sprite;
  internal string price;
  internal bool isUnlocked;
  internal bool isSelected;
  internal int index;
  internal ShopProductNames hatName;

  private void Start()
  {
    HatManager.ClearSelected += ClearSelected;
  }

  public void SetValues(ShopProductNames name, string price, bool isUnlocked, bool isSelected, Sprite hat)
  {
    sprite.sprite = hat;
    hatName = name;
    this.price = price;
    this.isUnlocked = isUnlocked;
    this.isSelected = isSelected;
  }

  private void ClearSelected()
  {
    isSelected = false;
  }

}
