using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMobileJoystick : MobileJoystick
{
  private bool hasBoost = false, tutJumped = false;

  void Awake()
  {
    jumpDistance = 1000000000;
  }

  public void SetJumpDistance()
  {
    jumpDistance = 90000;
  }
  public override void BoostPressed()
  {
    if (!hasBoost)
      TutorialManager.boostPressed(); hasBoost = true;
    base.BoostPressed();
  }
  public override void JumpPressed()
  {
    if (!tutJumped)
      TutorialManager.jumpPressed(); tutJumped = true;
    base.JumpPressed();
  }
}
