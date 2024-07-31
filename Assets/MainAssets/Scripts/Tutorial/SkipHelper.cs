using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkipHelper : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI coinCount;

  // Start is called before the first frame update
  void Start()
  {
    UpgradesManager.updateCosts += SkipQuickUpdate;
  }

  void OnDestroy()
  {
    UpgradesManager.updateCosts -= SkipQuickUpdate;
  }

  private void SkipQuickUpdate()
  {
    coinCount.text = string.Format("{0}", GameManager.missionCoins + GameManager.totalCoins);
  }
}
