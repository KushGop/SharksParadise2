using UnityEngine;
using AYellowpaper.SerializedCollections;
using Gley.EasyIAP;

[CreateAssetMenu(fileName = "Closet", menuName = "Sharks Paradise/Closet")]
public class Closet_ScritableObject : ScriptableObject
{
  public SerializedDictionary<ShopProductNames, GameObject> hatDictionary;
}
