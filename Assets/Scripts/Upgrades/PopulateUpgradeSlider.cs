using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateUpgradeSlider : MonoBehaviour
{

  public GameObject upgradeSlider;

  private void Start()
  {
    foreach (KeyValuePair<UpgradeList, int> pair in UpgradesManager.upgradesData.upgrades)
    {
      GameObject slider = Instantiate(upgradeSlider, transform);
      slider.transform.GetComponent<UpgradeSlider>().SetKeyPair(pair.Key, pair.Value);
    }
  }
}
