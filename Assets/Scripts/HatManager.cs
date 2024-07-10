using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.EasyIAP;

public static class HatManager
{
  public delegate void NormalEvent();
  public static NormalEvent ClearSelected;

  public static ShopProductNames selectedHat;
}
