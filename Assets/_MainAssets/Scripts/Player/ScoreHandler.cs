using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
  public Transform newPoints;
  public Transform multiplyerText;
  public float multiplyerTimer = 0f;
  public int starfishMultiplyer = 1;

  private void Start()
  {
    GameManager.multiplyer = 1;
  }

  public void UpdateLastFish(Fishes fishName)
  {
    GameManager.lastFish = fishName;
    GameManager.fishEaten();
  }
  public void UpdateMultiplyer(Fishes fishName, Vector3 pos)
  {
    if (fishName == GameManager.lastFish)
    {
      MissionData.IncrementMission(MissionName.multiplyerMax);
      if (GameManager.multiplyer < EnemyList.multiplyerCap[fishName] && fishName == GameManager.lastFish)
      {
        GameManager.multiplyer++;
      }
      multiplyerText.GetComponent<NewPoints>().OnMultiply(GameManager.multiplyer * starfishMultiplyer, pos);
      // Mathf.Lerp(1f, 0, 1.2f * gameStats.multiplyer * Time.deltaTime);
    }
    else
    {
      GameManager.multiplyer = 1;
      if (starfishMultiplyer == 2)
      {
        multiplyerText.GetComponent<NewPoints>().OnMultiply(GameManager.multiplyer * starfishMultiplyer, pos);
      }
      MissionManager.ResetMission(MissionName.multiplyerMax);
      MissionData.IncrementMission(MissionName.multiplyerMax);
    }
  }

  public void AddPoints(int value)
  {
    GameManager.score += value * GameManager.multiplyer * starfishMultiplyer;
    newPoints.GetComponent<NewPoints>().OnNewPoint(value * GameManager.multiplyer * starfishMultiplyer);
  }

  //IEnumerator MultiplyerTimeout()
  //{
  //  //TODO: Multiplyer fades away
  //  yield return new WaitForSeconds(2f);
  //}
}
