using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identifier : MonoBehaviour
{
  public Fishes fishName;
  public FishType fishType;
  public int value, id;

  public Identifier(Fishes name, FishType type, int val)
  {
    fishName = name;
    fishType = type;
    value = val;
  }
}
