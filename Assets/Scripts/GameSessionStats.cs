using UnityEngine;


[CreateAssetMenu(fileName = "GameSessionStats", menuName = "Sharks Paradise/GameSessionStats", order = 0)]
public class GameSessionStats : ScriptableObject
{
  public int score, highscore, xp, coins, totalCoins, totalXP;
  public string lastFish;
  public int multiplyer, multiplyerCap;
  public Transform position;

  public string getVariableToString(string name)
  {
    return GetType().GetField(name).GetValue(this).ToString();
  }
}
