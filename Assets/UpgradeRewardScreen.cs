using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRewardScreen : MonoBehaviour
{
  [SerializeField] private Image backdrop;
  [SerializeField] private GameObject reward;
  private Color colorIn = new(0, 0, 0, 0.33f);
  private Color colorOut = new(0, 0, 0, 0);
  private bool tap = false;

  private void Start()
  {
    backdrop.color = colorOut;
    UpgradesManager.activateReward += StartRewards;
    gameObject.SetActive(false);
  }

  private void OnEnable()
  {
    StartCoroutine(FadeIn());
  }
  /*
   * Event: Full upgrade stat
   * 
   * fade background in, fade object in, grow object in
   * claim reward
   * 
   * 
   * Event: Tap screen
   * 
   * fade object out, fade next object in if needed, grow next object in if needed
   * else
   * fade background out, fade object out
   */
  public void OnTap()
  {
    tap = true;
  }
  private void StartRewards(RewardList[] rewards, int[] amount)
  {
    gameObject.SetActive(true);
    StartCoroutine(ShowRewards(rewards, amount));
  }
  IEnumerator ShowRewards(RewardList[] rewards, int[] amount)
  {
    for (int i = 0; i < rewards.Length; i++)
    {
      GameObject newReward = Instantiate(reward, transform);
      newReward.transform.GetComponent<Reward>().SetInfo(rewards[i], amount[i]);
      while (!tap)
        yield return null;
      tap = false;
      Destroy(newReward);
    }
    StartCoroutine(FadeOut());
    yield return null;
  }

  IEnumerator FadeIn()
  {
    float elapsedTime = 0f;
    float waitTime = 1f;
    while (elapsedTime < waitTime)
    {
      backdrop.color = Color.Lerp(backdrop.color, colorIn, elapsedTime / waitTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
  }
  IEnumerator FadeOut()
  {
    float elapsedTime = 0f;
    float waitTime = 1f;
    while (elapsedTime < waitTime)
    {
      backdrop.color = Color.Lerp(backdrop.color, colorOut, elapsedTime / waitTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    gameObject.SetActive(false);
  }
}
