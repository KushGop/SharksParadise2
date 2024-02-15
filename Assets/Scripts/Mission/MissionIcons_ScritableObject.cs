using UnityEngine;

[CreateAssetMenu(fileName = "MissionIcons", menuName = "Sharks Paradise/MissionIcons")]
public class MissionIcons_ScritableObject : ScriptableObject
{
  public SerializableDictionary<MissionName, Sprite> iconDictionary = new()
  {
    { MissionName.timesInked, null },
    { MissionName.bigSharkDodges, null },
    { MissionName.bigSharksEaten, null },
    { MissionName.birdsEaten, null },
    { MissionName.coinsCollected, null },
    { MissionName.multiplyerMax, null },
    { MissionName.starfishesCollected, null },
    { MissionName.timesStung, null },
  };
}
