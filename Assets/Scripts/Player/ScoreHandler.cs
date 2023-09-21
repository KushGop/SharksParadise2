using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{

  public GameSessionStats gameStats;
  public MissionStats missionStats;
  public EnemyList enemyList;
  public Transform newPoints;
  public Transform multiplyerText;
  public float multiplyerTimer = 0f;
  public int starfishMultiplyer = 1;

  private void Start()
  {
    gameStats.multiplyer = 1;
  }

  public void updateLastFish(string fishName)
  {
    gameStats.lastFish = fishName;
  }
  public void updateMultiplyer(string fishName, Vector3 pos)
  {
    if (fishName == gameStats.lastFish || starfishMultiplyer > 1)
    {
      missionStats.IncrementMission(MissionName.multiplyerMax);
      if (gameStats.multiplyer < enemyList.multiplyerCap[fishName] && fishName == gameStats.lastFish)
      {
        gameStats.multiplyer++;
      }
      multiplyerText.GetComponent<NewPoints>().OnMultiply(gameStats.multiplyer * starfishMultiplyer, pos);
      // Mathf.Lerp(1f, 0, 1.2f * gameStats.multiplyer * Time.deltaTime);
    }
    else
    {
      gameStats.multiplyer = 1;
      missionStats.ResetMission(MissionName.multiplyerMax);
      missionStats.IncrementMission(MissionName.multiplyerMax);
    }
  }

  public void addPoints(int value)
  {
    gameStats.score += value * gameStats.multiplyer * starfishMultiplyer;
    newPoints.GetComponent<NewPoints>().OnNewPoint(value * gameStats.multiplyer * starfishMultiplyer);
  }

  IEnumerator MultiplyerTimeout()
  {
    yield return new WaitForSeconds(2f);
  }
}
