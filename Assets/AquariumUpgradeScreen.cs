using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumUpgradeScreen : MonoBehaviour
{

  [SerializeField] GameObject children;

  private void Start()
  {
    children.SetActive(false);
    AquariumManager.tryUpgrade += SetActive;
  }
  private void OnDestroy()
  {
    AquariumManager.tryUpgrade -= SetActive;
  }

  private void SetActive()
  {
    children.SetActive(true);
  }

  public void Yes()
  {
    children.SetActive(false);
    AquariumManager.SelectUpgrade(true);
  }

  public void No()
  {
    AquariumManager.SelectUpgrade(false);
    children.SetActive(false);
  }
}
