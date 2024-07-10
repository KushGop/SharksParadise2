using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.EasyIAP;

public class PopulateCloset : MonoBehaviour
{
  [SerializeField] Closet_ScritableObject hats;
  [SerializeField] GameObject hatPrefab;
  [SerializeField] HatLoader hatLoader;
  [SerializeField] SnapToItem snap;
  private ShopProductNames hatSelected;

  private void Start()
  {
    hatSelected = hatLoader.GetHat();
    GameObject newHat;
    int i = 0;
    foreach (ShopProductNames hat in hats.hatDictionary.Keys)
    {
      if (hat == hatSelected)
        snap.SetCurrentItem(i);

      newHat = Instantiate(hatPrefab, transform);
      Sprite s = hats.hatDictionary[hat].GetComponent<SpriteRenderer>().sprite;
      newHat.GetComponent<HatScript>().SetValues(
        hat,
        API.GetLocalizedPriceString(hat),
        hat != ShopProductNames.NoHat ? API.IsActive(hat) : true,
        hat == hatSelected,
        s
        );
      i++;
    }
  }
}
