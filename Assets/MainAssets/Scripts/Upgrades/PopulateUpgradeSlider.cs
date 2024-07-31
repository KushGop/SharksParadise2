using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateUpgradeSlider : MonoBehaviour
{

  public GameObject upgradeSlider;
  public GameObject prestigeButton;
  public GameObject empty;

  private void Start()
  {
    foreach (KeyValuePair<UpgradeList, int> pair in UpgradesManager.upgradesData.upgrades)
    {
      if ((ushort)pair.Key == 20) continue;
      if ((ushort)pair.Key == 10)
      {
        GameObject pButton = Instantiate(prestigeButton, transform);
        Instantiate(empty, transform);
      }
      InstatiateSlider(pair);
    }
    void InstatiateSlider(KeyValuePair<UpgradeList, int> pair)
    {
      GameObject slider = Instantiate(upgradeSlider, transform);
      slider.transform.GetComponent<UpgradeSlider>().SetKeyPair(pair.Key, pair.Value);
    }
  }
}
