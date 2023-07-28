using UnityEngine;


[CreateAssetMenu(fileName = "GameSessionStats", menuName = "Sharks Paradise/GameSessionStats", order = 0)]
public class GameSessionStats : ScriptableObject
{
  public int score, highscore;
  public string lastFish;
  public int multiplyer, multiplyerCap;
  public Transform position;
}
