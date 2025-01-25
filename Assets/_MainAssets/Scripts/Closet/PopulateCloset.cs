using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.EasyIAP;
using UnityEngine.UI;

public class PopulateCloset : MonoBehaviour
{
  [SerializeField] Closet_ScritableObject hats;
  [SerializeField] ClosetManager closetManager;
  [SerializeField] HatLoader hatLoader;
  //[SerializeField] SnapToItem snap;
  private ShopProductNames hatSelected;

  private void Start()
  {
    hatSelected = hatLoader.GetHat();
    GameObject newHat;
    foreach (ShopProductNames hat in hats.hatDictionary.Keys)
    {

      newHat = Instantiate(hats.hatDictionary[hat], transform);
      HatScript hs = newHat.GetComponent<HatScript>();
      hs.SetValues(
        hat,
        API.GetLocalizedPriceString(hat),
        hat != ShopProductNames.NoHat ? API.IsActive(hat) : true,
        hat == hatSelected,
        closetManager
        );
      if (hat == hatSelected)
      {
        closetManager.SelectHat(hs);
      }
    }
  }
}
