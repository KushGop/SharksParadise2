using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.GameServices;

public class FirstAchievement : MonoBehaviour
{
  void Start()
  {
    if (!API.IsComplete(AchievementNames.SharksParadise) && API.IsLoggedIn())
    {
      API.SubmitAchievement(AchievementNames.SharksParadise);
    }
  }
}
