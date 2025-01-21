using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightText : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI text;

  private void Start()
  {
    text.text = "Day 1";
    GameManager.switchToDay += ChangeToDay;
    GameManager.switchToNight += ChangeToNight;
  }
  private void OnDestroy()
  {
    GameManager.switchToDay -= ChangeToDay;
    GameManager.switchToNight -= ChangeToNight;
  }

  private void ChangeToNight()
  {
    text.text = "Night " + GameManager.day;
  }
  private void ChangeToDay()
  {
    text.text = "Day " + GameManager.day;
  }
}
