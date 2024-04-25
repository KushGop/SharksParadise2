using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionCollectCoins : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI coinCountUI;
  [SerializeField] private AudioSource ding;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    ding.PlayOneShot(ding.clip);
    coinCountUI.text = (int.Parse(coinCountUI.text) + 10).ToString();
    Destroy(collision.gameObject);
  }
}
