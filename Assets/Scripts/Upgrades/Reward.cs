using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AYellowpaper.SerializedCollections;

public class Reward : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI amount;
  [SerializeField] private Image img;

  [Header("Arrays need to line up")]
  [SerializedDictionary("Reward", "Icon")]
  public SerializedDictionary<RewardList, Sprite> rewards;
  void Start()
  {
    StartCoroutine(GrowReward());
  }

  public void SetInfo(RewardList r, int a)
  {
    img.sprite = rewards[r];
    amount.text = "*" + a.ToString();
  }

  IEnumerator GrowReward()
  {
    float elapsedTime = 0;
    float waitTime = 0.3f;
    while (elapsedTime < waitTime)
    {
      transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, (elapsedTime / waitTime));
      elapsedTime += Time.deltaTime;

      // Yield here
      yield return null;
    }
    yield return null;
  }
}
