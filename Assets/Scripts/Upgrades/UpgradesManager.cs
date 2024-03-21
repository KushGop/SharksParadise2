using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradesManager
{
  public static UpgradesData upgradesData = new();

  public delegate void UpdateUI();
  public static UpdateUI updateCosts;

  public delegate void Reset();
  public static Reset prestige;

  public delegate void Reward(RewardList[] rl, int[] a);
  public static Reward activateReward;

  public delegate void TryPrestige();
  public static TryPrestige tryPrestige;


  public static Dictionary<RewardType, (RewardList[], int[])> rewards = new()
  {
    { RewardType.fullUpgrade, (new[] { RewardList.gems, RewardList.coins }, new[] { 10, 500 }) },
    { RewardType.prestige, (new[] { RewardList.token, RewardList.gems }, new[] { 1, 30 }) },
  };
}
