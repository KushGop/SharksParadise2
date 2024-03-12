using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradesManager
{
  public static UpgradesData upgradesData = new();
  public delegate void UpdateUI();
  public static UpdateUI updateCosts;
}
