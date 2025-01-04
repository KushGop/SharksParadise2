using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TutorialManager
{

  public delegate void TutEvent();

  public static TutEvent joystick;
  public static TutEvent boost;
  public static TutEvent jump;
  public static TutEvent energy;
  public static TutEvent fish;
  public static TutEvent bird;
  public static TutEvent score;
  public static TutEvent jelly;
  public static TutEvent coinFish;
  public static TutEvent nightIcon;
  public static TutEvent dayNight;
  public static TutEvent night;
  public static TutEvent bigShark;
  public static TutEvent endSeq;
  public static TutEvent title;

  public delegate void TutNextEvent();

  //public static UnityEvent moved;
  public static TutNextEvent moved;
  public static TutNextEvent boostPressed;
  public static TutNextEvent jumpPressed;
  public static TutNextEvent eatFish;
  public static TutNextEvent eatBird;
  public static TutNextEvent death;

  public static readonly float tutorialDelayTime = 1f;
  public static bool isInTutorial = false;
  public static bool shouldMove = false;
}
