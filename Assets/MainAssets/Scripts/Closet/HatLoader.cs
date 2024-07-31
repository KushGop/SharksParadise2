using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.EasyIAP;

public class HatLoader : MonoBehaviour, IDataPersistence
{
  private ShopProductNames selectedHat;

  public ShopProductNames GetHat()
  {
    return selectedHat;
  }
  public void SetHat(ShopProductNames newHat)
  {
    selectedHat = newHat;
  }

  public void LoadData(GameData data)
  {
    print("load hat");
    selectedHat = data.hat;
  }

  public void SaveData(GameData data)
  {
    data.hat = selectedHat;
  }
}
