using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "Aquarium", menuName = "Sharks Paradise/Aquarium")]
public class Aquarium_SO : ScriptableObject
{
  public SerializedDictionary<Fishes, GameObject> images;
}
