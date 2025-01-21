using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBoatFactroy : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Boat";
  }
  
}
