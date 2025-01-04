using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutAdjust : MonoBehaviour
{

  [SerializeField] private GridLayoutGroup grid;
  private void Awake()
  {
    grid.cellSize = new(Screen.width > 1300 ? Screen.width * 0.6f : Screen.width * 0.75f, grid.cellSize.y);
  }
}
