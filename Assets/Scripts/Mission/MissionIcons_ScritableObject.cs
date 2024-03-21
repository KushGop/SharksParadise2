using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "MissionIcons", menuName = "Sharks Paradise/MissionIcons")]
public class MissionIcons_ScritableObject : ScriptableObject
{
  public SerializedDictionary<MissionName, Sprite> iconDictionary;
}
