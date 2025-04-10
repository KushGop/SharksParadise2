using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaIndicator : MonoBehaviour
{
  [SerializeField] private CircleCollider2D circle;
  private PlayerMovement movement;

  private void Start()
  {
    circle.radius = 13 + (UpgradesManager.upgradesData.upgrades[UpgradeList.warningRadius] * 0.1f);
    movement = transform.parent.GetComponent<PlayerMovement>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.CompareTag("Predator"))
    {
      movement.Indicator(1, FishType.PREDATOR, other.transform);
    }
  }
  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.transform.CompareTag("Predator"))
    {
      movement.Indicator(-1, FishType.PREDATOR, other.transform);
    }
  }
}
