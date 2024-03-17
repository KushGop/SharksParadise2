using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrestigeButton : MonoBehaviour
{

  [SerializeField] Button button;
  [SerializeField] TextMeshProUGUI pres;

  private void Start()
  {
    UpgradesManager.updateCosts += ChangeButton;
    Disable();
    ChangeButton();
  }

  public void ChangeButton()
  {
    foreach (KeyValuePair<UpgradeList, int> pair in UpgradesManager.upgradesData.upgrades)
    {
      if ((ushort)pair.Key < 10)
      {
        if (pair.Value != 10)
        {
          Disable();
          return;
        }
      }
      else
      {
        break;
      }
    }
    pres.alpha = 1f;
    button.interactable = true;
  }

  private void Disable()
  {
    button.interactable = false;
    pres.alpha = 0.5f;
  }

  public void ActivateButton()
  {
    print("ACTIVATE PRESTIGE");
  }
}
