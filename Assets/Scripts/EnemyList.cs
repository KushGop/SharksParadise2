using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyList", menuName = "Sharks Paradise/EnemyList", order = 0)]
public class EnemyList : ScriptableObject
{
  List<string> fullList = new List<string> { "SmallShark", "BigShark", "GoldFish", "AngelFish", "Jellyfish", "Starfish", "Food", "Squid", "CoinFish" };
  Dictionary<string, int> pointDictionary = new Dictionary<string, int> { { "SmallShark", 100 }, { "BigShark", 1000 }, { "GoldFish", 10 }, { "AngelFish", 15 }, { "Jellyfish", 50 }, { "Starfish", 0 }, { "Food", 5 }, { "Squid", 150 }, { "CoinFish", 0 } };
  public List<string> triggerAreaList = new List<string> { "SmallShark", "BigShark", "GoldFish", "AngelFish", "Squid", "CoinFish" };
  public List<string> playAreaList = new List<string> { "Prey", "Predator" };
  public Dictionary<string, int> multiplyerCap = new Dictionary<string, int> { { "SmallShark", 5 }, { "GoldFish", 20 }, { "AngelFish", 15 }, { "Jelly", 8 }, { "Bird", 8 }, { "Starfish", 1 }, { "SharkNet", 50 }, { "Squid", 5 }, { "CoinFish", 0 } };

  //Fishname : scaleModifier  USED FOR SKELETON
  public Dictionary<string, float> scaleModifier = new Dictionary<string, float> { { "SmallShark", -1.5f }, { "BigShark", -1.5f }, { "GoldFish", 1f }, { "AngelFish", 2f }, { "CoinFish", 1f } };
}

