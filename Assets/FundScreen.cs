using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FundScreen : MonoBehaviour
{
  [SerializeField] GameObject children;

  private void Start()
  {
    children.SetActive(false);
    AquariumManager.funds += () => children.SetActive(true);
  }
  private void OnDestroy()
  {
    AquariumManager.funds -= () => children.SetActive(true);
  }
}
