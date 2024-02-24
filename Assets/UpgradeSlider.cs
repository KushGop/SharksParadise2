using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlider : MonoBehaviour
{
  private UpgradeList key;
  [SerializeField] Slider slider;

  public void IncrementUpgrade()
  {
    if (UpgradesManager.upgradesData.upgrades.ContainsKey(key))
    {
      UpgradesManager.upgradesData.upgrades[key]++;
      if (UpgradesManager.upgradesData.upgrades[key] > 10)
      {
        Debug.Log("Error: Over max limit for " + key.ToString());
      }
    }
    else
    {
      Debug.Log("Does not contain upgrade key: " + key.ToString());
    }
    slider.value = UpgradesManager.upgradesData.upgrades[key];
  }

  public void SetKeyPair(UpgradeList key, int count)
  {
    this.key = key;
    slider.value = count;
  }
}
