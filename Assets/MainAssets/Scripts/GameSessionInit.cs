using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionInit : MonoBehaviour
{
  void Start()
  {
    GameManager.coins = 0;
    GameManager.gems = 0;
    GameManager.score = 0;
    GameManager.xp = 0;
  }
}
