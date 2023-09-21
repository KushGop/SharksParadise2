using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionInit : MonoBehaviour
{

  public GameSessionStats gameSessionStats;

  // Start is called before the first frame update
  void Start()
  {
    gameSessionStats.coins  = 0;
    gameSessionStats.score = 0;
    gameSessionStats.xp = 0;
  }
}
