using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeScreen : MonoBehaviour
{
  [SerializeField] GameObject children;

  private void Start()
  {
    children.SetActive(false);
    UpgradesManager.tryPrestige += SetActive;
  }
  private void OnDestroy()
  {
    UpgradesManager.tryPrestige -= SetActive;
  }

  private void SetActive()
  {
    children.SetActive(true);
  }

  public void Yes()
  {
    children.SetActive(false);
    UpgradesManager.activateReward(UpgradesManager.rewards[RewardType.prestige].Item1, UpgradesManager.rewards[RewardType.prestige].Item2);
    UpgradesManager.upgradesData.upgrades[UpgradeList.prestigeCount]++;
    UpgradesManager.prestige();
    DataPersistenceManager.instance.SaveGame();
  }

  public void No()
  {
    children.SetActive(false);
  }
}
