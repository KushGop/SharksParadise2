using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identifier : MonoBehaviour
{
  public string fishName, fishType;
  public int value;

  public Identifier(string name, string type, int val) {
    fishName = name;
    fishType = type;
    value = val;
  }
}
