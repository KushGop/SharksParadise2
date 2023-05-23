using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyList", menuName = "Sharks Paradise/EnemyList", order = 0)]
public class EnemyList : ScriptableObject
{
  List<string> fullList = new List<string>{"Shark","GoldFish","AngelFish","Jellyfish","Starfish","Food"};
  Dictionary<string,int> pointDictionary = new Dictionary<string,int>{{"Shark",100},{"GoldFish",10},{"AngelFish",15},{"Jellyfish",50},{"Starfish",0},{"Food",5}};
  public List<string> triggerAreaList = new List<string>{"Shark","GoldFish","AngelFish"};
  public List<string> playAreaList = new List<string>{"Prey","Predator"};
}
