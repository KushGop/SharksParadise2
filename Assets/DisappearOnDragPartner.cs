using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnDragPartner : MonoBehaviour
{
  private void Start()
  {
    gameObject.SetActive(true);
    GameManager.disappear += Disappear;
  }
  private void OnDestroy()
  {
    GameManager.disappear -= Disappear;
  }
  private void Disappear()
  {
    gameObject.SetActive(false);
  }
}
