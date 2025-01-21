using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tools
{
  public class Tools
  {
    public string ShortenNum(int num) => ShortenNum(num.ToString());
    public string ShortenNum(string num)
    {
      int val = Convert.ToInt32(num);
      if (val > 999 && val < 999999)
        return Mathf.FloorToInt(val / 1000).ToString();
      if (val > 999999)
        return Mathf.FloorToInt(val / 1000000).ToString();
      return num;
    }
  }
}
