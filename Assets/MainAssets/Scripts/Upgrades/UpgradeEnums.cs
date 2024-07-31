/*
 *  0x is a coin
 *  1x is a gem
 *  2x is for prestige
 *  x0 is for double base
 *  x1 is for double base
 */

public enum UpgradeList : ushort
{
  baseSpeed = 00,
  boostSpeed = 01,
  boostCost = 02,
  jumpCost = 03,
  refillSpeed = 04,
  refillDelay = 05,
  treasureFrequency = 10,
  starfishFrequency = 11,
  warningRadius = 12,
  coinFishFrequency = 13,
  powerTime = 14,
  prestigeCount = 20,
  greatStarfishFrequency = 21,
  greatTreasureFrequency = 22,
  greatCoinFishFrequency = 23
}

public enum RewardList
{
  coins,
  gems,
  token,
  life
}

public enum RewardType
{
  prestige,
  fullUpgrade
}
