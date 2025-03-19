using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFactory : AbstractFactory
{

  bool isFrenzy = false;

  private void Awake()
  {
    isFrenzy = false;
    GameManager.FullBirdMeter += BirdFrenzy;
  }
  private void OnDestroy()
  {
    GameManager.FullBirdMeter -= BirdFrenzy;
  }
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Bird";
    newEnemy.transform.tag = isFrenzy ? "Food" : "Prey";
    newEnemy.transform.localScale = new Vector3(-1, 1, 1) * (player.playerSize / 100);
    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.SEAGULL;
    identifier.fishType = isFrenzy ? FishType.FOOD : FishType.PREY;
    identifier.value = value + Random.Range(lower, upper);

    if (isFrenzy && GameManager.isNight)
    {
      newEnemy.GetComponent<BirdMovement>().QuickNightSet();
    }
  }

  void BirdFrenzy()
  {
    isFrenzy = true;
    for (int i = 0; i < 60; i++)
    {
      AddEnemy();
    }
  }

}
