using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFactory : AbstractFactory
{
  protected override void SetPreferences()
  {
    newEnemy.transform.name = "Bubble";
    newEnemy.transform.tag = "Bubble";
    newEnemy.transform.localScale = new Vector3(1, 1, 1);
    identifier = newEnemy.AddComponent<Identifier>();

    identifier.fishName = Fishes.BUBBLE;
    identifier.fishType = FishType.BUBBLE;
    identifier.value = 3;
  }
  protected override Quaternion SetRotation()
  {
    return Quaternion.identity;
  }
  public override void UpdateObject(Transform o)
  {
    UpdateOrigin();
    o.SetPositionAndRotation(SetPosition(), SetRotation());
  }
}
