using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Indicator attached to predator shark
public class IndicatorScript : MonoBehaviour
{
  [SerializeField] Image arrow;

  [SerializeField] Color red;
  [SerializeField] Color green;
  Transform target, player;
  float scale;

  private void Start()
  {
    GameManager.Invincible += SwitchColor;
    SwitchColor(GameManager.isInvincible);
  }
  private void OnDestroy()
  {
    GameManager.Invincible -= SwitchColor;
  }

  private void LateUpdate()
  {
    scale = Mathf.Abs(Vector3.Distance(target.position, player.position)) / 12;
    transform.localScale = Vector3.one * scale;
    transform.right = target.position - player.position;
  }

  public void PointToward(Transform obj, Transform player)
  {
    target = obj;
    this.player = player;
  }

  private void SwitchColor(bool invincible)
  {
    arrow.color = invincible ? green : red;
  }
}
